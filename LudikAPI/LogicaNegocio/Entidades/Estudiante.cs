using Dominio;
using System.Collections.Generic;
using System;

namespace Dominio
{
	public class Estudiante : Usuario
	{
        public List<PerfilEstudiante> perfiles { get; set; }

        public List<Hito> hitos { get; set; }

        public List<PreguntaRespuestaSeguridad> preguntasSeguridad { get; set; }
        

        public Boolean constrastarRespuestas(PreguntaRespuestaSeguridad pRS)
		{
			return true;
		}


		public void actualizar()
		{

		}

        protected override string getTipoUsuario()
        {
            return "Estudiante";
        }

    }

}

