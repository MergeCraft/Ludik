using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Tienda : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private List<Recompensa> Recompesas;

		private Recompensa[] recompensa;

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

