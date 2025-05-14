using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.ValueObjects
{
    [ComplexType]
    public record NombreCompleto
    {
        [Required]//que es requerido
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 20 caracteres.")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El apellido debe tener entre 3 y 20 caracteres.")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]//RegularExpresion sirve para verificar que no van caracteres especiales
        public string Apellido { get; set; }

        public NombreCompleto(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }

        

    }

}

