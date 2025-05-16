using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public record NombreUsuario
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 20 caracteres.")]
        public string Nombre { get; set; }

        public NombreUsuario(string nombre)
        {
            Nombre = nombre;
           
        }

    }

}

