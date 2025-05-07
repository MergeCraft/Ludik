using System;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
    public abstract class Usuario : IEntity, IValidable
	{
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int imagenPerfi;

        public String nombre;

        public String nombreUsuario;

        public String apellido;

        public String contraseña;
        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

