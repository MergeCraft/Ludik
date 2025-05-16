using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace InterfacesRepositorio
{
	public interface IRepositorioEnlacesUnionGrupo : IRepositorio<EnlaceUnion>
	{
        public String obtenerCodigoInvitacion(int idGrupo);
	
    }

}

