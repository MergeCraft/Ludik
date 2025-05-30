using System;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class EnlaceUnion : IEntity, IValidable
    {
        public int Id { get; set; }

        public String codigoBase { get; set; }

        public DateTime expiracion { get; set; }
        public string urlCompleta { get; set; } // Este será el enlace listo para usar

        private static readonly string dominioBase = "https://tusitio.com/unirse-grupo?codigo=";
        // analizando
        public static EnlaceUnion CrearNuevo()
        {
            var codigo = Guid.NewGuid().ToString("N");
            return new EnlaceUnion
            {
                codigoBase = codigo,
                urlCompleta = dominioBase + codigo,
                expiracion = DateTime.UtcNow.AddDays(7)
            };
        }
        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(codigoBase))
                throw new Exception("El código del enlace no puede ser vacío.");
            if (expiracion < DateTime.UtcNow)
                throw new Exception("El enlace está expirado.");
        }

        

    }

}

