using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.ValueObjects
{
    [ComplexType]
    public record NombreCompleto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 20 caracteres.")]
        [RegularExpression("^[a-zA-Z������������\\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El apellido debe tener entre 3 y 20 caracteres.")]
        [RegularExpression("^[a-zA-Z������������\\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]//RegularExpresion sirve para verificar que no van caracteres especiales
        public string Apellido { get; set; }

        public NombreCompleto(string nombre, string apellido)
        {
            if (!EsNombreValido(nombre))
                throw new ValidationException("Nombre inválido");
            if (!EsNombreValido(apellido))
                throw new ValidationException("Apellido inválido");

            Nombre = nombre;
            Apellido = apellido;
        }

        private bool EsNombreValido(string valor)
        {
            return !string.IsNullOrWhiteSpace(valor) &&
                   valor.Length >= 3 && valor.Length <= 20 &&
                   valor.All(c => char.IsLetter(c) || c == ' ');
        }



    }

}

