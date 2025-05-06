using System;
using Dominio;
using System.Collections.Generic;

namespace Dominio
{
	public class PerfilEstudiante
	{
		private int id;

		private int avatarGrupo;

		private String enlaceAvatar;

		private int metaCalificacion;

		private Estudiante estudiante;

		private int monedas;

		private List<Medalla> medallasObtenidas;

		private List<RendimientoPeriodo> historialRendimientoPeriodos;

		private Grupo grupo;

		private List<Recompensa> inventario;

		private BarraProgreso barraProgreso;

		private Estudiante estudiante;

		private RendimientoPeriodo[] rendimientoPeriodo;

		private Medalla[] medalla;

		private Recompensa[] recompensa;

		private BarraProgreso barraProgreso;

		public void asignarMedalla(Medalla m)
		{

		}

		public void quitarMedalla(Medalla m)
		{

		}

	}

}

