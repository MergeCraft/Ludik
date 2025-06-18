using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LogicaAplicacion.DTOs.ImagenPerfilDtos;
using LogicaAplicacion.InterfacesCasosUsos.Imagenes;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        private readonly IServicioGestionImagenPerfil _servicioGestionImagen;

        public ImagenController(IServicioGestionImagenPerfil servicioGestionImagen)
        {
            _servicioGestionImagen = servicioGestionImagen;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)] // Devolver las nuevas URLs es más útil que NoContent.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SubirImagen([FromRoute] int idPerfilEstudiante, IFormFile imagen)
        {
            if (imagen == null || imagen.Length == 0)
            {
                return BadRequest(new { Codigo = "Error.Validation", Mensaje = "No se ha proporcionado una imagen válida." });
            }

            // Extraer el ID del usuario desde los claims del token JWT.
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            var resultado = await _servicioGestionImagen.SubirImagenDePerfilAsync(idPerfilEstudiante, userIdString, imagen);

            return resultado.EsExitoso
                ? Ok()
                : this.ManejarFallo(resultado);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ImagenPerfilDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObtenerUrlImagen([FromRoute] int idPerfilEstudiante)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            var resultadoDto = await _servicioGestionImagen.ObtenerUrlImagenPerfilAsync(idPerfilEstudiante, userIdString);

            return resultadoDto.EsExitoso
                ? Ok(resultadoDto.Valor)
                : this.ManejarFallo(resultadoDto);
        }
    }
}
