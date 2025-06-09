using System;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace Dominio
{
	public class EnlaceUnion : IEntity, IValidable
    {
        public int Id { get; set; }

        public String codigoBase { get; set; }

        public DateTime expiracion { get; set; }
        public string urlCompleta { get; set; } // Este ser� el enlace listo para usar

        public EnlaceUnion() { }

        public EnlaceUnion(string urlCompleta, string codigoUnico)
        {
            this.urlCompleta = urlCompleta;
            this.codigoBase = codigoUnico;
            this.expiracion = DateTime.UtcNow.AddDays(300); 
        }




        public Resultado esValido()
        {
            var errores = new List<Error>();

            if (string.IsNullOrWhiteSpace(codigoBase))
                errores.Add(new Error("EnlaceUnion.CodigoBase", "El código del enlace no puede ser vacío."));

            if (expiracion < DateTime.UtcNow)
                errores.Add(new Error("EnlaceUnion.Expiracion", "El enlace está expirado."));

            if (errores.Any())
                return Resultado.Falla(errores);

            return Resultado.Exitoso();
        }


    }

}

