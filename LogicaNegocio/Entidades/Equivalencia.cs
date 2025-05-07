using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Equivalencia : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int nota;

        public List<Medalla> medallasNecesarias;

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

