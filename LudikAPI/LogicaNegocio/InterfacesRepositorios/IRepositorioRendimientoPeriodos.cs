using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioRendimientoPeriodos : IRepositorio<RendimientoPeriodo>
	{
		void almacenarLogrosPrevios(int idGrupo);

	}

}

