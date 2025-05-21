using System;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Medalla : IEntity, IValidable
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la medalla debe tener entre 3 y 50 caracteres.")]
        public String Nombre { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "la descripcion de la medalla debe tener entre 3 y 100 caracteres.")]
        public String descripcion { get; set; }
        [Required]
        public String icono { get; set; }

        public int monedasOtorgadas { get; set; }

        public Boolean asignacionMutua { get; set; }

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

