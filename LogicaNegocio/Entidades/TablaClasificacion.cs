
using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class TablaClasificacion : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private String nombre;

		private Medalla medallaAsociada;

		private List<PerfilEstudiante> participantes;

		private PerfilEstudiante[] perfilEstudiante;

		private Medalla medalla;

        /// <see>vistaCompleta.observer.Observador#actualizar(vistaCompleta.observer.Observable, vistaCompleta.observer.Evento)</see>
        ///  
        public void actualizar()
		{

		}

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

