using LogicaAplicacion.InterfacesCasosUsos.RecuperarContrasena;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Mvc;
using WebApi.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet]
        /// <summary>
        /// Obtener las preguntas de seguridad que ha respondido el estudiante al momento de registrase.
        /// </summary>
        /// <response code="200">Devuelve la lista de preguntas.</response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public string ObtenerPreguntasDeSeguridadPorNombreUsuario([FromBody] string nombreUsuario)
        {
            Resultado resultado = _obtenerPreguntasDeSegurididadPorNombreUsuario.EjecutarAsync(nombreUsuario);
            return resultado.EsFallo ? ManejadorJwt(resultado)
                : resultado.Valor;
        }

        [HttpPut("{id}")]
        /// <summary>
        /// Restablecer la contraseña de un estudiante.
        /// </summary>
        /// <response code="204">La contraseña fue restablecida con éxito.</response>
        /// <response code="401"></response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public void RestablecerContrasena(int id, [FromBody] string value)
        {
        }

    }
}
