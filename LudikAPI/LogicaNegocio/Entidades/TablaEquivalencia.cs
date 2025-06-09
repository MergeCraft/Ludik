using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dominio;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace Dominio
{
	public class TablaEquivalencia : IEntity, IValidable
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la Tabla debe tener entre 3 y 50 caracteres.")]
        public String Nombre { get; set; }

        public List<Equivalencia> equivalencias { get; set; }
        public int maxCalificacionSegun(List<Medalla> medallas)
		{
			return 0;
		}

		public Equivalencia siguienteEquivalencia(List<Medalla> medallas)
		{
			return null;
		}

		public int hallarValorMaxDeTabla()
		{
			return 0;
		}

        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
    }

}

