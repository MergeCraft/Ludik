using System;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;

namespace Dominio
{
    public abstract class Usuario : IEntity, IValidable
	{
        public int Id { get; set; }

        public int ImagenPerfil { get; set; }
        [Required]
        public NombreCompleto NombreCompleto { get; set; }
        [Required]
        public NombreUsuario NombreUsuario { get; set; }
        [Required]
        public Contrasenia Contrasenia { get; set; }
        public void EsValido()
        {
            if (NombreCompleto == null)
                throw new Exception("Nombre completo requerido");
            if (NombreUsuario == null)
                throw new Exception("Nombre de usuario requerido");
            if (Contrasenia == null)
                throw new Exception("Contrase�a requerida");
        }
    }

}

