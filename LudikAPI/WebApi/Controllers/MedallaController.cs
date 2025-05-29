using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedallaController : ControllerBase
    {
        private IAltaMedalla _altaMedalla;

        public MedallaController(IAltaMedalla altaMedalla)
        {
            _altaMedalla = altaMedalla;
        }

        /// <summary>
        /// Este endpoint permite obtener todas las medallas que tiene el profesor y el sistema precargadas.
        /// </summary>
        /// <returns>
        /// 200 Ok: Si se pueden obtener correctamente las medallas, devuelve la lista.
        /// 401 Unauthorized: Si quien lo intenta hacer no es una persona autorizada (alguien que no sea un profesor).
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento. 
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Este endpoint permite obtener todas las medallas que tiene el profesor y el sistema precargadas.
        /// </summary>
        /// <returns>
        /// 200 Ok: Si existe la medalla, devuelve la medalla.
        /// 400 Bad Request: Si el id de la medalla es inavalido.
        /// 401 Unauthorized: Si quien lo intenta hacer no es una persona autorizada (alguien que no sea un profesor).
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento. 
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public string Get(int id)
        {
            return "value";
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

        // PUT api/<MedallaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MedallaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
