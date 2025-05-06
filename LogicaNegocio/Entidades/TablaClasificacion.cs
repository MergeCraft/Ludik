using vistaCompleta.observer;
using System;
using Dominio;
using System.Collections.Generic;

namespace Dominio
{
	public class TablaClasificacion : Observador
	{
		private int id;

		private String nombre;

		private Medalla medallaAsociada;

		private List<PerfilEstudiante> participantes;

		private PerfilEstudiante[] perfilEstudiante;

		private Medalla medalla;


		/// <see>vistaCompleta.observer.Observador#actualizar(vistaCompleta.observer.Observable, vistaCompleta.observer.Evento)</see>
		///  
		public void actualizar(Observable origen, Evento evento)
		{

		}

	}

}

