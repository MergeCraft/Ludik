using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Grupo : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
			return true;
		}

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

