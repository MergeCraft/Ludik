using Dominio;
using vistaCompleta.observer;
using System.Collections.Generic;
using System;

namespace Dominio
{
	public class Estudiante : Usuario, Observador
	{
		private List<PerfilEstudiante> perfiles;

		private List<Hito> hitos;

		private List<PreguntaRespuestaSeguridad> preguntasSeguridad;

		private PerfilEstudiante[] perfilEstudiante;

		private Hito hito;

		private PreguntaRespuestaSeguridad[] preguntaRespuestaSeguridad;

		public Boolean constrastarRespuestas(PreguntaRespuestaSeguridad pRS)
		{
			return null;
		}


		/// <see>vistaCompleta.observer.Observador#actualizar(vistaCompleta.observer.Observable, vistaCompleta.observer.Evento)</see>
		///  
		public void actualizar(Observable origen, Evento evento)
		{

		}

	}

}

