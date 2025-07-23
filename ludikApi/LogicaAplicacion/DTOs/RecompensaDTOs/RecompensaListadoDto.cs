using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.RecompensaDTOs
{
    public class RecompensaDto
    {
		public int Id { get; set; }
		public string Nombre { get; set; }
        public string EnlaceImagenCompleta { get; set; }
        public string EnlaceImagenMiniatura { get; set; }
        public int Precio { get; set; }
        public bool RequiereImagen { get; set; }
    }
}
