using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.ValueObjects
{
    [ComplexType]
    public record Email
    {
        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
       ErrorMessage = "El correo contiene caracteres no permitidos o formato inválido.")]
        public string Correro { get; set; }

        public Email(string correro)
        {
            Correro = correro;
        }
       

    }

}

