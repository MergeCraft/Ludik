using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

using InterfacesRepositorio;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioUsuariosEF : IRepositorioUsuarios
    {
        private readonly ContextoDb _db;
        private readonly UserManager<Usuario> _userManager;
        public RepositorioUsuariosEF(ContextoDb db, UserManager<Usuario> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public Task<Usuario> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> GetAllAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<Usuario> GetUsuarioPorNombreAsync(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentNullException(nameof(nombreUsuario));

            var usuario = await _db.Users
                .SingleOrDefaultAsync(e => e.UserName == nombreUsuario);

            if (usuario != null)
                return usuario;

            throw new UsuarioNoValidoException($"Usuario con '{nombreUsuario}' no encontrado.");
        }

        public async Task<bool> VerificarContrasenaAsync(Usuario usuario, string clave)
        {
            if (usuario == null) 
                throw new ArgumentNullException(nameof(usuario));
            return await _userManager.CheckPasswordAsync(usuario, clave);
        }

        public async Task<IList<string>> GetRolesAsync(Usuario usuario)
        {
            if(usuario == null) 
                throw new ArgumentNullException(nameof(usuario));
            return await _userManager.GetRolesAsync(usuario);
        }

        public async Task AddAsync(Usuario usuarioNuevo)
        {
            if(usuarioNuevo == null)
                throw new UsuarioNoValidoException("El usuario proporcionado no es válido.");

            await _db.Users.AddAsync(usuarioNuevo);
            await _db.SaveChangesAsync();
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
