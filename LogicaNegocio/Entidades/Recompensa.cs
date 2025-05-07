using System;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public abstract class Recompensa : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public String nombre;

        public String imagen;

        public int precio;
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

