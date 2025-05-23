using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
using LogicaNegocio.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private ILogin _login;

        public UsuarioController(ILogin login)
        {
            _login = login;
        }
        /// <summary>
        /// Este endpoint permite que un usuario se autentifique en el sistema.
        /// </summary>
        /// <returns>
        /// 200 Ok: Si el usuario fue logueado correctamente devuelve una token.
        /// 400 Bad Request: Si los datos enviados son inválidos o faltan.
        /// 401 Unauthorized: Si las credenciales son incorrectas.
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UsuarioDto usr)
        {
            if (usr == null)
                return BadRequest("Lo campos de NombreUsuario y contraseña con obligatorios.");

            try
            {
                //TODO: refactorizar para hacer uso de polimorfismo en lugar de preguntarle el tipo de usuario o considerar ponerle un nombreTipo
                var usuarioDto = await _login.Ejecutar(usr.NombreUsuario, usr.Contrasenia);

                string token = ManejadorJwt.ManejadorJwt.GenerarToken(usuarioDto.NombreUsuario, usuarioDto.Rol);

                return Ok(new
                {
                    Token = token,
                    Rol = usuarioDto.Rol,
                    NombreUsuario = usuarioDto.NombreUsuario,
                    UsuarioId = usuarioDto.Id
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = "Credenciales incorrectas. " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Ocurrió un error inesperado. " + ex.Message });
            }
        }
    }
}
