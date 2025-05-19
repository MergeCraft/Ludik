using System;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class EnlaceUnion : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public String codigoBase;

        public DateTime expiracion;

        public void EsValido()
        {
            throw new NotImplementedException();
        }

        public String generarCodigoBase()
		{
			return null;
		}

	}

}

