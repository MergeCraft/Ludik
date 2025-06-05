using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.MedallaDTOs
{
    public class MedallaAltaDto
    {

        public string UrlImagen { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CantidadMedallasBrinda { get; set; }
        public bool EsAsignacionMutua { get; set; }
        
    }
}
