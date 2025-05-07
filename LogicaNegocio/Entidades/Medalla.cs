using System;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Medalla : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private String nombre;

		private String descripcion;

		private String icono;

		private int monedasOtorgadas;

		private Boolean asignacionMutua;

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

