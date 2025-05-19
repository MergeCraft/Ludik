using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace LogicaNegocio.ValueObjects
{
    [ComplexType]
    public record Email
    {
        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo no es v�lido.")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
       ErrorMessage = "El correo contiene caracteres no permitidos o formato inv�lido.")]
        public string Correo { get; private set; }

        // Constructor sin parámetros requerido por EF
        private Email() { }

        public Email(string correo)
        {
            if (!EsValido(correo))
                throw new ValidationException("El correo no cumple con los requisitos de seguridad.");

            Correo = correo;
        }

        public static bool EsValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            return Regex.IsMatch(
                correo,
                @"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
            );
        }

    }

}

