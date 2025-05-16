using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesEntidades
{
    public interface IVerificarContrasenia
    {
        public bool VerificarContrasenia(string contrasenia, string hash);
    }
}
