using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.Resultados;

namespace Dominio
{
	public class Grupo : IEntity, IValidable
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre del grupo debe tener entre 3 y 30 caracteres.")]
        public String nombre { get; set; }

        public String institucion { get; set; }

        public String materia { get; set; }

        public DateTime fCreacion { get; set; }
        [Required]
        public TablaEquivalencia tablaEquivalencia { get; set; }

        public Tienda tienda { get; set; }

        public List<TablaClasificacion> tablasClasificacion { get; set; }

        public List<PerfilEstudiante> alumnos { get; set; }

        public List<SolicitudUnion> solicitudes { get; set; }

        public EnlaceUnion enlaceUnion { get; set; }

        [ForeignKey(nameof(Profesor))]
        public string ProfesorId { get; set; } // Clave for�nea

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

        public Resultado esValido()
        {
            var errores = new List<Error>();

            if (string.IsNullOrWhiteSpace(nombre) || nombre.Length < 3 || nombre.Length > 30)
                errores.Add(new Error("Grupo.Nombre", "El nombre del grupo debe tener entre 3 y 30 caracteres."));

            if (tablaEquivalencia == null)
                errores.Add(new Error("Grupo.TablaEquivalencia", "La tabla de equivalencia es obligatoria."));

            if (string.IsNullOrWhiteSpace(ProfesorId))
                errores.Add(new Error("Grupo.ProfesorId", "El identificador del profesor es obligatorio."));

            if (errores.Any())
                return Resultado.Falla(errores);

            return Resultado.Exitoso();
        }
    }

}

