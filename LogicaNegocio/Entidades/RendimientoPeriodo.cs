using System;
using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class RendimientoPeriodo : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private DateTime fInicio;

		private DateTime fFin;

		private String notaObtenida;

		private List<Medalla> medallasObtuvoEstudiante;

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

