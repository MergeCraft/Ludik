using Dominio;
using System.Collections.Generic;
using System;

namespace Dominio
{
	public class Estudiante : Usuario
	{
		private List<PerfilEstudiante> perfiles;

		private List<Hito> hitos;

		private List<PreguntaRespuestaSeguridad> preguntasSeguridad;

		public Boolean constrastarRespuestas(PreguntaRespuestaSeguridad pRS)
		{
			return true;
		}


		/// <see>vistaCompleta.observer.Observador#actualizar(vistaCompleta.observer.Observable, vistaCompleta.observer.Evento)</see>
		///  
		public void actualizar()
		{

		}

	}

}

