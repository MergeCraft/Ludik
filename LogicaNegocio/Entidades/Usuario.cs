using System;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
    public abstract class Usuario : IEntity, IValidable
	{
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private int imagenPerfi;

		private String nombre;

		private String nombreUsuario;

		private String apellido;

		private String contraseña;
        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

