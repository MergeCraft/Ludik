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
    public class RepositorioUsuariosEF : IRepositorioUsuarios
    {
        private readonly ContextoDb _db;
        public RepositorioUsuariosEF(ContextoDb db)
        {
            _db = db;
        }
        public void Add(Usuario usuarioNuevo)
        {

        }

        public IEnumerable<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public Usuario GetById(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<Usuario> loginUsuario(string identificador)
        {
            try
            {
                var estudiante = await _db.Estudiantes
                    .SingleOrDefaultAsync(e => e.NombreUsuario.Valor == identificador);

                if (estudiante != null)
                    return estudiante;

                var profesor = await _db.Profesores
                    .SingleOrDefaultAsync(p => p.NombreUsuario.Valor == identificador);

                if (profesor != null)
                    return profesor;

                throw new UsuarioNoValidoException($"Usuario con '{identificador}' no encontrado.");
            }
            catch (UsuarioNoValidoException)
            {
                // Re-lanzamos la excepción
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar loguear usuario.", ex);
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
