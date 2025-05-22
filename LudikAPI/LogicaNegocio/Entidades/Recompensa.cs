using System;
using System.ComponentModel.DataAnnotations;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public abstract class Recompensa : IEntity, IValidable
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre Recompensa  debe tener entre 3 y 50 caracteres.")]
        public String Nombre { get; set; }

        public String imagen { get; set; }

        public int precio { get; set; }
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

