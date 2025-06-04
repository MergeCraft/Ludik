using LogicaAplicacion.DTOs.SolocitudUnionDTOs;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaAplicacion.InterfacesCasosUsos.SolicitudUnion;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IAltaEstudiante _altaEstudiante;
        private readonly ICrearSolicitudUnion _crearSolicitudUnion;

        public EstudianteController(IAltaEstudiante altaEstudiante,ICrearSolicitudUnion crearSolicitudUnion)
        {
            _altaEstudiante = altaEstudiante;
            _crearSolicitudUnion = crearSolicitudUnion;
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
        [HttpGet("unirse-grupo", Name = "UnirseAGrupo")]
        [Authorize(Roles = "Estudiante")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UnirseAGrupo([FromQuery] string codigo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codigo))
                    return BadRequest("El código del enlace es obligatorio.");

                string estudianteId = User.FindFirst("id")?.Value;

                if (string.IsNullOrEmpty(estudianteId))
                    return Unauthorized("No se pudo obtener el ID del estudiante desde el token.");

                var solicitudDto = new SolicitudUnionDto
                {
                    IdEstudiante = estudianteId,
                    CodigoEnlace = codigo
                };

                await _crearSolicitudUnion.EjecutarAsync(solicitudDto);

                return StatusCode(StatusCodes.Status201Created, "Solicitud de unión generada correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Error = "Ocurrió un error inesperado. " + ex.Message });
            }
        }
    }
}
