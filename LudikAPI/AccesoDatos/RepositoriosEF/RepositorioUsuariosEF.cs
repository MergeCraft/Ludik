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
        private readonly Context _db;
        public RepositorioUsuariosEF(Context db)
        {
            _db = db;
        }

        public Task AddAsync(Usuario unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetByIdAsync(int id)
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

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Usuario unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Usuario unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}
