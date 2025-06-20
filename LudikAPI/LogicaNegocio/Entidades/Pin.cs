using System;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public class Pin : IEntity, IValidable
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public String Codigo { get; set; }

        public DateTime FCreacion { get; set; }

        public DateTime FExpiracion { get; set; }

        public int TiempoDeVida { get; set; }

        public Boolean FueUtilizado { get; set; }
        public String generarPin()
		{
			return null;
		}

		public void asignarPin(int usuarioId)
		{

		}

        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
    }

}

