using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

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

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            

            return Ok(resultado.Valor);
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
            var resultado = await _obtenerMedallaPorId.EjecutarAsync(id);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            

            return Ok(resultado.Valor);
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
                Resultado resultado = await _altaMedalla.EjecutarAsync(medallaDto);
                if (resultado.EsFallo)
                    return this.ManejarFallo(resultado);

                return StatusCode(StatusCodes.Status201Created, "Medalla creada correctamente.");
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

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            

            return NoContent();
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
            Resultado resultado = await _bajaMedalla.EjecutarAsync(id);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            

            return NoContent();
        }
    }
}
