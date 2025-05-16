using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IAlta _altaEstudiante;

        public EstudianteController(IAlta altaEstudiante)
        {
            _altaEstudiante = altaEstudiante;
        }

        [HttpPost("alta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AltaEstudiante([FromBody] EstudianteAltaDto estudianteDto)
        {
            try
            {
                if (estudianteDto == null)
                {
                    return BadRequest("Debe enviar los datos del estudiante.");
                }

                _altaEstudiante.Ejecutar(estudianteDto);

                return StatusCode(StatusCodes.Status201Created, "Estudiante registrado correctamente.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Ocurrió un error inesperado. " + ex.Message });
            }
        }
    }
}
