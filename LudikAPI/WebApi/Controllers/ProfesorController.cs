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

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly  IAltaProfesor _altaProfesor;
        private readonly IObtenerGruposDeProfesor _obtenerGruposDeProfesor;
        public ProfesorController(IAltaProfesor altaProfesor, IObtenerGruposDeProfesor obtenerGruposDeProfesor)
        {
            _altaProfesor = altaProfesor;
            _obtenerGruposDeProfesor = obtenerGruposDeProfesor;
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
        [Authorize(Roles = "Profesor")]
        [ProducesResponseType(typeof(List<GrupoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerGruposProfesor()
        {
            try
            {
                var idProfesorAutenticado = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(idProfesorAutenticado))
                    return Unauthorized(new { Mensaje = "No se pudo identificar al usuario autenticado." });


                Resultado<List<GrupoDto>> resultado = await _obtenerGruposDeProfesor.EjecutarAsync(idProfesorAutenticado);

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
