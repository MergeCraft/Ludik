using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dominio;
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
                if (unGrupo.tablaEquivalencia != null)
                    _db.Entry(unGrupo.tablaEquivalencia).State = EntityState.Unchanged;

                if (unGrupo.enlaceUnion != null)
                    _db.Entry(unGrupo.enlaceUnion).State = EntityState.Added; 
                
                if (unGrupo.tienda != null)
                    _db.Entry(unGrupo.tienda).State = EntityState.Added; 
                

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
                    .Include(g => g.tablaEquivalencia)
                    .Include(g => g.enlaceUnion)
                    .Include(g => g.tienda)
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
                    .Include(g => g.alumnos) 
                    .Include(g => g.solicitudes)
                    .Include(g => g.tablasClasificacion)
                    .Include(g => g.enlaceUnion)
                    .Include(g => g.tienda)
                    .FirstOrDefaultAsync(g => g.Id == id);

                if (grupo == null)
                {
                    return Resultado.Falla(Error.NotFound); // [cite: 14, 39]
                }

                //TODO: Revisaar con Mnauel, ya que:
                // La eliminación explícita de entidades relacionadas es necesaria si
                // no tienes configurada la eliminación en cascada en la base de datos o en el modelo EF Core,
                // o si necesitas lógica adicional antes de eliminar.
                // Si la cascada está bien configurada, EF Core podría manejar esto al eliminar 'grupo'.

                if (grupo.alumnos != null && grupo.alumnos.Any())
                    _db.PerfilesEstudiantes.RemoveRange(grupo.alumnos);
                if (grupo.solicitudes != null && grupo.solicitudes.Any())
                    _db.SolicitudesUnion.RemoveRange(grupo.solicitudes);
                if (grupo.tablasClasificacion != null && grupo.tablasClasificacion.Any())
                    _db.TablasClasificacion.RemoveRange(grupo.tablasClasificacion);

                if (grupo.enlaceUnion != null)
                    _db.EnlacesUnion.Remove(grupo.enlaceUnion);
                if (grupo.tienda != null)
                    _db.Tiendas.Remove(grupo.tienda);

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
                    .Include(g => g.enlaceUnion) 
                    .FirstOrDefaultAsync(g => g.enlaceUnion.codigoBase == codigoBase);
        }

        public async Task<List<Grupo>> ObtenerGruposPorEstudianteId(string idEstudiante)
        {

            if (string.IsNullOrWhiteSpace(idEstudiante))
            {
                // Devuelve una lista vacía si el ID es inválido para evitar errores en la consulta.
                return new List<Grupo>();
            }

            List<Grupo> gruposDelEstudiante = await _db.Grupos
                .Where(g => g.alumnos.Any(pe => pe.EstudianteId == idEstudiante))
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
                .Where(g => g.ProfesorId == idProfesor)
                .ToListAsync();

            return gruposDelProfesor;
        }
    }
    
}
