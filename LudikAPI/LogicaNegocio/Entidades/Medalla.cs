using System;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace Dominio
{
	public class Medalla : IEntity, IValidable
    {
        public int Id { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre de la medalla debe tener entre 3 y 50 caracteres.")]
        public String Nombre { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "la descripcion de la medalla debe tener entre 3 y 50 caracteres.")]
        public String Descripcion { get; set; }
        [Required]
        public String Icono { get; set; }

        public int MonedasOtorgadas { get; set; }

        public Boolean TieneAsignacionMutua { get; set; }

        public Resultado esValido()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
                return Resultado.Falla(new Error("Error.Validation", "La medalla debe de tener un nombre."));
            if (Nombre.Length < 5)
                return Resultado.Falla(new Error("Error.Validation", "El nombre de la medalla debe de tener al menos 5 caracteres."));
            if (Nombre.Length>30)
                return Resultado.Falla(new Error("Error.Validation", "El nombre de la medalla debe de tener menos de 30 caracteres."));
            if (Descripcion.Length > 50)
                return Resultado.Falla(new Error("Error.Validation", "La descripción de la medalla no puede superar los 50 caracteres."));
            if (MonedasOtorgadas < 0)
                return Resultado.Falla(new Error("Error.Validation", "La cantidad de monedas otorgadas no puede ser menor a 0."));
            
            return Resultado.Exitoso();

        }

       
    }

}

