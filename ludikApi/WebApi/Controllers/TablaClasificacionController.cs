using LogicaAplicacion.DTOs.TablaClasificacionDTOs;
using System.Security.Claims;
using LogicaAplicacion.InterfacesCasosUsos.TablaClasificacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using LogicaNegocio.Resultados;
using LogicaAplicacion.ImplementacionCasosUsos.TablaClasificacion;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Policy = "EsProfesor")]
	public class TablaClasificacionController : ControllerBase
	{
		private readonly IAltaTablaClasificacion _altaTablaClasificacion;
		private readonly IObtenerTablaClasificacion _obtenerTablaClasificacion;
		private readonly IObtenerTodasLasTablasClasificacion _obtenerTodasLasTablasClasificacion;
		private readonly IBajaTablaClasificacion _bajaTablaClasificacion;
		public TablaClasificacionController(IAltaTablaClasificacion altaTablaClasificacion, IObtenerTablaClasificacion obtenerTablaClasificacion, IObtenerTodasLasTablasClasificacion obtenerTodasLasTablasClasificacion, IBajaTablaClasificacion bajaTablaClasificacion)
		{
			_altaTablaClasificacion = altaTablaClasificacion;
			_obtenerTablaClasificacion = obtenerTablaClasificacion;
			_obtenerTodasLasTablasClasificacion = obtenerTodasLasTablasClasificacion;
			_bajaTablaClasificacion = bajaTablaClasificacion;

		}
		/// <summary>
		/// Crea una nueva tabla de clasificación dentro de un grupo.
		/// </summary>
		/// <param name="grupoId">ID del grupo al que pertenece la tabla.</param>
		/// <param name="dto">Datos de la tabla a crear (nombre + medalla asociada).</param>
		/// <response code="201">Tabla creada con éxito.</response>
		/// <response code="400">Validaciones de negocio fallaron.</response>
		/// <response code="401">Usuario no autenticado.</response>
		/// <response code="403">El usuario no es profesor o no es dueño del grupo.</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> CrearTablaClasificacion(
	[FromQuery] int grupoId,
	[FromBody] TablaClasificacionAltaDto dto)
		{
			var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(profesorId))
				return Unauthorized(new Error("Error.Unauthorized", "No se pudo identificar al profesor del token."));

			var resultado = await _altaTablaClasificacion.EjecutarAsync(grupoId, dto);

			if (resultado.EsFallo)
				return this.ManejarFallo(resultado);

			return StatusCode(
				StatusCodes.Status201Created,
				new { Mensaje = "Tabla de clasificación creada exitosamente." }
			);
		}
		/// <summary>
		/// Obtiene la información de una tabla de clasificación (nombre y participantes ordenados) por su ID.
		/// </summary>
		/// <param name="tablaId">ID de la tabla de clasificación a consultar.</param>
		/// <response code="200">Devuelve el DTO con el nombre y la lista de participantes.</response>
		/// <response code="400">La consulta falló (p. ej. tabla no existe).</response>
		/// <response code="401">Usuario no autenticado.</response>
		/// <response code="403">El usuario no tiene rol de Profesor o no puede acceder a esta tabla.</response>
		[HttpGet("{tablaId}")]
		[ProducesResponseType(typeof(TablaClasificacionInfoDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> ObtenerTablaClasificacion(int tablaId)
		{
			// 1) Verificamos que venga un profesor autenticado
			var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(profesorId))
				return Unauthorized(new Error("Error.Unauthorized", "No se pudo identificar al profesor del token."));

			// 2) Ejecutamos el caso de uso
			var resultado = await _obtenerTablaClasificacion.EjecutarAsync(tablaId);

			// 3) Si falla (tabla no existe o permiso denegado), devolvemos el error
			if (resultado.EsFallo)
				return this.ManejarFallo(resultado);

			// 4) Si todo OK, devolvemos 200 con el DTO
			return Ok(resultado.Valor);
		}
		/// <summary>
		/// Obtiene todas las tablas de clasificación (cada una con sus participantes ordenados).
		/// </summary>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<TablaClasificacionInfoDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> ObtenerTodasLasTablasClasificacion()
		{
			var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(profesorId))
				return Unauthorized(new Error("Error.Unauthorized", "No se pudo identificar al profesor."));

			var resultado = await _obtenerTodasLasTablasClasificacion.EjecutarAsync();
			if (resultado.EsFallo)
				return this.ManejarFallo(resultado);

			return Ok(resultado.Valor);
		}
		/// <summary>
		/// Elimina una tabla de clasificación por su ID.
		/// </summary>
		/// <param name="tablaId">ID de la tabla a eliminar.</param>
		/// <response code="204">Eliminación exitosa.</response>
		/// <response code="400">Falló la operación.</response>
		/// <response code="401">Usuario no autenticado.</response>
		/// <response code="403">Usuario no autorizado.</response>
		[HttpDelete("{tablaId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> EliminarTablaClasificacion(int tablaId)
		{
			var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(profesorId))
				return Unauthorized(new Error("Error.Unauthorized", "No se pudo identificar al profesor del token."));

			var resultado = await _bajaTablaClasificacion.EjecutarAsync(tablaId);
			if (resultado.EsFallo)
				return this.ManejarFallo(resultado);

			return NoContent();
		}
	}
}
