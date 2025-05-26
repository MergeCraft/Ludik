using InterfacesRepositorio;
using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioMedallas : IRepositorio<Medalla>
	{
        Task<List<Medalla>> ObtenerMedallasAsignablesMutuamenteAsync(int idGrupo);

    }

}

