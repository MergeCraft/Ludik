using System;
using Dominio;
using System.Collections.Generic;

namespace Dominio
{
	public class Grupo
	{
		private int id;

		private String nombre;

		private String ciudad;

		private String materia;

		private DateTime fCreacion;

		private TablaEquivalencia tablaEquivalencia;

		private Tienda tienda;

		private List<TablaClasificacion> tablasClasificacion;

		private List<PerfilEstudiante> alumnos;

		private List<SolicitudUnion> solicitudes;

		private EnlaceUnion enlaceUnion;

		private Tienda tienda;

		private TablaEquivalencia tablaEquivalencia;

		private SolicitudUnion[] solicitudUnion;

		private TablaClasificacion[] tablaClasificacion;

		private EnlaceUnion enlaceUnion;

		public void asignarMedalla(PerfilEstudiante pEstudiante, Medalla m)
		{

		}

		public void quitarMedalla(PerfilEstudiante pEstudiante, Medalla m)
		{

		}

		public void aceptarSolicitud(Estudiante estudiante)
		{

		}

		public void denegarSolicitud()
		{

		}

		public bool estudiantePertenece(PerfilEstudiante pEstudiante)
		{
			return null;
		}

	}

}

