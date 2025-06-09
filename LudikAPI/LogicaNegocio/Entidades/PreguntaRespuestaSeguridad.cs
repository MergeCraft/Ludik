using System;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace Dominio
{
	public class PreguntaRespuestaSeguridad : IEntity, IValidable
    {
        public int Id { get; set; }

        public String pregunta { get; set; }

        public String respuesta { get; set; }
        public bool coincide(PreguntaRespuestaSeguridad pRS)
		{
			return true;
		}

        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
    }

}

