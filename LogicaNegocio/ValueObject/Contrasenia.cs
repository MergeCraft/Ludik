using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.ValueObjects
{
    [ComplexType]
    public record Contrasenia
    {
        public Contrasenia(string nombre)
        {
            Clave = nombre;
           
        }
        public string Clave { get; set; }

    }

}

