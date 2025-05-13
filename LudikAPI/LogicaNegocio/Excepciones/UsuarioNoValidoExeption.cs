using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class UsuarioNoValidoExeption : Exception
    {
        public UsuarioNoValidoExeption() { }
        public UsuarioNoValidoExeption(string mensaje) : base(mensaje) { }
        public UsuarioNoValidoExeption(string mensaje, Exception ex) : base(mensaje, ex) { }
    }
}
