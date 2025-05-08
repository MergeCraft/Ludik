using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.ValueObjects
{
    [ComplexType]
    public record NombreCompleto
    {
        public NombreCompleto(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }

        public string Nombre { get; set; }

        public string Apellido{get; set;}

    }

}

