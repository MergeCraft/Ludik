using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;
using LogicaNegocio.ValueObject;

namespace LogicaNegocio.Entidades
{
	public class RendimientoPeriodo : IEntity, IValidable
    {
        public int Id { get; set; }

        public RangoFechas Rangofecha { get; set; }

        public String NotaObtenida { get; set; }

        public List<Medalla> MedallasObtuvoEstudiante { get; set; }

        [ForeignKey(nameof(PerfilEstudiante))]
        public int PerfilEstudianteId { get; set; } // Clave for�nea


        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
    }

}

