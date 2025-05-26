using System;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class EnlaceUnion : IEntity, IValidable
    {
        public int Id { get; set; }

        public String codigoBase { get; set; }

        public DateTime expiracion { get; set; }

        public void EsValido()
        {
            throw new NotImplementedException();
        }
        public EnlaceUnion()
        {
            
        }
        public String generarCodigoBase()
		{
			return null;
		}

	}

}

