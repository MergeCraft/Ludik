using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public int TablaEquivalenciaId { get; set; }

        [Required]
        [ForeignKey(nameof(TablaEquivalenciaId))]
        public TablaEquivalencia TablaEquivalencia { get; set; }

        public Tienda Tienda { get; set; }

        public List<TablaClasificacion> TablasClasificacion { get; set; }

        public List<PerfilEstudiante> Alumnos { get; set; }

        public List<SolicitudUnion> Solicitudes { get; set; }

        public int EnlaceUnionId { get; set; }

        [ForeignKey("EnlaceUnionId")]
        public EnlaceUnion EnlaceUnion { get; set; }

        [ForeignKey(nameof(Profesor))]
        public int ProfesorId { get; set; } 

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

