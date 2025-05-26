using Azure.Core;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
using LogicaNegocio.Excepciones;
using LogicaNegocio.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;

        public LoginController(ILogin login)
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
        public async Task<IActionResult> Login([FromBody] LoginSolicitudDto usr)
        {
            if (usr == null)
                return BadRequest("Lo campos de NombreUsuario y contraseña con obligatorios.");

            try
            {

                var usuarioDto = await _login.Ejecutar(usr.NombreUsuario, usr.Contrasenia);

                if (usuarioDto == null)
                    return Unauthorized("Credenciales incorrectas.");


                string token = ManejadorJwt.ManejadorJwt.GenerarToken(usuarioDto.NombreUsuario, usuarioDto.Rol);

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
                // 401 porque el usuario no existe
                return Unauthorized(new { Error = ex.Message });
            }
            catch (ContraseniaNoValidaException ex)
            {
                // 401 porque la contraseña no coincide
                return Unauthorized(new { Error = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                // 400 si faltó algún parámetro en la entrada
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                // 500 para cualquier otro error inesperado
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Error = "Ocurrió un error inesperado. " + ex.Message });
            }
        }

    }

}
