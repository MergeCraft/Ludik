using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
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
            .Include(p => p.Estudiante)
            .Include(p => p.MedallasObtenidas)
                .ThenInclude(pm => pm.Medalla)
            .Include(p => p.Grupo)
                .ThenInclude(g => g.TablaEquivalencia)
                    .ThenInclude(te => te.Equivalencias)
                        .ThenInclude(eq => eq.MedallasNecesarias)
            // >>> Nuevo Include para los historiales de rendimiento
            .Include(p => p.HistorialRendimientoPeriodos)
                .ThenInclude(rp => rp.RendimientoMedallas)
                    .ThenInclude(rpm => rpm.Medalla)
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
                    new Error("Error.Unexpected",
                              $"Error al consultar la BD: {detalle}"));
            }
            catch (Exception ex)
            {
                return Resultado<List<PerfilEstudiante>>.Falla(
                    new Error("Error.Unexpected",
                              $"Error inesperado: {ex.Message}"));
            }
        }

        public async Task<Resultado> AddAsync(PerfilEstudiante unObjeto)
        {
            if (unObjeto == null)
                return Resultado.Falla(new Error("Error.Validation", "El perfil de estudiante no puede ser nulo."));

            try
            {
                await _db.PerfilesEstudiantes.AddAsync(unObjeto);
                await _db.SaveChangesAsync();
                return Resultado.Exitoso();
            }
            catch (DbUpdateException dbEx)
            {
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                return Resultado.Falla(new Error("Error.Unexpected", $"Error al agregar el perfil: {detalle}"));
            }
            catch (Exception ex)
            {
                return Resultado.Falla(new Error("Error.Unexpected", ex.Message));
            }
        }

        public Task<Resultado<IEnumerable<PerfilEstudiante>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado<PerfilEstudiante>> GetByIdAsync(int id)
        {
            try
            {
                var perfil = await _db.PerfilesEstudiantes
                    .Include(p => p.InventarioRecompensas)
                        .ThenInclude(ir => ir.Recompensa)
                            .ThenInclude(r => (r as PersonalizacionAvatar).AtributoDesbloqueable)
                    .Include(p => p.MedallasObtenidas)
                        .ThenInclude(pm => pm.Medalla)
                    .Include(p => p.BarraProgreso)
                    .Include(p => p.Grupo)
                        .ThenInclude(g => g.TablaEquivalencia)
                            .ThenInclude(te => te.Equivalencias)
                                .ThenInclude(eq => eq.MedallasNecesarias)
                    .Include(p => p.PotenciadorActivo)
                    .Include(p => p.Estudiante)
                        .ThenInclude(e => e.Perfiles)
                            .ThenInclude(pe => pe.MedallasObtenidas)
                                .ThenInclude(pm => pm.Medalla)
                    .Include(p => p.Estudiante)
                        .ThenInclude(e => e.Perfiles)
                            .ThenInclude(pe => pe.PotenciadorActivo)
                    .Include(p => p.Estudiante)
                        .ThenInclude(e => e.Perfiles)
                            .ThenInclude(pe => pe.InventarioRecompensas)
                                .ThenInclude(ir => ir.Recompensa)
                    .Include(p => p.Estudiante)
                        .ThenInclude(e => e.Perfiles)
                            .ThenInclude(pe => pe.BarraProgreso)
                    .Include(p => p.Estudiante)
                        .ThenInclude(e => e.Perfiles)
                            .ThenInclude(pe => pe.Grupo)
                                .ThenInclude(g => g.TablaEquivalencia)
                                    .ThenInclude(te => te.Equivalencias)
                                        .ThenInclude(eq => eq.MedallasNecesarias)
                    .Include(p => p.Estudiante)
                        .ThenInclude(e => e.Perfiles)
                            .ThenInclude(pe => pe.HistorialRendimientoPeriodos)
                                .ThenInclude(hr => hr.RendimientoMedallas)
                                    .ThenInclude(rm => rm.Medalla)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (perfil == null)
                {
                    return Resultado<PerfilEstudiante>.Falla(
                        new Error("Error.NotFound", $"No se encontró el perfil de estudiante con Id: {id}."));
                }

                return Resultado<PerfilEstudiante>.Exitoso(perfil);
            }
            catch (Exception ex)
            {
                return Resultado<PerfilEstudiante>.Falla(
                    new Error("Error.Unexpected", $"Error inesperado: {ex.Message}"));
            }
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado> UpdateAsync(PerfilEstudiante unObjeto)
        {
            if (unObjeto == null)
                return Resultado.Falla(new Error("Error.Validation", "El objeto a actualizar no puede ser nulo."));
            

            try
            {
                _db.PerfilesEstudiantes.Update(unObjeto);
                await _db.SaveChangesAsync();
                return Resultado.Exitoso();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Resultado.Falla(Error.Conflict);
            }
            catch (DbUpdateException dbEx)
            {
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                return Resultado.Falla(new Error("Error.Unexpected", $"Error al actualizar en la BD: {detalle}"));
            }
            catch (Exception ex)
            {
                return Resultado.Falla(new Error("Error.Unexpected", ex.Message));
            }
        }

        public List<Medalla> verMedallasAlumno(int idAlumno, int idGrupo)
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado<PerfilEstudiante>> GetByEstudianteYGrupoConMedallasAsync(string estudianteId, int grupoId)
        {
            try
            {
                var perfil = await _db.PerfilesEstudiantes
                    .Include(p => p.MedallasObtenidas)
                        .ThenInclude(pm => pm.Medalla)
                    .Include(p => p.BarraProgreso)
                    .Include(p => p.Estudiante)   
                    .Include(p => p.Grupo)        
                    .FirstOrDefaultAsync(p => p.EstudianteId == estudianteId && p.GrupoId == grupoId);

                if (perfil == null)
                    return Resultado<PerfilEstudiante>.Falla(
                        new Error("Perfil.NotFound",
                                  $"No se encontró el perfil del estudiante '{estudianteId}' en el grupo {grupoId}."));
                return Resultado<PerfilEstudiante>.Exitoso(perfil);
            }
            catch (Exception ex)
            {
                return Resultado<PerfilEstudiante>.Falla(new Error("Error.Unexpected", ex.Message));
            }
        }

        public Task<Resultado<IEnumerable<Recompensa>>> ObtenerItemsAvatarAdquiridosAsync(int idPerfilEstudiante)
        {
            throw new NotImplementedException();
        }
        public async Task<Resultado> SaveCambiosAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
                return Resultado.Exitoso();
            }
            catch (DbUpdateException dbEx)
            {
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                return Resultado.Falla(new Error("Error.BD", $"Error al guardar los cambios: {detalle}"));
            }
            catch (Exception ex)
            {
                return Resultado.Falla(new Error("Error.Unexpected", ex.Message));
            }
        }
    }
}
