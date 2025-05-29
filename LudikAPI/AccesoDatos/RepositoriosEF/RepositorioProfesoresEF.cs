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
        private readonly ContextoDb _db;
        public RepositorioProfesoresEF(ContextoDb db)
        {
            _db = db;
        }
        public void Add(Profesor profesorNuevo)
        {
            try
            {
                if (profesorNuevo == null)
                {
                    throw new UsuarioNoValidoException();
                }

                _db.Profesores.Add(profesorNuevo);
                _db.SaveChanges();
            }
            catch (UsuarioNoValidoException ex)
            {
                throw new UsuarioNoValidoException("El Usuario no es valido.");
            }
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
