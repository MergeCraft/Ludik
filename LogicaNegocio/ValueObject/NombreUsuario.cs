using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.ValueObjects
{
    [ComplexType]
    public record NombreUsuario
    {
        public NombreUsuario(string nombre)
        {
            Nombre = nombre;
           
        }
        public string Nombre { get; set; }

    }

}

