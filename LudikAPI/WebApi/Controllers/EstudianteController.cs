using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.Resultados;
using LogicaAplicacion.DTOs.GrupoDTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IAltaEstudiante _altaEstudiante;
        private readonly IObtenerGruposDeEstudiante _obtenerGruposPorEstudiante;

        public EstudianteController(IAltaEstudiante altaEstudiante, IObtenerGruposDeEstudiante obtenerGruposPorEstudiante)
        {
            _altaEstudiante = altaEstudiante;
            _obtenerGruposPorEstudiante = obtenerGruposPorEstudiante;
        }

        /// <summary>
        /// Este endpoint permite registrar un nuevo estudiante en el sistema.
        /// </summary>
        /// <returns>
        /// 201 Created: Si el estudiante fue registrado correctamente.
        /// 400 Bad Request: Si los datos enviados son inválidos o faltan.
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>

        [HttpPost("alta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AltaEstudiante([FromBody] EstudianteAltaDto estudianteDto)
        {
            try
            {
                if (estudianteDto == null)
                    return BadRequest("Debe enviar los datos del estudiante.");

                await _altaEstudiante.EjecutarAsync(estudianteDto);

                return StatusCode(StatusCodes.Status201Created, "Estudiante registrado correctamente.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (UsuarioNoValidoException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Error = "Ocurrió un error inesperado. " + ex.Message });
            }
        }

        /// <summary>
        /// Un estudiante autenticado obtiene todos los grupos a los que pertenece.
        /// </summary>
        /// <returns>
        /// 200 OK: Retorna la lista de grupos del estudiante.
        /// 401 Unauthorized: Si el usuario no está autenticado.
        /// 403 Forbidden: Si el usuario autenticado no tiene el rol "Estudiante".
        /// 404 Not Found: Si el estudiante (obtenido de los claims) no existe.
        /// 500 Internal Server Error: Si ocurre un error inesperado.
        /// </returns>
        [HttpGet("mis-grupos")]
        [Authorize(Roles = "Estudiante")]
        [ProducesResponseType(typeof(List<GrupoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerGruposDeEstudiante()
        {
            try
            {
                var idEstudianteAutenticado = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                if (string.IsNullOrEmpty(idEstudianteAutenticado))
                    return Unauthorized(new { Mensaje = "No se pudo identificar al usuario autenticado." });
                

                Resultado<List<GrupoDto>> resultado = await _obtenerGruposPorEstudiante.EjecutarAsync(idEstudianteAutenticado);

                if (resultado.EsFallo)
                {
                    if (resultado.Errores.Any(e => e.Codigo == Error.NotFound.Codigo))
                    {
                        return NotFound(resultado.Errores.ToList());
                    }
                    return BadRequest(resultado.Errores.ToList());
                }

                return Ok(resultado.Valor);
            }
            catch (Exception ex)
            {
                // IMPORTANTE: En producción, no exponer ex.Message directamente.
                // Loguear ex.ToString() para tener todos los detalles internamente.
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Mensaje = "Ocurrió un error inesperado al obtener los grupos del estudiante. " + ex.Message });
            }
        }
    }
}

