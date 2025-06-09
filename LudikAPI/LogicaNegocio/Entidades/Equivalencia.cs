using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace Dominio
{
	public class Equivalencia : IEntity, IValidable
    {
        public int Id { get; set; }

        public int nota { get; set; }

        public List<Medalla> medallasNecesarias { get; set; }

        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
    }

}

