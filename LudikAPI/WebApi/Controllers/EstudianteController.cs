using LogicaAplicacion.DTOs.SolocitudUnionDTOs;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaAplicacion.InterfacesCasosUsos.SolicitudUnion;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.Resultados;
using LogicaAplicacion.DTOs.GrupoDTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IAltaEstudiante _altaEstudiante;
        private readonly ICrearSolicitudUnion _crearSolicitudUnion;
        private readonly IObtenerGruposDeEstudiante _obtenerGruposPorEstudiante;

        public EstudianteController(IAltaEstudiante altaEstudiante,ICrearSolicitudUnion crearSolicitudUnion,IObtenerGruposDeEstudiante obtenerGruposPorEstudiante)
        {
            _altaEstudiante = altaEstudiante;
            _crearSolicitudUnion = crearSolicitudUnion;
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
            Resultado resultado = await _altaEstudiante.EjecutarAsync(estudianteDto);

            return resultado.EsExitoso
                ? StatusCode(StatusCodes.Status201Created)
                : this.ManejarFallo(resultado);
            // StatusCode(StatusCodes.Status201Created, "Estudiante registrado correctamente.")
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
        [Authorize(Policy = "EsEstudiante")]
        [ProducesResponseType(typeof(List<GrupoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerGruposDeEstudiante()
        {
            var idEstudianteAutenticado = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Resultado<List<GrupoDto>> resultado = await _obtenerGruposPorEstudiante.EjecutarAsync(idEstudianteAutenticado);

            return resultado.EsExitoso
                ? Ok(resultado.Valor)
                : this.ManejarFallo(resultado);

        }
        [HttpPost("unirse-grupo")]
        [Authorize(Policy = "EsEstudiante")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)] // si el código no existe
        [ProducesResponseType(typeof(object), StatusCodes.Status409Conflict)] // si el estudiante ya es miembro
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UnirseAGrupo([FromQuery] string codigo)
        {
            var estudianteId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            var solicitudDto = new SolicitudUnionDto
            {
                IdEstudiante = estudianteId,
                CodigoEnlace = codigo
            };

            Resultado resultado = await _crearSolicitudUnion.EjecutarAsync(solicitudDto);

            return resultado.EsExitoso
                ? StatusCode(StatusCodes.Status201Created)
                : this.ManejarFallo(resultado);
            
        }
    }
}

