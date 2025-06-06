using LogicaAplicacion.DTOs.UsuarioDTOs;
using System.ComponentModel.DataAnnotations;
using LogicaAplicacion.InterfacesCasosUsos.Profesor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.DTOs.ProfesorDTOs;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LogicaAplicacion.DTOs.SolocitudUnionDTOs;
using LogicaAplicacion.InterfacesCasosUsos.SolicitudUnion;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly  IAltaProfesor _altaProfesor;
        private readonly IObtenerGruposDeProfesor _obtenerGruposDeProfesor;
        private readonly IObtenerSolicitudesUnionDelGrupo _obtenerSolicitudesUnionDelGrupo;
        private readonly IAceptarSolicitudUnion _aceptarSolicitudUnion;
        public ProfesorController(IAltaProfesor altaProfesor, IObtenerGruposDeProfesor obtenerGruposDeProfesor, IObtenerSolicitudesUnionDelGrupo obtenerSolicitudesUnionDelGrupo,IAceptarSolicitudUnion aceptarSolicitudUnion)
        {
            _altaProfesor = altaProfesor;
            _obtenerGruposDeProfesor = obtenerGruposDeProfesor;
            _obtenerSolicitudesUnionDelGrupo = obtenerSolicitudesUnionDelGrupo;
            _aceptarSolicitudUnion = aceptarSolicitudUnion;
        }
        /// <summary>
        /// Este endpoint permite registrar un nuevo Profesor en el sistema.
        /// </summary>
        /// <returns>
        /// 201 Created: Si el estudiante fue registrado correctamente.
        /// 400 Bad Request: Si los datos enviados son inválidos o faltan.
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>

        [HttpPost("alta")]
        [Authorize(Policy = "EsProfesor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AltaProfesor([FromBody] ProfesorAltaDto profesorDto)
        {
            Resultado resultado = await _altaProfesor.EjecutarAsync(profesorDto);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            

            return Created();
        }
        /// <summary>
        /// Obtiene todos los grupos que un profesor posee.
        /// </summary>
        /// <returns>
        /// 200 OK: Retorna la lista de grupos del profesor.
        /// 400 Bad Request: Si el ID del profesor es inválido.
        /// 404 Not Found: Si el profesor no existe.
        /// 500 Internal Server Error: Si ocurre un error inesperado.
        /// </returns>

        [HttpGet("mis-grupos")]
        [Authorize(Policy = "EsProfesor")]
        [ProducesResponseType(typeof(List<GrupoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerGruposProfesor()
        {
            var idProfesorAutenticado = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (string.IsNullOrEmpty(idProfesorAutenticado))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));
            

            Resultado<List<GrupoDto>> resultado = await _obtenerGruposDeProfesor.EjecutarAsync(idProfesorAutenticado);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            

            return Ok(resultado.Valor);
        }
        /// <summary>
        /// Obtiene las solicitudes pendientes de unión a un grupo del profesor autenticado.
        /// </summary>
        /// <param name="grupoId">ID del grupo</param>
        /// <returns>
        /// 200 OK: Lista de solicitudes pendientes.
        /// 400 Bad Request: Grupo inválido o error de validación.
        /// 404 Not Found: Grupo no encontrado.
        /// 401 Unauthorized: Usuario no autenticado.
        /// 500 Internal Server Error: Error inesperado.
        /// </returns>
        [HttpGet("solicitudes-union")]
        [Authorize(Policy = "EsProfesor")]
        [ProducesResponseType(typeof(List<SolicitudUnionListadoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerSolicitudesUnion([FromQuery][Required] int grupoId)
        {
            try
            {
                var idProfesorAutenticado = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(idProfesorAutenticado))
                    return Unauthorized(new { Mensaje = "No se pudo identificar al usuario autenticado." });

                var resultado = await _obtenerSolicitudesUnionDelGrupo.EjecutarAsync(grupoId, idProfesorAutenticado);

                if (resultado.EsFallo)
                {
                    if (resultado.Errores.Any(e => e.Codigo == Error.NotFound.Codigo))
                        return NotFound(resultado.Errores.ToList());

                    return BadRequest(resultado.Errores.ToList());
                }

                return Ok(resultado.Valor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Mensaje = "Ocurrió un error inesperado al obtener las solicitudes. " + ex.Message });
            }
        }
        /// <summary>
        /// Acepta una solicitud de unión de un estudiante a un grupo del profesor autenticado.
        /// </summary>
        /// <param name="solicitudId">ID de la solicitud de unión</param>
        /// <returns>
        /// 200 OK: Solicitud aceptada correctamente.
        /// 400 Bad Request: Datos inválidos o solicitud ya procesada.
        /// 404 Not Found: La solicitud no existe.
        /// 401 Unauthorized: Usuario no autenticado.
        /// 500 Internal Server Error: Error inesperado.
        /// </returns>
        [HttpPost("aceptar-solicitud")]
        [Authorize(Policy = "EsProfesor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AceptarSolicitud([FromQuery][Required] int solicitudId)
        {
            try
            {
                var idProfesor = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(idProfesor))
                    return Unauthorized(new { Mensaje = "No se pudo identificar al usuario autenticado." });

                var resultado = await _aceptarSolicitudUnion.EjecutarAsync(solicitudId);

                if (resultado.EsFallo)
                {
                    if (resultado.Errores.Any(e => e.Codigo == "NotFound" || e.Mensaje.Contains("no existe")))
                        return NotFound(resultado.Errores.ToList());

                    return BadRequest(resultado.Errores.ToList());
                }

                return Ok(new { Mensaje = "La solicitud fue aceptada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Mensaje = "Ocurrió un error inesperado al aceptar la solicitud. " + ex.Message
                });
            }
        }

    }
}
