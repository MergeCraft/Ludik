using LogicaAplicacion.DTOs.ProfesorDTOs;
using System.ComponentModel.DataAnnotations;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using LogicaNegocio.Resultados;
using System.Security.Claims;
using WebApi.Helpers;
using LogicaAplicacion.ImplementacionCasosUsos.Grupos;
using LogicaAplicacion.InterfacesCasosUsos.PerfilEstudiante;
using LogicaAplicacion.DTOs.PerfilEstudianteDTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "EsProfesor")]
    public class GrupoController : ControllerBase
    {
        private readonly IAltaGrupo _altaGrupo;
        private readonly IEditarGrupo _editarGrupo;
        private readonly IBajaGrupo _bajaGrupo;
        private readonly IObtenerInformacionGrupo _obtenerInformacionGrupo;
        private readonly IObtenerPerfilesPorGrupo _obtenerPerfilesPorGrupo;

        public GrupoController(IAltaGrupo altaGrupo, IEditarGrupo editarGrupo, IBajaGrupo bajaGrupo, IObtenerInformacionGrupo obtenerInformacionGrupo, IObtenerPerfilesPorGrupo obtenerPerfilesPorGrupo)
        {
            _altaGrupo = altaGrupo;
            _editarGrupo = editarGrupo;
            _bajaGrupo = bajaGrupo;
            _obtenerInformacionGrupo = obtenerInformacionGrupo;
            _obtenerPerfilesPorGrupo = obtenerPerfilesPorGrupo;
        }
        /// <summary>
        /// Este endpoint permite registrar un nuevo grupo en el sistema.
        /// </summary>
        /// <returns>
        /// 201 Created: Si el grupo fue registrado correctamente.
        /// 400 Bad Request: Si los datos enviados son inválidos o faltan.
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>

        [HttpPost("alta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AltaGrupo([FromBody] GrupoAltaRequestDto grupoRequest)
        {

            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));
            
            var resultado = await _altaGrupo.EjecutarAsync(grupoRequest, profesorId);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);

            return Created();
           
        }
        /// <summary>
        /// Este endpoint permite editar los datos de un grupo existente.
        /// </summary>
        /// <returns>
        /// 200 OK: Si el grupo fue actualizado correctamente.
        /// 400 Bad Request: Si los datos enviados son inválidos o faltan.
        /// 404 Not Found: Si el grupo no existe.
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>
        [HttpPut("editar/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditarGrupo([FromBody] GrupoEditarDto grupoDto)
        {
            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));
            
            var resultado = await _editarGrupo.EjecutarAsync(grupoDto, profesorId);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            
            return NoContent();
            
        }
        /// <summary>
        /// Este endpoint permite eliminar un grupo existente.
        /// </summary>
        /// <returns>
        /// 200 OK: Si el grupo fue eliminado correctamente.
        /// 404 Not Found: Si el grupo no existe.
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>
        [HttpDelete("eliminar/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarGrupo(int id)
        {
            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));

            var resultado = await _bajaGrupo.EjecutarAsync(id, profesorId);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            
            //DELETE exitoso siempre debe devolver 204 NoContent
            return NoContent();
        }
        /// <summary>
        /// Devuelve la información básica de un grupo (sin tienda, alumnos, solicitudes, tablas de clasificación).
        /// </summary>
        /// <param name="grupoId">ID del grupo</param>
        /// <returns>
        /// 200 OK: Información parcial del grupo.
        /// 404 Not Found: Grupo no encontrado.
        /// 500 Internal Server Error: Error inesperado.
        /// </returns>
        [HttpGet("info/{grupoId:int}")]
        [ProducesResponseType(typeof(GrupoInformacionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerInformacionGrupo(int grupoId)
        {
            try
            {
                var resultado = await _obtenerInformacionGrupo.EjecutarAsync(grupoId);

                if (resultado.EsFallo)
                    return this.ManejarFallo(resultado);

                return Ok(resultado.Valor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Mensaje = "Ocurrió un error inesperado al obtener la información del grupo. " + ex.Message
                });
            }
        }
        /// <summary>
        /// Devuelve la lista de perfiles de estudiantes de un grupo.
        /// </summary>
        /// <param name="grupoId">Id del grupo</param>
        /// <returns>Lista de perfiles de estudiantes</returns>
        [HttpGet("{grupoId:int}/perfiles")]
        [ProducesResponseType(typeof(List<PerfilEstudianteInformacionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPerfilesPorGrupo(int grupoId)
        {
            try
            {
                // Aquí asumo que el método del caso de uso devuelve Resultado<List<PerfilEstudianteInformacionDto>>
                var resultado = await _obtenerPerfilesPorGrupo.EjecutarAsync(grupoId);

                if (resultado.EsFallo)
                    return this.ManejarFallo(resultado);

                return Ok(resultado.Valor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Mensaje = "Ocurrió un error inesperado al obtener los perfiles del grupo. " + ex.Message
                });
            }
        }
    }
}
