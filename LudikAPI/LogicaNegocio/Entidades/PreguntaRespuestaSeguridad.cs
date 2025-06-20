using System;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public class PreguntaRespuestaSeguridad : IEntity, IValidable
    {
        public int Id { get; set; }

        public String Pregunta { get; set; }

        public String Respuesta { get; set; }
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

