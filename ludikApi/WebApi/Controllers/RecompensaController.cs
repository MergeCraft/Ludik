using LogicaAplicacion.DTOs.GrupoDTOs;
using System.Security.Claims;
using LogicaAplicacion.InterfacesCasosUsos.Recompensa;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using Microsoft.AspNetCore.Authorization;
using LogicaAplicacion.ImplementacionCasosUsos.Recompensa;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "EsProfesor")]
    public class RecompensaController : ControllerBase
    {
        private readonly IAltaRecompensa _altaRecompensa;
        private readonly IEditarRecompensa _editarRecompensa;
        private readonly IBajaRecompensa _bajaRecompensa;

        public RecompensaController(IAltaRecompensa altaRecompensa,IEditarRecompensa editarRecompensa,IBajaRecompensa bajaRecompensa)
        {
            _altaRecompensa = altaRecompensa;
            _editarRecompensa = editarRecompensa;
            _bajaRecompensa = bajaRecompensa;
        }
    
    /// <summary>
    /// Este endpoint permite a un profesor crear una recompensa.
    /// </summary>
    /// <returns>
    /// 201 Created: Si la Recompensa se creo correctamente.
    /// 400 Bad Request: Si faltan datos o alguno es inválido.
    /// 401 Unauthorized: Si quien lo intenta hacer no es una persona autorizada (alguien que no sea un profesor).
    /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
    /// </returns>
        [HttpPost("alta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AltaRecompensa([FromBody] RecompensaAltaDto recompensaRequest)
        {

            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));

            var resultado = await _altaRecompensa.EjecutarAsync(recompensaRequest,profesorId);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);

            return StatusCode(StatusCodes.Status201Created, "La Recompensa ha sido creado correctamente.");

        }
        /// <summary>
        /// Este endpoint permite a un profesor editar una recompensa.
        /// </summary>
        /// <returns>
        /// 201 Created: Si la Recompensa se edito correctamente.
        /// 400 Bad Request: Si faltan datos o alguno es inválido.
        /// 401 Unauthorized: Si quien lo intenta hacer no es una persona autorizada (alguien que no sea un profesor).
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>
        [HttpPut("{recompensaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditarRecompensa([FromRoute] string recompensaId,[FromBody] RecompensaEditarDto dto)
        {
            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));

            var resultado = await _editarRecompensa.EjecutarAsync(recompensaId, dto, profesorId);
            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);

            return Ok("Recompensa actualizada correctamente.");
        }
        /// <summary>
        /// Este endpoint permite a un profesor eliminar una recompensa.
        /// </summary>
        /// <returns>
        /// 201 Created: Si la Recompensa se elimino correctamente.
        /// 400 Bad Request: Si faltan datos o alguno es inválido.
        /// 401 Unauthorized: Si quien lo intenta hacer no es una persona autorizada (alguien que no sea un profesor).
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>
        [HttpDelete("{recompensaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BajaRecompensa([FromRoute] string recompensaId)
        {
            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(new Error("Error.Unauthorized", "No autenticado.")));

            var resultado = await _bajaRecompensa.EjecutarAsync(recompensaId, profesorId);
            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);

            return Ok("Recompensa eliminada correctamente.");
        }
    }
}
