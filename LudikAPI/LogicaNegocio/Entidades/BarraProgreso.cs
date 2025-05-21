using Dominio;
using LogicaNegocio.InterfacesEntidades;
using System.Collections.Generic;

namespace Dominio
{
	public class BarraProgreso : IEntity, IValidable
    {
        public int Id { get; set; }

        public int valorMin { get; set; }

        public int valorMax { get; set; }

        public TablaEquivalencia tablaEquivalencia { get; set; }

        public void EsValido()
        {
            throw new NotImplementedException();
        }

        public int hallarPosicionActual(List<Medalla> medallas)
		{
			return 0;
		}

		public int hallarValorMax(TablaEquivalencia te)
		{
			return 0;
		}

		public int hallarValorMin(TablaEquivalencia te)
		{
			return 0;
		}

	}

}

