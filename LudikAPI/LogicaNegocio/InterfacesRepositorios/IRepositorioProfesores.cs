using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace InterfacesRepositorio
{
	public interface IRepositorioProfesores : IRepositorio<Profesor>
	{
        Task<Resultado<Profesor>> GetByStringId(string id);

    }

}

