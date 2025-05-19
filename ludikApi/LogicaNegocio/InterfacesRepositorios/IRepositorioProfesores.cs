using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioProfesores : IRepositorio<Profesor>
	{
        bool ExisteNombreUsuario(string nombreUsuario);
        bool ExisiteMailProfesor(string emailUsuario);
    }

}

