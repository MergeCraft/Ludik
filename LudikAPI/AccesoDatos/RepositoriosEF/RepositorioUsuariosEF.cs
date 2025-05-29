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


        public async Task<Usuario> GetUsuarioPorNombreAsync(string nombreUsuario)
        {
            var usuario = await _db.Users
                .SingleOrDefaultAsync(e => e.UserName == nombreUsuario);

            if (usuario != null)
                return usuario;

            throw new UsuarioNoValidoException($"Usuario con '{nombreUsuario}' no encontrado.");
        }

        public async Task<bool> VerificarContrasenaAsync(Usuario usuario, string clave)
        {
            return await _userManager.CheckPasswordAsync(usuario, clave);
        }

        public async Task<IList<string>> GetRolesAsync(Usuario usuario)
        {
            return await _userManager.GetRolesAsync(usuario);
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
