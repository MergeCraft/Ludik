using LogicaAplicacion.DTOs.KudoDTOs;
using LogicaAplicacion.DTOs.PerfilEstudianteDTO;
using LogicaAplicacion.InterfacesCasosUsos.Kudo;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "EsEstudiante")]
    public class KudoController : ControllerBase
    {

        private readonly IAsignarKudo _asignarKudo;

        public KudoController(IAsignarKudo asignarKudo)
        {
            _asignarKudo = asignarKudo;
        }

        /// <summary>
        /// Permite asignarle un kudo a un estudiante.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AsignarKudo([FromBody] AsignarKudoDto kudoDto)
        {
            var estudianteId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(estudianteId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));

            var resultado = await _asignarKudo.EjecutarAsync(kudoDto);

            return resultado.EsExitoso ? Ok(resultado) : this.ManejarFallo(resultado);

            
        }
    }
}
