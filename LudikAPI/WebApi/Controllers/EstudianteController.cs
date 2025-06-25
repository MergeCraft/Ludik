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
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaAplicacion.DTOs.RecompensaDTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IAltaEstudiante _altaEstudiante;
        private readonly ICrearSolicitudUnion _crearSolicitudUnion;
        private readonly IObtenerGruposDeEstudiante _obtenerGruposPorEstudiante;
        private readonly ICanjearRecompensa _canjearRecompensa;
        private readonly IObtenerRecompensasInventarioPerfil _obtenerRecompensasInventarioPerfil;

        public EstudianteController(IAltaEstudiante altaEstudiante,ICrearSolicitudUnion crearSolicitudUnion,IObtenerGruposDeEstudiante obtenerGruposPorEstudiante,ICanjearRecompensa canjearRecompensa,IObtenerRecompensasInventarioPerfil obtenerRecompensasInventarioPerfil)
        {
            _altaEstudiante = altaEstudiante;
            _crearSolicitudUnion = crearSolicitudUnion;
            _obtenerGruposPorEstudiante = obtenerGruposPorEstudiante;
            _canjearRecompensa = canjearRecompensa;
            _obtenerRecompensasInventarioPerfil = obtenerRecompensasInventarioPerfil;
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
        /// <summary>
        /// Un estudiante autenticado genera una solicitud de union para grupos .
        /// </summary>
        /// <returns>
        /// 200 OK: Solicitud creada correctamente.
        /// 401 Unauthorized: Si el usuario no está autenticado.
        /// 403 Forbidden: Si el usuario autenticado no tiene el rol "Estudiante".
        /// 404 Not Found: Si el estudiante (obtenido de los claims) no existe.
        /// 500 Internal Server Error: Si ocurre un error inesperado.
        /// </returns>
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
        /// <summary>
        /// Un estudiante autenticado canjea una recompensa .
        /// </summary>
        /// <returns>
        /// 200 OK: recompensa canjeada correctamente.
        /// 401 Unauthorized: Si el usuario no está autenticado.
        /// 403 Forbidden: Si el usuario autenticado no tiene el rol "Estudiante".
        /// 404 Not Found: Si el estudiante (obtenido de los claims) no existe.
        /// 500 Internal Server Error: Si ocurre un error inesperado.
        /// </returns>
        [HttpPost("perfiles/{perfilId}/recompensas/{recompensaId}/canjear")]
        [Authorize(Policy = "EsEstudiante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CanjearRecompensa(int perfilId, int recompensaId)
        {
            // Sin validaciones extra: se llama directamente al caso de uso
            var resultado = await _canjearRecompensa.EjecutarAsync(recompensaId, perfilId);
            if (resultado.EsExitoso)
                return Ok(new { message = "Recompensa canjeada correctamente." });
            return this.ManejarFallo(resultado);
        }
        /// <summary>
        /// Un estudiante autenticado obtiene las recompensas en su inventario para un perfil dado.
        /// </summary>
        /// <param name="perfilId">Identificador del perfil del estudiante.</param>
        /// <returns>
        /// 200 OK: Devuelve la lista de recompensas adquiridas.
        /// 401 Unauthorized: Si el usuario no está autenticado o no es Estudiante.
        /// 404 Not Found: Si no existe el perfil.
        /// 500 Internal Server Error: Si ocurre un error inesperado.
        /// </returns>
        [HttpGet("perfiles/{perfilId}/recompensas")]
        [Authorize(Policy = "EsEstudiante")]
        [ProducesResponseType(typeof(List<RecompensaListadoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerRecompensasInventario(int perfilId)
        {
            var resultado = await _obtenerRecompensasInventarioPerfil.EjecutarAsync(perfilId);
            if (resultado.EsExitoso)
                return Ok(resultado.Valor);
            return this.ManejarFallo(resultado);
        }
    }
}

