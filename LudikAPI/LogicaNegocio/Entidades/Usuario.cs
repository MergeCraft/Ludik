using System;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Dominio
{
    public  class Usuario : IdentityUser, IValidable
	{


        public string ImagenPerfil { get; set; }

        [Required]
        public NombreUsuario NombreUsuario { get; set; }
       
        public void EsValido()
        {
            if (NombreUsuario == null)
                throw new Exception("Nombre de usuario requerido");

        }

    }

}

