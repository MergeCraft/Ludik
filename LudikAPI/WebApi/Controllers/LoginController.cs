using Dominio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Jwt;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IManejadorJwt _manejadorJwt;
        private readonly UserManager<Usuario> _userManager; 
        private readonly SignInManager<Usuario> _signInManager;

        public LoginController(
            IManejadorJwt manejadorJwt,
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager)
        {
            _manejadorJwt = manejadorJwt;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Este endpoint permite que un usuario se autentifique en el sistema.
        /// </summary>
        /// <returns>
        /// 200 Ok: Si el usuario fue logueado correctamente devuelve un token y datos del usuario.
        /// 400 Bad Request: Si los datos enviados son inválidos o faltan, o si la cuenta requiere acciones adicionales.
        /// 401 Unauthorized: Si las credenciales son incorrectas o la cuenta está bloqueada.
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginRespuestaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginSolicitudDto loginSolicitud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var result = await _signInManager.PasswordSignInAsync(
                    loginSolicitud.NombreUsuario,
                    loginSolicitud.Contrasenia,
                    isPersistent: false,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var usuario = await _userManager.FindByNameAsync(loginSolicitud.NombreUsuario);
                    if (usuario == null)
                    {
                        // Esto no debería pasar si PasswordSignInAsync tuvo éxito, pero es una salvaguarda.
                        return Unauthorized(new { Mensaje = "Error al obtener los detalles del usuario." });
                    }

                    var roles = await _userManager.GetRolesAsync(usuario);
                    string rolUnico = roles.FirstOrDefault();

                    string token = _manejadorJwt.GenerarToken(
                        usuario.Id,
                        usuario.UserName,
                        rolUnico
                    );

                    var respuesta = new LoginRespuestaDto
                    {
                        Token = token,
                        Rol = rolUnico, 
                        NombreUsuario = usuario.UserName,
                        Id = usuario.Id
                    };

                    return Ok(respuesta);
                }

                if (result.IsLockedOut)
                    return Unauthorized(new { Mensaje = "Cuenta bloqueada. Intente más tarde." });
                

                // Si ninguna de las anteriores, es credenciales incorrectas.
                return Unauthorized(new { Mensaje = "Nombre de usuario o contraseña incorrectos." });

            }
            catch (ArgumentNullException ex)
            {
                // Log ex
                return BadRequest(new { Mensaje = "Los datos de la solicitud no pueden ser nulos.", Detalle = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Mensaje = $"Ocurrió un error inesperado durante el inicio de sesión.", Detalle = ex.Message });
            }
        }
    }

}


