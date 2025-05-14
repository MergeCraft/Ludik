using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
	public class PerfilEstudiante : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int avatarGrupoId;

        public String enlaceAvatar;

        public int metaCalificacion;

        [ForeignKey(nameof(Estudiante))]
        public int EstudianteId { get; set; } 

        public int monedas;

        public List<Medalla> medallasObtenidas;

        public List<RendimientoPeriodo> historialRendimientoPeriodos;

        [ForeignKey(nameof(Grupo))]
        public int GrupoId { get; set; }

        public List<Recompensa> inventario;

        public BarraProgreso barraProgreso;

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

