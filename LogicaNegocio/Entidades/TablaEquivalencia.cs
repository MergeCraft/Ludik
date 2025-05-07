using System;
using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class TablaEquivalencia : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public String nombre;

        public List<Equivalencia> equivalencias;

        public Equivalencia[] equivalencia;
        public int maxCalificacionSegun(List<Medalla> medallas)
		{
			return 0;
		}

		public Equivalencia siguienteEquivalencia(List<Medalla> medallas)
		{
			return null;
		}

		public int hallarValorMaxDeTabla()
		{
			return 0;
		}

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

