using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Resultados;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioEstudiantesEF : IRepositorioEstudiantes
    {
        private readonly ContextoDb _db;
        public RepositorioEstudiantesEF(ContextoDb db)
        {
            _db = db;
        }

        public async Task<Resultado> AddAsync(Estudiante estudianteNuevo)
        {
            if (estudianteNuevo == null)
                return Resultado.Falla(new Error("Repositorio.Estudiante.Add.Null", "El estudiante no puede ser nulo.")); 

            try
            {
                _db.Estudiantes.Add(estudianteNuevo);
                await _db.SaveChangesAsync();
                return Resultado.Exitoso();
            }
            catch (DbUpdateException dbEx)
            {
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                return Resultado.Falla(new Error("Repositorio.Estudiante.Add.DbError", $"Error al guardar el estudiante: {detalle}")); 
            }
            catch (Exception e)
            {
                return Resultado.Falla(new Error("Unexpected", e.Message)); 
            }
        }


        public async Task<Resultado<IEnumerable<Estudiante>>> GetAll()
        {
            try
            {
                var estudiantes = await _db.Estudiantes.ToListAsync();
                return Resultado<IEnumerable<Estudiante>>.Exitoso(estudiantes); 
            }
            catch (Exception e)
            {
                return Resultado<IEnumerable<Estudiante>>.Falla(Error.Unexpected);
            }
        }

        public async Task<Resultado<Estudiante>> GetByIdAsync(int id)
        {
            try
            {
                var estudiante = await _db.Estudiantes.FindAsync(id);
                if (estudiante == null)
                    return Resultado<Estudiante>.Falla(Error.NotFound); 
                
                return Resultado<Estudiante>.Exitoso(estudiante); 
            }
            catch (Exception e)
            {
                return Resultado<Estudiante>.Falla(new Error("Unexpected",e.Message));
            }
        }
        public async Task<Estudiante> GetByIdAsyncString(string id)
        {
            return await _db.Estudiantes.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<Resultado<IEnumerable<Estudiante>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }


        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(Estudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(Estudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public List<Medalla> getMedallasAlumno(int idAlumno, int idGrupo)
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado<Estudiante>> GetByPerfilIdAsync(int perfilId)
        {
            try
            {
                // 1) Recuperar el Perfil para saber su EstudianteId
                var perfil = await _db.PerfilesEstudiantes
                    .AsNoTracking()
                    .Include(p => p.Estudiante)          // trae el estudiante
                    .FirstOrDefaultAsync(p => p.Id == perfilId);

                if (perfil == null)
                    return Resultado<Estudiante>.Falla(
                        new Error("Error.NotFound", $"No se encontró el perfil con Id {perfilId}."));

                var estudianteId = perfil.EstudianteId;

                // 2) Recuperar EL ESTUDIANTE completo con todos sus perfiles y medallas
                var estudiante = await _db.Estudiantes
                    .AsNoTracking()
                    .Where(e => e.Id == estudianteId)
                    .Include(e => e.Perfiles)
                        .ThenInclude(p => p.PerfilMedallas)
                            .ThenInclude(pm => pm.Medalla)
                    // si persistes PotenciadorActivo, inclúyelo también:
                    .Include(e => e.Perfiles)
                        .ThenInclude(p => p.PotenciadorActivo)
                    .FirstOrDefaultAsync();

                if (estudiante == null)
                    return Resultado<Estudiante>.Falla(
                        new Error("Error.NotFound", $"Estudiante {estudianteId} no encontrado."));

                return Resultado<Estudiante>.Exitoso(estudiante);
            }
            catch (Exception ex)
            {
                return Resultado<Estudiante>.Falla(
                    new Error("Error.Unexpected", ex.Message));
            }
        }


    }
}
