using System;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Pin : IEntity, IValidable
    {
        public int Id { get; set; }

        public int idUsuario { get; set; }

        public String pin { get; set; }

        public DateTime fCreacion { get; set; }

        public DateTime fExpiracion { get; set; }

        public int tiempoDeVida { get; set; }

        public Boolean fueUtilizado { get; set; }
        public String generarPin()
		{
			return null;
		}

		public void asignarPin(int usuarioId)
		{

		}

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

