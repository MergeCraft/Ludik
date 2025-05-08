using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.ValueObjects
{
    [ComplexType]
    public record Email
    {
        public Email(string nombre)
        {
            Correro = nombre;
        }

        public string Correro { get; set; }

       

    }

}

