using Dominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
	public class Profesor : Usuario
	{
        [Required]
        public Email correo;

        public List<Medalla> medallas;

        public List<TablaEquivalencia> tablasEquivalencia;

        public List<Grupo> grupos;

		public void asignarMedalla(Medalla medalla, Grupo grupo, PerfilEstudiante pEstudiante)
		{

		}

	}

}

