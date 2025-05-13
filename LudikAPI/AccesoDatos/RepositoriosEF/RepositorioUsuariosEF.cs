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
        public void Add(Usuario unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public Usuario GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario loginUsuario(string identificador, string hashContrasena)
        {
            try
            {
                var usr = _db.Usuarios
            .SingleOrDefault(u =>(u.nombreUsuario.Nombre == identificador)&& u.contrasenia.Clave == hashContrasena);
            return usr;
            }
            catch (UsuarioNoValidoExeption ex)
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
