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

            var profesorId = User.FindFirstValue("id");
            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));
            
            var resultado = await _altaGrupo.EjecutarAsync(grupoRequest, profesorId);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);

            return Created();
            /*
            try
            {
                if (grupoRequest == null)
                    return BadRequest("Debe enviar los datos del grupo.");
                
                string profesorId = User.FindFirst("id")?.Value;

                if (string.IsNullOrEmpty(profesorId))
                    return Unauthorized("No se pudo determinar el ID del profesor desde el token.");

                string codigo = Guid.NewGuid().ToString("N");
                string url = _linkGenerator.GetUriByAction(
                    HttpContext,
                    action: "UnirseAGrupo",
                    controller: "Estudiante",
                    values: new { codigo });

                var grupoDto = new GrupoAltaDto
                {
                    Nombre = grupoRequest.Nombre,
                    TablaEquivalenciaId = grupoRequest.TablaEquivalenciaId,
                    ProfesorId = profesorId,
                    Institucion = grupoRequest.Institucion,
                    Materia = grupoRequest.Materia,
                    CodigoEnlace = codigo,
                    UrlCompleta = url
                };

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
            */
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
            var profesorId = User.FindFirstValue("id");
            if (string.IsNullOrEmpty(profesorId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized));
            
            var resultado = await _editarGrupo.EjecutarAsync(grupoDto, profesorId);

            if (resultado.EsFallo)
                return this.ManejarFallo(resultado);
            
            return NoContent();
            /*
            try
            {
                if (grupoDto == null)
                    return BadRequest("Debe enviar los datos del grupo a editar.");

                string profesorId = User.FindFirst("id")?.Value;

                if (string.IsNullOrEmpty(profesorId))
                    return Unauthorized("No se pudo determinar el ID del profesor desde el token.");

                await _editarGrupo.EjecutarAsync(grupoDto, profesorId);

                return Ok("Grupo actualizado correctamente.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Ocurrió un error inesperado. " + ex.Message });
            }
            */
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
            var profesorId = User.FindFirstValue("id");

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
