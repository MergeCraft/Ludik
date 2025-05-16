using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
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

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] UsuarioLoginPruebaDto usr)
        {
            try
            {
                if (usr == null)
                {
                    return BadRequest("Lo campos de NombreUsuario y contraseña con obligatorios.");
                }

                var usuario = _login.Ejecutar(usr.NombreUsuario, usr.Contrasenia);
                if (usuario == null)
                {
                    return BadRequest("No se encontró ningún usuario con los datos recibidos.");
                }

                string token = ManejadorJwt.ManejadorJwt.GenerarToken(usr.NombreUsuario, usuario.Rol);
                return Ok(new { Token = token, Rol = usuario.Rol, Email = usuario.NombreUsuario, UsuarioId = usuario.Id });
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
