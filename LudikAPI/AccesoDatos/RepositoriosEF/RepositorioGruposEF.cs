using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Resultados;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
	public class RepositorioGruposEF : IRepositorioGrupos

	{
		private readonly ContextoDb _db;
		public RepositorioGruposEF(ContextoDb db)
		{
			_db = db;
		}
		public void aceptarSolicitud(SolicitudUnion idSolicitud)
		{
			throw new NotImplementedException();
		}

		public async Task<Resultado> AddAsync(Grupo unGrupo)
		{
			if (unGrupo == null)
				return Resultado.Falla(new Error("Grupo.Add.Validacion", "El Grupo no puede ser nulo."));

			try
			{
				if (unGrupo.TablaEquivalencia != null)
					_db.Entry(unGrupo.TablaEquivalencia).State = EntityState.Unchanged;

				if (unGrupo.EnlaceUnion != null)
					_db.Entry(unGrupo.EnlaceUnion).State = EntityState.Added;

				if (unGrupo.Tienda != null)
					_db.Entry(unGrupo.Tienda).State = EntityState.Added;


				await _db.Grupos.AddAsync(unGrupo);
				await _db.SaveChangesAsync();
				return Resultado.Exitoso();
			}
			catch (DbUpdateException dbEx)
			{
				var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
				return Resultado.Falla(new Error("Grupo.Add.DbError", $"Error al guardar el grupo en la BD: {detalle}"));
			}

			catch (GrupoNoValidoExeption valEx)
			{
				return Resultado.Falla(new Error("Grupo.Add.Validacion", valEx.Message));
			}
			catch (Exception e)
			{
				return Resultado.Falla(Error.Unexpected); // [cite: 14, 47]
			}
		}

        public async Task<Resultado<Grupo>> GetByIdAsync(int id)
        {
            try
            {
                var grupo = await _db.Grupos
                    .Include(g => g.Alumnos)                              
                    .Include(g => g.TablaEquivalencia)
                        .ThenInclude(t => t.Equivalencias)
                    .Include(g => g.EnlaceUnion)
                    .Include(g => g.Tienda)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (grupo == null)
                {
                    return Resultado<Grupo>.Falla(Error.NotFound);
                }

                return Resultado<Grupo>.Exitoso(grupo);
            }
            catch (Exception e)
            {
                return Resultado<Grupo>.Falla(Error.Unexpected);
            }
        }

        public async Task<Resultado<IEnumerable<Grupo>>> GetAllAsync()
		{
			try
			{
				var grupos = await _db.Grupos.ToListAsync();
				return Resultado<IEnumerable<Grupo>>.Exitoso(grupos);
			}
			catch (Exception e)
			{
				return Resultado<IEnumerable<Grupo>>.Falla(Error.Unexpected);
			}
		}

		public async Task<Resultado> UpdateAsync(Grupo grupoNuevo)
		{
			if (grupoNuevo == null)
				return Resultado.Falla(new Error("Grupo.Update.Validacion", "El grupo para actualizar no puede ser null.")); // [cite: 14]


			try
			{
				var grupoExistente = await _db.Grupos.FindAsync(grupoNuevo.Id);
				if (grupoExistente == null)
					return Resultado.Falla(Error.NotFound);


				_db.Entry(grupoExistente).CurrentValues.SetValues(grupoNuevo);

				await _db.SaveChangesAsync();
				return Resultado.Exitoso();
			}
			catch (DbUpdateConcurrencyException dbEx)
			{

				return Resultado.Falla(new Error(Error.Conflict.Codigo, $"Error de concurrencia al actualizar el grupo: {dbEx.Message}"));
			}
			catch (DbUpdateException dbEx)
			{
				var detalle = dbEx.InnerException?.Message ?? dbEx.Message;

				return Resultado.Falla(new Error("Grupo.Update.DbError", $"Error al actualizar el grupo en la BD: {detalle}"));
			}
			catch (GrupoNoValidoExeption valEx)
			{
				return Resultado.Falla(new Error("Grupo.Update.Validacion", valEx.Message));
			}
			catch (Exception e)
			{
				return Resultado.Falla(Error.Unexpected);
			}
		}

		public async Task<Resultado> RemoveAsync(int id)
		{
			try
			{
				var grupo = await _db.Grupos
					.Include(g => g.Alumnos)
					.Include(g => g.Solicitudes)
					.Include(g => g.TablasClasificacion)
					.Include(g => g.EnlaceUnion)
					.Include(g => g.Tienda)
					.FirstOrDefaultAsync(g => g.Id == id);

				if (grupo == null)
				{
					return Resultado.Falla(Error.NotFound); // [cite: 14, 39]
				}

				if (grupo.Alumnos != null && grupo.Alumnos.Any())
					_db.PerfilesEstudiantes.RemoveRange(grupo.Alumnos);
				if (grupo.Solicitudes != null && grupo.Solicitudes.Any())
					_db.SolicitudesUnion.RemoveRange(grupo.Solicitudes);
				if (grupo.TablasClasificacion != null && grupo.TablasClasificacion.Any())
					_db.TablasClasificacion.RemoveRange(grupo.TablasClasificacion);

				if (grupo.EnlaceUnion != null)
					_db.EnlacesUnion.Remove(grupo.EnlaceUnion);
				if (grupo.Tienda != null)
					_db.Tiendas.Remove(grupo.Tienda);

				_db.Grupos.Remove(grupo);
				await _db.SaveChangesAsync();
				return Resultado.Exitoso(); // [cite: 12]
			}
			catch (DbUpdateException dbEx)
			{
				var detalle = dbEx.InnerException?.Message ?? dbEx.Message;

				return Resultado.Falla(new Error("Grupo.Remove.DbError", $"Error al eliminar el grupo: {detalle}"));
			}
			catch (Exception e)
			{
				return Resultado.Falla(Error.Unexpected);
			}
		}



		public Task<TablaEquivalencia> obtenerTablaDelGrupoAsync(int idGrupo)
		{
			throw new NotImplementedException();
		}

		public Task<int> calcularNotaEstudianteAsync(int idAlumno, int idGrupo)
		{
			throw new NotImplementedException();
		}

		public Task aceptarSolicitudAsync(SolicitudUnion idSolicitud)
		{
			throw new NotImplementedException();
		}

		public Task rechazarSolicitudAsync(SolicitudUnion idSolictud)
		{
			throw new NotImplementedException();
		}

		public Task<List<Grupo>> obtenerGruposPorProfesorAsync(int idProfesor)
		{
			throw new NotImplementedException();
		}

		public Task unirseAGrupoAsync(int idAlumno, Grupo grupo)
		{
			throw new NotImplementedException();
		}

		public Task<List<Estudiante>> obtenerAlumnosDelGrupoAsync(int idGrupo)
		{
			throw new NotImplementedException();
		}

		public Task reiniciarLogrosDeGrupoAsync(int idGrupo)
		{
			throw new NotImplementedException();
		}

		public Task<List<TablaClasificacion>> obtenerTablasDeClasificacionDeGrupoAsync(int idGrupo)
		{
			throw new NotImplementedException();
		}

		public Task<Resultado> RemoveAsync(Grupo unObjeto)
		{
			throw new NotImplementedException();
		}

		public async Task<Grupo> ObtenerPorEnlaceAsync(string codigoBase)
		{
			return await _db.Grupos
					.Include(g => g.EnlaceUnion)
					.FirstOrDefaultAsync(g => g.EnlaceUnion.CodigoUnico == codigoBase);
		}

		public async Task<List<Grupo>> ObtenerGruposPorEstudianteId(string idEstudiante)
		{

			if (string.IsNullOrWhiteSpace(idEstudiante))
			{
				// Devuelve una lista vacía si el ID es inválido para evitar errores en la consulta.
				return new List<Grupo>();
			}

			List<Grupo> gruposDelEstudiante = await _db.Grupos
				.Include(g => g.Alumnos)
				.Where(g => g.Alumnos.Any(pe => pe.EstudianteId == idEstudiante))
				.ToListAsync();

			return gruposDelEstudiante;
		}

		public async Task<List<Grupo>> ObtenerGruposPorProfesorId(string idProfesor)
		{
			if (string.IsNullOrWhiteSpace(idProfesor))
			{
				// Devuelve una lista vacía si el ID es inválido para evitar errores en la consulta.
				return new List<Grupo>();
			}

			List<Grupo> gruposDelProfesor = await _db.Grupos
				.Include(g => g.Alumnos)
				.Where(g => g.ProfesorId == idProfesor)
				.ToListAsync();

			return gruposDelProfesor;
		}
	}

}
