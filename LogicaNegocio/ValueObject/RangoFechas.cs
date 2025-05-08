using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObject
{
    [ComplexType]
    public record RangoFechas
    {
        public RangoFechas(DateTime fIni,DateTime fFin)
        {
            fechaInicio=fIni;
            fechaFin=fFin;
        }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
    }
}
