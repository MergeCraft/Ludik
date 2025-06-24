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
    public class PerfilImagenController : ControllerBase
    {
        private readonly IServicioGestionImagenPerfil _servicioGestionImagen;

        public PerfilImagenController(IServicioGestionImagenPerfil servicioGestionImagen)
        {
            _servicioGestionImagen = servicioGestionImagen;
        }

        [HttpPost("{idPerfilEstudiante}/imagen")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SubirImagen([FromRoute] int idPerfilEstudiante, IFormFile imagen)
        {
            //Todo: refactorizar para que se pueda utilizar de manera generica por un profesor o un estudiante. 
            // Actualmente solo se permite a estudiantes subir imagenes de perfil.
            if (imagen == null || imagen.Length == 0)
            {
                return BadRequest(new { Codigo = "Error.Validation", Mensaje = "No se ha proporcionado una imagen válida." });
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            await using var streamImagen = imagen.OpenReadStream();

            var resultado = await _servicioGestionImagen.SubirImagenDePerfilAsync(idPerfilEstudiante, userIdString, streamImagen);

            return resultado.EsExitoso
                ? Ok()
                : this.ManejarFallo(resultado);
        }

        [HttpGet("{idPerfilEstudiante}/imagen")]
        [ProducesResponseType(typeof(ImagenPerfilDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
