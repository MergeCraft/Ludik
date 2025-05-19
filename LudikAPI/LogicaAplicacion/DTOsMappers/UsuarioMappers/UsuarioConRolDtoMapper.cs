using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using LogicaAplicacion.DTOs.UsuarioDTOs;

namespace LogicaAplicacion.DTOsMappers.UsuarioMappers
{
    public class UsuarioConRolDtoMapper
    {
        public static UsuarioConRolDto toDto(Usuario usuario)
        {
            string rol;
            if (usuario is Profesor)
            {
                rol = "Profesor";
            }
            else
            {
                rol = "Estudiante";
            }
            return new UsuarioConRolDto
            {

                NombreUsuario = usuario.NombreUsuario.Nombre,
                Rol = rol,
                Id = usuario.Id
            };
        }

    }
}
