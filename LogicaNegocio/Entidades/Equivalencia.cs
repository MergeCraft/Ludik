using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Equivalencia : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private int nota;

		private List<Medalla> medallasNecesarias;

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

