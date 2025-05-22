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
        public GrupoController(IAltaGrupo altaGrupo)
        {
            _altaGrupo = altaGrupo;
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
        public IActionResult AltaGrupo([FromBody] GrupoAltaDto GrupoDto)
        {
            try
            {
                if (GrupoDto == null)
                {
                    return BadRequest("Debe enviar los datos del grupo.");
                }

                _altaGrupo.Ejecutar(GrupoDto);

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
    }
}
