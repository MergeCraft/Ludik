using System;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public abstract class Recompensa : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private String nombre;

		private String imagen;

		private int precio;
        public void EsValido()
        {
            throw new NotImplementedException();
        }

        public void otorgar(PerfilEstudiante pEstudiante)
		{

		}

		public void pagar(int precio)
		{

		}

	}

}

