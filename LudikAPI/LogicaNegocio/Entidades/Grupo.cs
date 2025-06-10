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
        public String Nombre { get; set; }

        public String Institucion { get; set; }

        public String Materia { get; set; }

        public DateTime FCreacion { get; set; }
        [Required]
        public TablaEquivalencia TablaEquivalencia { get; set; }

        public Tienda Tienda { get; set; }

        public List<TablaClasificacion> TablasClasificacion { get; set; }

        public List<PerfilEstudiante> Alumnos { get; set; }

        public List<SolicitudUnion> Solicitudes { get; set; }

        public EnlaceUnion EnlaceUnion { get; set; }

        [ForeignKey(nameof(Profesor))]
        public string ProfesorId { get; set; } 

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

            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3 || Nombre.Length > 30)
                errores.Add(new Error("Grupo.Nombre", "El nombre del grupo debe tener entre 3 y 30 caracteres."));

            if (TablaEquivalencia == null)
                errores.Add(new Error("Grupo.TablaEquivalencia", "La tabla de equivalencia es obligatoria."));

            if (string.IsNullOrWhiteSpace(ProfesorId))
                errores.Add(new Error("Grupo.ProfesorId", "El identificador del profesor es obligatorio."));

            if (errores.Any())
                return Resultado.Falla(errores);

            return Resultado.Exitoso();
        }
    }

}

