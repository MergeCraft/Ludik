using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Avatar
    {
        // Atributos Generales
        public string Nombre { get; set; }
        public string ColorFondo { get; set; }
        public bool Voltear { get; set; }
        public int Rotacion { get; set; }
        public int Zoom { get; set; }

        // Atributos Faciales
        public string ColorPiel { get; set; }
        public string Cejas { get; set; }
        public string Ojos { get; set; }
        public string Boca { get; set; }

        // Atributos de Vello Facial
        public string Barba { get; set; }
        public string ColorBarba { get; set; }
        public int ProbabilidadBarba { get; set; }

        // Atributos de Accesorios y Pelo
        public string Gorro { get; set; }
        public string ColorSombrero { get; set; }
        public string Pelo { get; set; }
        public string ColorPelo { get; set; }
        public string Gafas { get; set; }
        public string ColorGafas { get; set; }
        public int ProbabilidadGafas { get; set; }

        // Atributos de Vestimenta
        public string Ropa { get; set; }
        public string ColorRopa { get; set; }
        public string LogoRopa { get; set; }
    }
}
