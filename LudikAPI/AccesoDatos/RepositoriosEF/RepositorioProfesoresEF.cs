using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.ValueObjects;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioProfesoresEF : IRepositorioProfesores
    {
        private readonly Context _db;
        public RepositorioProfesoresEF()
        {
            _db = new Context();
        }
        public void Add(Profesor profesorNuevo)
        {
            try
            {
                if (profesorNuevo == null)
                {
                    throw new UsuarioNoValidoException();
                }

                _db.Usuarios.Add(profesorNuevo);
                _db.SaveChanges();
            }
            catch (UsuarioNoValidoException ex)
            {
                throw new UsuarioNoValidoException("El Usuario no es valido.");
            }
        }

        public bool ExisiteMailProfesor(string emailUsuario)
        {
            return _db.Usuarios
                .OfType<Profesor>() // Filtra solo objetos que son Profesor
                .Any(p => p.email.Valor == emailUsuario); // Compara el valor dentro del ValueObject
        }

       

        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            return _db.Usuarios
                .Any(u => u.NombreUsuario.Valor == nombreUsuario);
        }

        public IEnumerable<Profesor> GetAll()
        {
            throw new NotImplementedException();
        }

        public Profesor GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Profesor unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Profesor unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}
