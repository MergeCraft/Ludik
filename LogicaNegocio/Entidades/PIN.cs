using System;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class PIN : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int idUsuario;

        public String pin;

        public DateTime fCreacion;

        public DateTime fExpiracion;

        public int tiempoDeVida;

        public Boolean fueUtilizado;
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

