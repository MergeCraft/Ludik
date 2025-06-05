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
using WebApi.Helpers;

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
        [Authorize(Roles = "Profesor")]
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
    }
}
