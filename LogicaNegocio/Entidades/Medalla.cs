using System;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Medalla : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public String nombre;

        public String descripcion;

        public String icono;

        public int monedasOtorgadas;

        public Boolean asignacionMutua;

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

