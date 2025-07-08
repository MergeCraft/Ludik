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
        public async Task<Resultado<Estudiante>> GetByStringIdAsync(string id)
        {
            try
            {
                var usuario = await _db.Users.FirstOrDefaultAsync(e => e.Id == id);

                if (usuario == null)
                    return Resultado<Estudiante>.Falla(Error.NotFound);
                Estudiante estudiante = (Estudiante)usuario;
                
 
                return Resultado<Estudiante>.Exitoso(estudiante);
            }
            catch (Exception e)
            {

                return Resultado<Estudiante>.Falla(new Error("Error.Unexpected", e.Message));
            }
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


    }
}
