using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioProfesores : IRepositorio<Profesor>
	{
        Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario);
        Task<bool> ExisiteMailProfesorAsync(string emailUsuario);
    }

}

