using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.DTOsMappers.UsuarioMappers;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;

namespace LogicaAplicacion.ImplementacionCasosUsos.Usuarios
{
    public class Login : ILogin, IVerificarContrasenia
    {
        private IRepositorioUsuarios _repositorioUsuarios;
        public Login(IRepositorioUsuarios repo)
        {
            _repositorioUsuarios = repo;
        }

        //Pre: el usuario no se encuentra logueado pero si registrado en la base de datos
        //Pos: el usuario se loguea en el sistema 
        public async Task<UsuarioConRolDto> Ejecutar(string nombreUsuario, string psw)
        {
            var usr = await _repositorioUsuarios.loginUsuario(nombreUsuario);
            if (usr == null)
                throw new UsuarioNoValidoException($"Nombre de usuario incorrecto.");

            if (!VerificarContrasenia(psw, usr.Contrasenia.Valor))
                throw new ContraseniaNoValidaException($"Contraseña incorrecta.");
                
            
            return UsuarioConRolDtoMapper.toDto(usr);
        }

      
        public bool VerificarContrasenia(string contrasenia, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(contrasenia, hash);
        }
    }
}
