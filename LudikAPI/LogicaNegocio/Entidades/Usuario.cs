using System;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;

namespace Dominio
{
    public abstract class Usuario : IEntity, IValidable
	{
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int imagenPerfil;
        [Required]
        public NombreCompleto NombreCompleto { get; set; }
        [Required]
        public NombreUsuario nombreUsuario { get; set; }
        [Required]
        public Contrasenia contrasenia { get; set; }
        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

