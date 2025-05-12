using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public record Contrasenia
    {
        public string Clave { get; set; }

        public Contrasenia(string clave)
        {
            Clave = clave;
           
        }
        

    }

}

