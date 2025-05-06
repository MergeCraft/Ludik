using Dominio;
using System;
using System.Collections.Generic;

namespace Dominio
{
	public class Profesor : Usuario
	{
		private String correo;

		private List<Medalla> medallas;

		private List<TablaEquivalencia> tablasEquivalencia;

		private List<Grupo> grupos;

		private Medalla[] medalla;

		private TablaEquivalencia[] tablaEquivalencia;

		private Grupo[] grupo;

		public void asignarMedalla(Medalla medalla, Grupo grupo, PerfilEstudiante pEstudiante)
		{

		}

	}

}

