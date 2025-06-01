using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Jwt;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;
        private readonly IManejadorJwt _manejadorJwt;

        public LoginController(ILogin login, IManejadorJwt manejadorJwt)
        {
            _login = login;
            _manejadorJwt = manejadorJwt;
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
        public async Task<IActionResult> Login([FromBody] LoginSolicitudDto usr)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                var usuarioDto = await _login.Ejecutar(usr.NombreUsuario, usr.Contrasenia);

                string token = _manejadorJwt.GenerarToken(
                    usuarioDto.Id,
                    usuarioDto.NombreUsuario,
                    usuarioDto.Rol
                );

                var respuesta = new LoginRespuestaDto
                {
                    Token = token,
                    Rol = usuarioDto.Rol,
                    NombreUsuario = usuarioDto.NombreUsuario,
                    Id = usuarioDto.Id
                };

                return Ok(respuesta);
            }
            catch (UsuarioNoValidoException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (ContraseniaNoValidaException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Error = $"Ocurrió un error inesperado. {ex.Message}" });
            }
        }

    }

}
