using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogicaNegocio.Entidades
{
	public class Profesor: Usuario
	{

        public List<Medalla> Medallas { get; set; }

        public List<TablaEquivalencia> TablasEquivalencia { get; set; }

        public List<Grupo> Grupos { get; set; }

        public void asignarMedalla(Medalla medalla, Grupo grupo, PerfilEstudiante pEstudiante)
		{

		}

    }

}

