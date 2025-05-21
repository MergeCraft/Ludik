using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
	public class PerfilEstudiante : IEntity, IValidable
    {
        public int Id { get; set; }

        public int avatarGrupoId { get; set; }

        public String enlaceAvatar { get; set; }

        public int metaCalificacion { get; set; }

        [ForeignKey(nameof(Estudiante))]
        public int EstudianteId { get; set; } 

        public int monedas { get; set; }

        public List<Medalla> medallasObtenidas { get; set; }

        public List<RendimientoPeriodo> historialRendimientoPeriodos { get; set; }

        [ForeignKey(nameof(Grupo))]
        public int GrupoId { get; set; }

        public List<Recompensa> inventario { get; set; }

        public BarraProgreso barraProgreso { get; set; }

        public void asignarMedalla(Medalla m)
		{

		}

        public void EsValido()
        {
            throw new NotImplementedException();
        }

        public void quitarMedalla(Medalla m)
		{

		}

	}

}

