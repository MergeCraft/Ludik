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
        public Email correo { get; set; }

        public List<Medalla> medallas { get; set; }

        public List<TablaEquivalencia> tablasEquivalencia { get; set; }

        public List<Grupo> grupos { get; set; }

        public void asignarMedalla(Medalla medalla, Grupo grupo, PerfilEstudiante pEstudiante)
		{

		}

	}

}

