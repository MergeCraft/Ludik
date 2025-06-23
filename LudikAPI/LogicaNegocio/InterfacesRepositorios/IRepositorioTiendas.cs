using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace InterfacesRepositorio
{
	public interface IRepositorioTiendas : IRepositorio<Tienda>
	{
        public Task<Resultado<Tienda>> GetByStringIdAsync(string idString);


    }

}

