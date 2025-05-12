using InterfacesRepositorio;
using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioMedallas : IRepositorio<Medalla>
	{
		List<Medalla> obtenerMedallasAsignablesMutuamente(int idGrupo);

	}

}

