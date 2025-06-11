using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Resultados;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioPerfilEstudianteGrupoEF : IRepositorioPerfilEstudianteGrupo
    {
        private readonly ContextoDb _db;
        public RepositorioPerfilEstudianteGrupoEF(ContextoDb db)
        {
            _db = db;
        }
        public async Task<Resultado<List<PerfilEstudiante>>> ObtenerPorGrupoIdAsync(int grupoId)
        {
            try
            {
                var perfiles = await _db.PerfilesEstudiantes
                                       .Include(p => p.BarraProgreso)
                                       .Where(p => p.GrupoId == grupoId)
                                       .ToListAsync();

                if (perfiles == null || !perfiles.Any())
                    return Resultado<List<PerfilEstudiante>>.Falla(
                        new Error("Error.Validation",
                                  $"No se encontraron perfiles para el grupo con Id {grupoId}."));

                return Resultado<List<PerfilEstudiante>>.Exitoso(perfiles);
            }
            catch (DbUpdateException dbEx)
            {
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                return Resultado<List<PerfilEstudiante>>.Falla(
                    new Error("PerfilEstudiante.ObtenerPorGrupoId.DbError",
                              $"Error al consultar la BD: {detalle}"));
            }
            catch (Exception ex)
            {
                // Puedes agregar logging aquí si lo deseas
                return Resultado<List<PerfilEstudiante>>.Falla(
                    new Error("PerfilEstudiante.ObtenerPorGrupoId.ErrorInesperado",
                              $"Error inesperado: {ex.Message}"));
            }
        }

        public Task<Resultado> AddAsync(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<IEnumerable<PerfilEstudiante>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<PerfilEstudiante>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public List<Medalla> verMedallasAlumno(int idAlumno, int idGrupo)
        {
            throw new NotImplementedException();
        }
    }
}
