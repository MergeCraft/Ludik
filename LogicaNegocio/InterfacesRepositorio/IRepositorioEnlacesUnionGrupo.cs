using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace InterfacesRepositorio
{
	public class IRepositorioEnlacesUnionGrupo : IRepositorio<EnlaceUnion>
	{
        public void Add(EnlaceUnion unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EnlaceUnion> GetAll()
        {
            throw new NotImplementedException();
        }

        public EnlaceUnion GetById(int id)
        {
            throw new NotImplementedException();
        }

        public String obtenerCodigoInvitacion(int idGrupo)
		{
			return null;
		}

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(EnlaceUnion unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(EnlaceUnion unObjeto)
        {
            throw new NotImplementedException();
        }
    }

}

