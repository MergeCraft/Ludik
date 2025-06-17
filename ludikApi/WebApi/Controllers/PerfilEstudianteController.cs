using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using LogicaAplicacion.InterfacesCasosUsos.PerfilEstudiante;
using LogicaNegocio.Resultados;
using LogicaAplicacion.DTOs.PerfilEstudianteDTO;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "EsEstudiante")]
    public class PerfilEstudianteController : ControllerBase
    {
        private readonly IObtenerPerfilConMedallas _uc;
        public PerfilEstudianteController(IObtenerPerfilConMedallas uc)
        {
            _uc = uc;
        }

        /// <summary>
        /// Obtiene el perfil del estudiante autenticado en el grupo indicado, incluyendo lista de medallas agrupadas.
        /// </summary>
        /// <param name="grupoId">ID del grupo al que pertenece el perfil.</param>
        /// <returns>
        /// 200 OK con PerfilConMedallasDto si existe y pertenece;
        /// 401 Unauthorized si no hay estudiante autenticado;
        /// 404 NotFound si no se encuentra el perfil en el grupo;
        /// 400 BadRequest para otros errores de validación o negocio.
        /// </returns>
        [HttpGet("mi-perfil/grupo/{grupoId}/medallas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PerfilConMedallasDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMiPerfilConMedallas(int grupoId)
        {
            
            var estudianteId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(estudianteId))
                return this.ManejarFallo(Resultado.Falla(Error.Unauthorized)); 

            var resultado = await _uc.EjecutarAsync(estudianteId, grupoId);

            if (!resultado.EsExitoso)
            {
                return this.ManejarFallo(resultado);
            }

            return Ok(resultado.Valor);
        }
    }
}
