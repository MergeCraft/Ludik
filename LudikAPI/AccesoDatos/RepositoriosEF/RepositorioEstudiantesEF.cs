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
        private readonly Context _db;
        public RepositorioEstudiantesEF(Context db)
        {
            _db = db;
        }

        public void Add(Estudiante estudianteNuevo)
        {
            try
            {
                if (estudianteNuevo == null)
                {
                    throw new UsuarioNoValidoException();
                }
              
                _db.Estudiantes.Add(estudianteNuevo);
                _db.SaveChanges();
            }
            catch (UsuarioNoValidoException ex)
            {
                throw new UsuarioNoValidoException("El Usuario no es valido.");
            }
        }
        //TODO: hacer que existe nombre usuario busque en las tablas de estudiantes y profesores
        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            return _db.Estudiantes
                .Any(u => u.NombreUsuario.Valor == nombreUsuario);
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

        public Estudiante GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void quitarMedalla(int idAlumno, int idMedalla)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Estudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Estudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public List<Medalla> verMedallasAlumno(int idAlumno, int idGrupo)
        {
            throw new NotImplementedException();
        }
    }
}
