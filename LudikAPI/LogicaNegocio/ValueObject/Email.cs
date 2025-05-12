using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.ValueObjects
{
    [ComplexType]
    public record Email
    {
        public string Correro { get; set; }

        public Email(string correro)
        {
            Correro = correro;
        }
       

    }

}

