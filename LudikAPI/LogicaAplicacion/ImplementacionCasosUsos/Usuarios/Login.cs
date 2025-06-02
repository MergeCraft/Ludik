using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.DTOsMappers.UsuarioMappers;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
using LogicaNegocio.Excepciones;
using LoginRespuestaDto = LogicaAplicacion.DTOs.UsuarioDTOs.LoginRespuestaDto;

namespace LogicaAplicacion.ImplementacionCasosUsos.Usuarios
{
    public class Login : ILogin
    {
        private IRepositorioUsuarios _repositorioUsuarios;
        public Login(IRepositorioUsuarios repo)
        {
            _repositorioUsuarios = repo;
        }

        //Pre: el usuario no se encuentra logueado pero si registrado en la base de datos
        //Pos: el usuario se loguea en el sistema 
        public async Task<LoginRespuestaDto> Ejecutar(string nombreUsuario, string psw)
        {
            if (psw == null)
                throw new ArgumentNullException(nameof(psw), "La contraseña no puede ser vacia.");

            var usr = await _repositorioUsuarios.GetUsuarioPorNombreAsync(nombreUsuario);
            if (usr == null)
                throw new UsuarioNoValidoException("Nombre de usuario incorrecto.");

            var esValida = await _repositorioUsuarios.VerificarContrasenaAsync(usr, psw);
            if (!esValida)
                throw new ContraseniaNoValidaException("Contraseña incorrecta.");

            var roles = await _repositorioUsuarios.GetRolesAsync(usr);
            if (roles == null || !roles.Any())
                throw new Exception("El usuario no tiene un rol asignado.");

            // Se toma el primer rol 
            string rolAsignado = roles.First();



            return new LoginRespuestaDto
            {
                Id = usr.Id,
                NombreUsuario = usr.UserName,
                Rol = rolAsignado
                // Token se genera posteriormente en el controlador
            };
        }

    }
}
