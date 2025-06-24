using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace InterfacesRepositorio
{
	public interface IRepositorioRecompensas : IRepositorio<Recompensa>
	{
        public Task<Resultado<IEnumerable<Recompensa>>> GetByTiendaIdAsync(int tiendaId);


    }

}

