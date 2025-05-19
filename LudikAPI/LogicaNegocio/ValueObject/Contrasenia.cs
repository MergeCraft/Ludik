using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public record Contrasenia
    {
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
       ErrorMessage = "La contrase�a debe tener al menos 8 caracteres, incluyendo una may�scula, una min�scula, un n�mero y un car�cter especial.")]
        public string Clave { get; set; }

        public Contrasenia(string clave)
        {
            if (!EsContraseniaValida(clave))
                throw new ValidationException("La contraseña no cumple con los requisitos de seguridad");

            Clave = clave;
        }

        private bool EsContraseniaValida(string clave)
        {
            return clave.Length >= 8 &&
                   clave.Any(char.IsUpper) &&
                   clave.Any(char.IsLower) &&
                   clave.Any(char.IsDigit) &&
                   clave.Any(c => !char.IsLetterOrDigit(c));
        }


    }

}

