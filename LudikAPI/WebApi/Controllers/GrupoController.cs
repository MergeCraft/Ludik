using LogicaAplicacion.DTOs.ProfesorDTOs;
using System.ComponentModel.DataAnnotations;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using LogicaNegocio.Resultados;
using System.Security.Claims;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "EsProfesor")]
    public class GrupoController : ControllerBase
    {
        private readonly IAltaGrupo _altaGrupo;
        private readonly IEditarGrupo _editarGrupo;
        private readonly IBajaGrupo _bajaGrupo;

        public GrupoController(IAltaGrupo altaGrupo, IEditarGrupo editarGrupo, IBajaGrupo bajaGrupo)
        {
            _altaGrupo = altaGrupo;
            _editarGrupo = editarGrupo;
            _bajaGrupo = bajaGrupo;

        }
        /// <summary>
        /// Este endpoint permite registrar un nuevo grupo en el sistema.
        /// </summary>
        /// <returns>
        /// 201 Created: Si el grupo fue registrado correctamente.
        /// 400 Bad Request: Si los datos enviados son inválidos o faltan.
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>

        [HttpPost("alta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AltaGrupo([FromBody] GrupoAltaRequestDto grupoRequest)
        {

            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));
            
            var resultado = await _altaGrupo.EjecutarAsync(grupoRequest, profesorId);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);

            return Created();
           
        }
        /// <summary>
        /// Este endpoint permite editar los datos de un grupo existente.
        /// </summary>
        /// <returns>
        /// 200 OK: Si el grupo fue actualizado correctamente.
        /// 400 Bad Request: Si los datos enviados son inválidos o faltan.
        /// 404 Not Found: Si el grupo no existe.
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>
        [HttpPut("editar/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditarGrupo([FromBody] GrupoEditarDto grupoDto)
        {
            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));
            
            var resultado = await _editarGrupo.EjecutarAsync(grupoDto, profesorId);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            
            return NoContent();
            
        }
        /// <summary>
        /// Este endpoint permite eliminar un grupo existente.
        /// </summary>
        /// <returns>
        /// 200 OK: Si el grupo fue eliminado correctamente.
        /// 404 Not Found: Si el grupo no existe.
        /// 500 Internal Server Error: Si ocurre un error inesperado durante el procesamiento.
        /// </returns>
        [HttpDelete("eliminar/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarGrupo(int id)
        {
            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));

            var resultado = await _bajaGrupo.EjecutarAsync(id, profesorId);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            
            //DELETE exitoso siempre debe devolver 204 NoContent
            return NoContent();
        }
    }
}
