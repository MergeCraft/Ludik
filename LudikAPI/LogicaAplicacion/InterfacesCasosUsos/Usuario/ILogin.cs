using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.UsuarioDTOs;

namespace LogicaAplicacion.InterfacesCasosUsos.Usuario
{
    public interface ILogin
    {
        UsuarioConRolDto Ejecutar(string nombreUsuario, string psw);
    }
}
