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
        public RepositorioEstudiantesEF()
        {
            _db = new Context();
        }
        public void Add(Estudiante estudianteNuevo)
        {
            try
            {
                if (estudianteNuevo == null)
                {
                    throw new UsuarioNoValidoExeption();
                }
              
                _db.Usuarios.Add(estudianteNuevo);
                _db.SaveChanges();
            }
            catch (UsuarioNoValidoExeption ex)
            {
                throw new UsuarioNoValidoExeption("El Usuario no es valido.");
            }
        }
        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            return _db.Usuarios
                .Any(u => u.NombreUsuario.Nombre == nombreUsuario);
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
