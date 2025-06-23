using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.RecompensaDTOs
{
    public class RecompensaAltaDto
    {
        public string Nombre { get; set; }
        public string RutaImagenCompleta { get; set; }
        public string RutaImagenMiniatura { get; set; }
        public int Precio { get; set; }
    }
}
