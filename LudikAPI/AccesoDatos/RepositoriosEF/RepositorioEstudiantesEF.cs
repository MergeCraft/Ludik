using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;
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

        public async Task AddAsync(Estudiante estudianteNuevo)
        {
            if(estudianteNuevo == null)
                throw new ArgumentNullException(nameof(estudianteNuevo), "El estudiante no puede ser nulo.");

            _db.Estudiantes.Add(estudianteNuevo);
            await _db.SaveChangesAsync();
        }

        public void asignarMedalla(int idAlumno, int idMedalla)
        {
            throw new NotImplementedException();
        }

        public void asignarMedallaEntreAlumnos(int idAlumnoOrigen, int idAlumnoDestino, int idMedalla)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Estudiante> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Estudiante> GetByIdAsync(int id)
        {
            return await _db.Estudiantes.FindAsync(id);
        }
        public async Task<Estudiante> GetByIdAsyncString(string id)
        {
            return await _db.Estudiantes.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void quitarMedalla(int idAlumno, int idMedalla)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Estudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Estudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public List<Medalla> getMedallasAlumno(int idAlumno, int idGrupo)
        {
            throw new NotImplementedException();
        }


    }
}
