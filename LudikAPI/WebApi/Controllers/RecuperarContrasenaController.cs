using LogicaAplicacion.DTOs.PreguntasDeSeguridadDTOs;
using LogicaAplicacion.DTOs.RestablecerContrasenaDTO;
using LogicaAplicacion.InterfacesCasosUsos.RecuperarContrasena;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Jwt;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecuperarContrasenaController : ControllerBase
    {
        private readonly IObtenerPreguntasDeSegurididadPorNombreUsuario _obtenerPreguntasDeSegurididadPorNombreUsuario;
        private readonly IRestablecerContrasena _restablecerContrasena;

        public RecuperarContrasenaController(
            IObtenerPreguntasDeSegurididadPorNombreUsuario obteeDeSegurididadPorNombreUsuario,
            IRestablecerContrasena restablecerContrasena)
        {
            _obtenerPreguntasDeSegurididadPorNombreUsuario = obteeDeSegurididadPorNombreUsuario;
            _restablecerContrasena = restablecerContrasena;
        }


        /// <summary>
        /// Obtener las preguntas de seguridad que ha respondido el estudiante al momento de registrase.
        /// </summary>
        /// <response code="200">Devuelve la lista de preguntas.</response>
        /// <response code="404"></response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet("preguntas/{nombreUsuario}")]
        [ProducesResponseType(typeof(PreguntasDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPreguntasDeSeguridadPorNombreUsuario(string nombreUsuario)
        {
            var resultado = await _obtenerPreguntasDeSegurididadPorNombreUsuario.EjecutarAsync(nombreUsuario);


            return resultado.EsExitoso ? Ok(resultado.Valor) : this.ManejarFallo(resultado);
        }


        /// <summary>
        /// Verifica las respuestas de seguridad y restablece la contraseña de un estudiante.
        /// </summary>
        /// <response code="204">La contraseña fue restablecida con éxito.</response>
        /// <response code="400">Datos inválidos o la contraseña no cumple las políticas.</response>
        /// <response code="401">Las respuestas de seguridad son incorrectas.</response>
        /// <response code="404">Usuario no encontrado.</response>
        [HttpPost("restablecer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RestablecerContrasena([FromBody] InformacionParaRestablecerContrasenaDto dto)
        {
            var resultado = await _restablecerContrasena.EjecutarAsync(dto);

            if (resultado.EsFallo)
            {
                return this.ManejarFallo(resultado);
            }

            return StatusCode(StatusCodes.Status204NoContent,"Se cambio la contraseña con éxito.");
        }


    }
}
