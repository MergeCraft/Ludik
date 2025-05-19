using System;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;

namespace Dominio
{
    public abstract class Usuario : IEntity, IValidable
	{
        public int Id { get; set; }

        public int ImagenPerfil;
        [Required]
        public NombreCompleto NombreCompleto { get; set; }
        [Required]
        public NombreUsuario NombreUsuario { get; set; }
        [Required]
        public Contrasenia Contrasenia { get; set; }
        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

