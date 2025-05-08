using Dominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;

namespace Dominio
{
	public class Profesor : Usuario
	{
        public Email correo;

        public List<Medalla> medallas;

        public List<TablaEquivalencia> tablasEquivalencia;

        public List<Grupo> grupos;

        public Medalla[] medalla;

        public TablaEquivalencia[] tablaEquivalencia;

        public Grupo[] grupo;

		public void asignarMedalla(Medalla medalla, Grupo grupo, PerfilEstudiante pEstudiante)
		{

		}

	}

}

