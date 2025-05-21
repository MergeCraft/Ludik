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
        public int Id { get; set; }

        public RangoFechas rangofecha { get; set; }

        public String notaObtenida { get; set; }

        public List<Medalla> medallasObtuvoEstudiante { get; set; }

        [ForeignKey(nameof(PerfilEstudiante))]
        public int perfilEstudianteId { get; set; } 


        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

