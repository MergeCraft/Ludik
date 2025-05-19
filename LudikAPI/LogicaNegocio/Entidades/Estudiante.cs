using Dominio;
using System.Collections.Generic;
using System;

namespace Dominio
{
	public class Estudiante : Usuario
	{
        public List<PerfilEstudiante> perfiles;

        public List<Hito> hitos;

        public List<PreguntaRespuestaSeguridad> preguntasSeguridad;
        

        public Boolean constrastarRespuestas(PreguntaRespuestaSeguridad pRS)
		{
			return true;
		}


		public void actualizar()
		{

		}

	}

}

