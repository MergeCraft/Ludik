using LogicaAplicacion.InterfacesCasosUsos.AsignacionMedalla;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LogicaAplicacion.DTOs.MedallaDTOs;
using WebApi.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "EsProfesor")]
    public class AsignacionMedallasController : ControllerBase
    {
        private readonly IAsignarMedalla _asignarMedalla;

        public AsignacionMedallasController(IAsignarMedalla asignarMedalla)
        {
            _asignarMedalla = asignarMedalla;
        }

        /// <summary>
        /// Asigna una medalla existente a un perfil de estudiante.
        /// </summary>
        /// <param name="idPerfilEstudiante">El ID del perfil del estudiante que recibirá la medalla.</param>
        /// <param name="request">DTO que contiene el ID de la medalla a asignar.</param>
        /// <returns>Un resultado de la operación.</returns>
        [HttpPost("perfil-estudiante/{idPerfilEstudiante}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AsignarMedalla([FromRoute] int idPerfilEstudiante, [FromBody] MedallaDto medallaDto)
        {

            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(profesorId))
                return Unauthorized();
            
            var resultado = await _asignarMedalla.EjecutarAsync(profesorId, idPerfilEstudiante, medallaDto);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            

            return Ok(new { Mensaje = "Medalla asignada exitosamente." });
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
