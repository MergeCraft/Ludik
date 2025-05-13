using System;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Medalla : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la medalla debe tener entre 3 y 50 caracteres.")]
        public String Nombre;
        [StringLength(100, MinimumLength = 0, ErrorMessage = "la descripcion de la medalla debe tener entre 3 y 100 caracteres.")]
        public String descripcion;
        [Required]
        public String icono;

        public int monedasOtorgadas;

        public Boolean asignacionMutua;

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

