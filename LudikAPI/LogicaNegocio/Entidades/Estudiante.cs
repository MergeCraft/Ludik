using Dominio;
using System.Collections.Generic;
using System;

namespace Dominio
{
	public class Estudiante: Usuario
	{
        public List<PerfilEstudiante> Perfiles { get; set; }

        public List<Hito> Hitos { get; set; }

        public List<PreguntaRespuestaSeguridad> PreguntasSeguridad { get; set; }
        

        public Boolean constrastarRespuestas(PreguntaRespuestaSeguridad pRS)
		{
			return true;
		}




    }

}

