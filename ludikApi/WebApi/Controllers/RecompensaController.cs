using LogicaAplicacion.DTOs.GrupoDTOs;
using System.Security.Claims;
using LogicaAplicacion.InterfacesCasosUsos.Recompensa;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "EsProfesor")]
    public class RecompensaController : ControllerBase
    {
        private readonly IAltaRecompensa _altaRecompensa;

        public RecompensaController(IAltaRecompensa altaRecompensa)
        {
            _altaRecompensa = altaRecompensa;
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
        [HttpPost("alta/{tiendaId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AltaRecompensa([FromBody] RecompensaAltaDto recompensaRequest,string tiendaId)
        {

            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));

            var resultado = await _altaRecompensa.EjecutarAsync(recompensaRequest, tiendaId,profesorId);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);

            return StatusCode(StatusCodes.Status201Created, "La Recompensa ha sido creado correctamente.");

        }
    }
}
