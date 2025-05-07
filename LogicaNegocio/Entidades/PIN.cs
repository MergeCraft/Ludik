using System;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class PIN : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private int idUsuario;

		private String pin;

		private DateTime fCreacion;

		private DateTime fExpiracion;

		private int tiempoDeVida;

		private Boolean fueUtilizado;
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

