using LogicaAplicacion.DTOs.TablaEquivalenciaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.TablaEquivalencia;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "EsProfesor")]
    public class TablaEquivalenciaController : Controller
    {
        private readonly IAltaTablaEquivalencia _altaTablaEquivalencia;

        public TablaEquivalenciaController(IAltaTablaEquivalencia altaTablaEquivalencia)
        {
            _altaTablaEquivalencia = altaTablaEquivalencia;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTablaEquivalencia([FromBody] TablaEquivalenciaAltaDto tablaDto)
        {

            var resultado = await _altaTablaEquivalencia.EjecutarAsync(tablaDto);


            if (resultado.EsFallo)
                this.ManejarFallo(resultado);

            return StatusCode(StatusCodes.Status201Created, "La tabla de equivalencia fue creada con exito.");
        }


    }
}
