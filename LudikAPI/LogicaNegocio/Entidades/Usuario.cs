using System;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;

namespace Dominio
{
    public abstract class Usuario : IEntity, IValidable
	{
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int imagenPerfil;

        public NombreCompleto NombreCompleto { get; set; }

        public NombreUsuario nombreUsuario { get; set; }
        
        public Contrasenia contrasenia { get; set; }
        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

