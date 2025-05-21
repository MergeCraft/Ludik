using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public record NombreUsuario
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 20 caracteres.")]
        public string Valor { get; set; }

        private NombreUsuario() { }
        public NombreUsuario(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Length < 3 || nombre.Length > 20)
                throw new ValidationException("Nombre de usuario inv�lido debe tener entre 3-20 caracteres");

            Valor = nombre;
        }

    }

}

