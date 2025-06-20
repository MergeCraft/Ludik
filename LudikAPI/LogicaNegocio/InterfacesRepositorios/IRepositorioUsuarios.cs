using InterfacesRepositorio;
using System;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioUsuarios : IRepositorio<Usuario>
	{
		Task<Usuario> GetUsuarioPorNombreAsync(String nombreUsuario);

        Task<IList<string>> GetRolesAsync(Usuario usuario);
    }

}

