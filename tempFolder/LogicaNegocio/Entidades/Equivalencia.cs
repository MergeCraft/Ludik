using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Equivalencia : IEntity, IValidable
    {
        public int Id { get; set; }

        public int nota { get; set; }

        public List<Medalla> medallasNecesarias { get; set; }

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

