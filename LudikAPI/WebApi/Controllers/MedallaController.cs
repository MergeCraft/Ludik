using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "EsProfesor")]
    public class MedallaController : ControllerBase
    {
        private IAltaMedalla _altaMedalla;
        private readonly IObtenerTodasLasMedallas _obtenerTodasLasMedallas;
        private readonly IObtenerMedallaPorId _obtenerMedallaPorId;
        private readonly IModificarMedalla _modificarMedalla;
        private readonly IBajaMedalla _bajaMedalla;

        public MedallaController(
            IAltaMedalla altaMedalla,
            IObtenerTodasLasMedallas obtenerTodasLasMedallas,
            IObtenerMedallaPorId obtenerMedallaPorId,
            IModificarMedalla modificarMedalla,
            IBajaMedalla bajaMedalla)
        {
            _altaMedalla = altaMedalla;
            _obtenerTodasLasMedallas = obtenerTodasLasMedallas;
            _obtenerMedallaPorId = obtenerMedallaPorId;
            _modificarMedalla = modificarMedalla;
            _bajaMedalla = bajaMedalla;
        }

        /// <summary>
        /// Este endpoint permite obtener todas las medallas que tiene el profesor y el sistema precargadas.
        /// </summary>
        /// <response code="200">Devuelve la lista de medallas.</response>
        /// <response code="401">No autorizado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MedallaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            Resultado<IEnumerable<MedallaDto>> resultado = await _obtenerTodasLasMedallas.EjecutarAsync();

            if (resultado.EsExitoso)
            {
                return Ok(resultado.Valor);
            }

            // Si EsFallo, asumimos que es un error inesperado del servicio, ya que un GET de todos
            // no suele tener errores de "negocio" más allá de fallos del sistema.
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Error al obtener las medallas.",
                Detail = string.Join(", ", resultado.Errores.Select(e => e.Mensaje))
            };
            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }

        /// <summary>
        /// Obtiene una medalla específica por su ID.
        /// </summary>
        /// <param name="id">ID de la medalla a obtener.</param>
        /// <response code="200">Devuelve la medalla solicitada.</response>
        /// <response code="400">ID de medalla inválido (ej. no positivo).</response>
        /// <response code="401">No autorizado.</response>
        /// <response code="404">Medalla no encontrada.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet("{id:int}")] // Restricción de tipo para el ID
        [ProducesResponseType(typeof(MedallaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ProblemDetails { Title = "ID de medalla inválido.", Status = StatusCodes.Status400BadRequest });
            }

            Resultado<MedallaDto> resultado = await _obtenerMedallaPorId.EjecutarAsync(id);

            if (resultado.EsExitoso)
            {
                return Ok(resultado.Valor);
            }


            if (resultado.EsFallo) // Asumimos que si falla aquí es porque no se encontró o hubo otro error.
            {
                return NotFound(new ProblemDetails { Title = "Medalla no encontrada o error al obtenerla.", Status = StatusCodes.Status404NotFound, Detail = string.Join(", ", resultado.Errores.Select(e => e.Mensaje)) });
            }

            // Fallback para otros errores no manejados explícitamente.
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails { Title = "Error interno del servidor.", Status = StatusCodes.Status500InternalServerError });
        }

        /// <summary>
        /// Este endpoint permite a un profesor crear una medalla.
        /// </summary>
        /// <returns>
        /// 201 Created: Si la medalla se creo correctamente.
        /// 400 Bad Request: Si faltan datos o alguno es inválido.
        /// 401 Unauthorized: Si quien lo intenta hacer no es una persona autorizada (alguien que no sea un profesor).
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>
        [HttpPost("alta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] MedallaAltaDto medallaDto)
        {
            try
            {
                await _altaMedalla.EjecutarAsync(medallaDto);
                return StatusCode(StatusCodes.Status201Created, "Medalla creada correctamente.");
            }
            catch (MedallaNoValidaException mException)
            {
                return BadRequest(new { Error = mException.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Error = "Ocurrió un error. " + e.Message });
            }
        }

        /// <summary>
        /// Modifica una medalla existente.
        /// </summary>
        /// <param name="id">ID de la medalla a modificar.</param>
        /// <param name="medallaDto">Datos de la medalla para modificar.</param>
        /// <response code="204">Medalla modificada correctamente.</response>
        /// <response code="400">ID inválido o datos inválidos.</response>
        /// <response code="401">No autorizado.</response>
        /// <response code="404">Medalla no encontrada.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] MedallaAltaDto medallaDto)
        {

            Resultado resultado = await _modificarMedalla.EjecutarAsync(medallaDto);

            if (resultado.EsExitoso)
            {
                return NoContent();
            }

            var primerError = resultado.Errores.FirstOrDefault();
            if (primerError != null && primerError.Codigo.Contains("NoEncontrada")) // Ejemplo de convención
            {
                return NotFound(new ProblemDetails { Title = "Medalla no encontrada para modificar.", Status = StatusCodes.Status404NotFound, Detail = string.Join(", ", resultado.Errores.Select(e => e.Mensaje)) });
            }

            return BadRequest(new ProblemDetails { Title = "Error al modificar la medalla.", Status = StatusCodes.Status400BadRequest, Detail = string.Join(", ", resultado.Errores.Select(e => e.Mensaje)) });
        }


        /// <summary>
        /// Elimina una medalla por su ID.
        /// </summary>
        /// <param name="id">ID de la medalla a eliminar.</param>
        /// <response code="204">Medalla eliminada correctamente.</response>
        /// <response code="400">ID inválido.</response>
        /// <response code="401">No autorizado.</response>
        /// <response code="404">Medalla no encontrada.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ProblemDetails { Title = "ID de medalla inválido.", Status = StatusCodes.Status400BadRequest });
            }

            Resultado resultado = await _bajaMedalla.EjecutarAsync(id);

            if (resultado.EsExitoso)
            {
                return NoContent();
            }

            var primerError = resultado.Errores.FirstOrDefault();
            if (primerError != null && primerError.Codigo.Contains("NoEncontrada")) // Ejemplo de convención
            {
                return NotFound(new ProblemDetails { Title = "Medalla no encontrada para eliminar.", Status = StatusCodes.Status404NotFound, Detail = string.Join(", ", resultado.Errores.Select(e => e.Mensaje)) });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails { Title = "Error al eliminar la medalla.", Status = StatusCodes.Status500InternalServerError, Detail = string.Join(", ", resultado.Errores.Select(e => e.Mensaje)) });
        }
    }
}
