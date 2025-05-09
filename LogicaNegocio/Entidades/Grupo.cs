using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
	public class Grupo : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public String nombre;

        public String ciudad;

        public String materia;

        public DateTime fCreacion;

        public TablaEquivalencia tablaEquivalencia;

        public Tienda tienda;

        public List<TablaClasificacion> tablasClasificacion;

        public List<PerfilEstudiante> alumnos;

        public List<SolicitudUnion> solicitudes;

        public EnlaceUnion enlaceUnion;

        [ForeignKey(nameof(Profesor))]
        public int profesorId { get; set; } // Clave foránea

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

