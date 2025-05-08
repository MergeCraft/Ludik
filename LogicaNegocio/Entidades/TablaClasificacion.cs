
using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class TablaClasificacion : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public String nombre;

        public Medalla medallaAsociada;

        public List<PerfilEstudiante> participantes;

        public PerfilEstudiante[] perfilEstudiante;

        public Medalla medalla;

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

