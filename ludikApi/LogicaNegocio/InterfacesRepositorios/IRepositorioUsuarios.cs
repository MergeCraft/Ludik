using InterfacesRepositorio;
using System;
using Dominio;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioUsuarios : IRepositorio<Usuario>
	{
		Usuario loginUsuario(String identificador);

	}

}

