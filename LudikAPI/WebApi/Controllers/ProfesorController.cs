using LogicaAplicacion.DTOs.UsuarioDTOs;
using System.ComponentModel.DataAnnotations;
using LogicaAplicacion.InterfacesCasosUsos.Profesor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.DTOs.ProfesorDTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly  IAltaProfesor _altaProfesor;
        public ProfesorController(IAltaProfesor altaProfesor)
        {
            _altaProfesor = altaProfesor;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AltaProfesor([FromBody] ProfesorAltaDto profesorDto)
        {
            if (profesorDto == null)
                return BadRequest("Debe enviar los datos del profesor.");

            // Verificar las DataAnnotations en el DTO
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _altaProfesor.EjecutarAsync(profesorDto);
                if (!resultado.Succeeded)
                {
                    // Agregar cada error de IdentityResult a ModelState y retornar BadRequest
                    foreach (var error in resultado.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    
                    return BadRequest(ModelState);
                }

                return StatusCode(StatusCodes.Status201Created, "Profesor registrado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Ocurrió un error inesperado. " + ex.Message });
            }
        }
    }
}
