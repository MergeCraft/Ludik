using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class PerfilEstudiante : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

