using LogicaAplicacion.DTOs.ProfesorDTOs;
using System.ComponentModel.DataAnnotations;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.DTOs.GrupoDTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private readonly IAltaGrupo _altaGrupo;
        private readonly IEditarGrupo _editarGrupo;
        public GrupoController(IAltaGrupo altaGrupo, IEditarGrupo editarGrupo)
        {
            _altaGrupo = altaGrupo;
            _editarGrupo = editarGrupo;
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
        public async Task<IActionResult> AltaGrupo([FromBody] GrupoAltaDto grupoDto)
        {
            try
            {
                if (grupoDto == null)
                    return BadRequest("Debe enviar los datos del grupo.");

                await _altaGrupo.EjecutarAsync(grupoDto);

                return StatusCode(StatusCodes.Status201Created, "Grupo registrado correctamente.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Ocurrió un error inesperado. " + ex.Message });
            }
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
        [HttpPut("editar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditarGrupo([FromBody] GrupoEditarDto grupoDto)
        {
            try
            {
                if (grupoDto == null)
                    return BadRequest("Debe enviar los datos del grupo a editar.");

                await _editarGrupo.EjecutarAsync(grupoDto);

                return Ok("Grupo actualizado correctamente.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Ocurrió un error inesperado. " + ex.Message });
            }
        }
    }
}
