using System;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class PreguntaRespuestaSeguridad : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private String pregunta;

		private String respuesta;
        public bool coincide(PreguntaRespuestaSeguridad pRS)
		{
			return true;
		}

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

