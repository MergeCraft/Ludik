using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace InterfacesRepositorio
{
	public interface IRepositorioProfesores : IRepositorio<Profesor>
    {
        public Task<Resultado<Profesor>> GetByStringIdAsync(string id);
    }

}

