using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.ValueObjects
{
    [ComplexType]
    public record NombreUsuario
    {
        public string Nombre { get; set; }

        public NombreUsuario(string nombre)
        {
            Nombre = nombre;
           
        }

    }

}

