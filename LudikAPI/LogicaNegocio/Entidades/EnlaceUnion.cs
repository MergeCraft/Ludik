using System;
using LogicaNegocio.InterfacesEntidades;

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




        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(codigoBase))
                throw new Exception("El c�digo del enlace no puede ser vac�o.");
            if (expiracion < DateTime.UtcNow)
                throw new Exception("El enlace est� expirado.");
        }

        

    }

}

