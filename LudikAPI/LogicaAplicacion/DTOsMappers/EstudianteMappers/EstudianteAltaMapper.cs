using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaNegocio.ValueObjects;

namespace LogicaAplicacion.DTOsMappers.EstudianteMappers
{
    public class EstudianteAltaMapper
    {
        public static EstudianteAltaDto toDto(string nombreUsuario, string nombre, string apellido, string contrasenia)
        {
            return new EstudianteAltaDto
            {
                NombreUsuario = nombreUsuario,
                Nombre = nombre,
                Apellido = apellido,
                Contrasenia = contrasenia
            };
        }
        public static Estudiante fromDto(EstudianteAltaDto dto)
        {
            return new Estudiante
            {
                NombreUsuario = new NombreUsuario(dto.NombreUsuario),
                NombreCompleto = new NombreCompleto(dto.Nombre, dto.Apellido),
                Contrasenia = new Contrasenia(dto.Contrasenia)
            };
        }
    }
}
