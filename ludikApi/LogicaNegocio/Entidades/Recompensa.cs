using System;
using System.ComponentModel.DataAnnotations;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public abstract class Recompensa : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre Recompensa  debe tener entre 3 y 50 caracteres.")]
        public String Nombre;

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

