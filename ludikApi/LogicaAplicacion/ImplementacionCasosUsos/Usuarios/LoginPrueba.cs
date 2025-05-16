using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.DTOsMappers.UsuarioMappers;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
using LogicaNegocio.ValueObjects;

namespace LogicaAplicacion.ImplementacionCasosUsos.Usuarios
{
    public class LoginPrueba : ILogin
    {
        private IRepositorioUsuarios _repositorioUsuarios;
        public LoginPrueba(IRepositorioUsuarios repo)
        {
            _repositorioUsuarios = repo;
        }
        public UsuarioConRolDto Ejecutar(string nombreUsuario, string psw)
        {
            var usr = _repositorioUsuarios.loginUsuario(nombreUsuario,psw);
            if (usr == null )
            {
                return null;
            }
            else
            {
                return UsuarioConRolDtoMapper.toDto(usr);
            }
        }
    }
}
