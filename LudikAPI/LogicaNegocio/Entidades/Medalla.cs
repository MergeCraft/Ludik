using System;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Medalla : IEntity, IValidable
    {
        public int Id { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre de la medalla debe tener entre 3 y 50 caracteres.")]
        public String Nombre { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "la descripcion de la medalla debe tener entre 3 y 100 caracteres.")]
        public String descripcion { get; set; }
        [Required]
        public String icono { get; set; }

        public int monedasOtorgadas { get; set; }

        public Boolean tieneAsignacionMutua { get; set; }

        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 5 || Nombre.Length > 30)
                throw new MedallaNoValidaException("El nombre de la medalla no cumple las restricciones.");
            if (descripcion.Length > 50)
                throw new MedallaNoValidaException("La descripci�n de la medalla no puede superar los 50 caracteres.");
            if(monedasOtorgadas < 0)
                throw new MedallaNoValidaException("La cantidad de monedas otorgadas no puede ser menor a 0.");

        }
    }

}

