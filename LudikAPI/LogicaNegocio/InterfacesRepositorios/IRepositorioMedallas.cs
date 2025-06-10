using InterfacesRepositorio;
using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace InterfacesRepositorio
{
	public interface IRepositorioMedallas : IRepositorio<Medalla>
	{
        Task<List<Medalla>> ObtenerMedallasAsignablesMutuamenteAsync(int idGrupo);
        Task<Resultado<IEnumerable<Medalla>>> FindByIdsAsync(List<int> ids);
    }

}

