using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObject;

namespace Dominio
{
	public class RendimientoPeriodo : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public RangoFechas rangofecha;

        public String notaObtenida;

        public List<Medalla> medallasObtuvoEstudiante;

        [ForeignKey(nameof(PerfilEstudiante))]
        public int perfilEstudianteId { get; set; } // Clave foránea


        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

