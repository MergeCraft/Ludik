using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
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
                return Resultado.Falla(Error.Unexpected); 
            }
        }

        public async Task<Resultado> asignarMedalla(int idAlumno, int idMedalla)
        {
            try
            {
                /*
    
                var alumno = await _db.Estudiantes.Include(e => e.MedallasObtenidas).FirstOrDefaultAsync(e => e.Id == idAlumno); // Asumiendo que Estudiante.Id es int
                var medalla = await _db.Medallas.FindAsync(idMedalla);

                if (alumno == null)
                    return Resultado.Falla(new Error("Repositorio.Estudiante.NotFound", $"Alumno con id {idAlumno} no encontrado.")); [cite: 15]
                if (medalla == null)
                    return Resultado.Falla(new Error("Repositorio.Medalla.NotFound", $"Medalla con id {idMedalla} no encontrada.")); [cite: 15]

                await _db.SaveChangesAsync();
                return Resultado.Exitoso(); 
                */
                
                return Resultado.Falla(new Error("Repositorio.Estudiante.NoImplementado", "Método AsignarMedallaAsync no implementado."));
            }
            catch (DbUpdateException dbEx)
            {
                return Resultado.Falla(new Error("Repositorio.Estudiante.AsignarMedalla.DbError", dbEx.InnerException?.Message ?? dbEx.Message));
            }
            catch (Exception e)
            {
                return Resultado.Falla(Error.Unexpected);
            }
        }

        public async Task<Resultado> asignarMedallaEntreAlumnos(int idAlumnoOrigen, int idAlumnoDestino, int idMedalla)
        {
            try
            {
                // Placeholder
                await Task.CompletedTask;
                return Resultado.Falla(new Error("Repositorio.Estudiante.NoImplementado", "Sin implementar"));
            }
            catch (Exception e)
            {
                return Resultado.Falla(Error.Unexpected);
            }
        }

        public async Task<Resultado> quitarMedalla(int idAlumno, int idMedalla)
        {
            try
            {
                // Placeholder
                await Task.CompletedTask;
                return Resultado.Falla(new Error("Repositorio.Estudiante.NoImplementado", "AsignarMedallaEntreAlumnosAsync no implementado."));
            }
            catch (Exception e)
            {
                return Resultado.Falla(Error.Unexpected);
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
                return Resultado<Estudiante>.Falla(Error.Unexpected);
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


    }
}
