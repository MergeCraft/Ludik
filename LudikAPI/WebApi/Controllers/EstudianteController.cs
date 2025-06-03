using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaNegocio.Excepciones;
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

        public EstudianteController(IAltaEstudiante altaEstudiante)
        {
            _altaEstudiante = altaEstudiante;
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
        public async Task<IActionResult> UnirseAGrupo([FromQuery] string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return BadRequest("El código del enlace es obligatorio.");

            return Ok("Solicitud de unión generada correctamente.");
        }
    }
}
