using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LogicaNegocio.ValueObject
{
    [Owned]
    public record RangoFechas
    {
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        private RangoFechas() { }
        public RangoFechas(DateTime fIni,DateTime fFin)
        {
            fechaInicio=fIni;
            fechaFin=fFin;
        }
        
    }
}
