using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.DTOsMappers.UsuarioMappers;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;

namespace LogicaAplicacion.ImplementacionCasosUsos.Usuarios
{
    public class LoginPrueba : ILogin, IVerificarContrasenia
    {
        private IRepositorioUsuarios _repositorioUsuarios;
        public LoginPrueba(IRepositorioUsuarios repo)
        {
            _repositorioUsuarios = repo;
        }
        //PreCondificon : el usuario no se encuentra logueado pero si registrado en la base de datos
        //PostCondicion : el usuario se loguea en el sistema 
        public UsuarioConRolDto Ejecutar(string nombreUsuario, string psw)
        {
            var usr = _repositorioUsuarios.loginUsuario(nombreUsuario);
            if (usr != null && VerificarContrasenia(psw, usr.Contrasenia.Valor)) {

                return UsuarioConRolDtoMapper.toDto(usr);
            } else {
                return null;
            }
        }

      
        public bool VerificarContrasenia(string contrasenia, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(contrasenia, hash);
        }
    }
}
