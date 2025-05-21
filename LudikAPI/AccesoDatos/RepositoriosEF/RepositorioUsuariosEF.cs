using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioUsuariosEF : IRepositorioUsuarios
    {
        private readonly Context _db;
        public RepositorioUsuariosEF()
        {
            _db = new Context();
        }
        public void Add(Usuario usuarioNuevo)
        {
            try
            {
                if (usuarioNuevo == null)
                {
                    throw new UsuarioNoValidoException();
                }

                usuarioNuevo.EsValido();
                _db.Usuarios.Add(usuarioNuevo);
                _db.SaveChanges();
            }
            catch (UsuarioNoValidoException ex)
            {
                throw new UsuarioNoValidoException("El Usuario no es valido.");
            }
        }

        public IEnumerable<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public Usuario GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario loginUsuario(string identificador)
        {
            try
            {
                var usr = _db.Usuarios
            .SingleOrDefault(u =>(u.NombreUsuario.Valor == identificador));
            return usr;
            }
            catch (UsuarioNoValidoException ex)
            {
                throw ex;
            }
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Usuario unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}
