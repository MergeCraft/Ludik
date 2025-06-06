using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.ProfesorDTOs;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.DTOsMappers.EstudianteMappers;
using LogicaAplicacion.DTOsMappers.ProfesorMappers;
using LogicaAplicacion.InterfacesCasosUsos.Profesor;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Identity;

namespace LogicaAplicacion.ImplementacionCasosUsos.Profesores
{
    public class AltaProfesor : IAltaProfesor
    {
        private readonly UserManager<Usuario> _userManager;


        public AltaProfesor(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        // Pre: el DTO no puede ser nulo.
        // Pos: crea un nuevo usuario-Profesor en Identity y persiste sus datos extra en la tabla Profesores.
        public async Task<Resultado> EjecutarAsync(ProfesorAltaDto profesorAltaDto)
        {
            if (profesorAltaDto == null)
            {
                return Resultado.Falla(new Error(
                    "Error.Validation",
                    "Los datos para el alta del profesor no pueden ser nulos."));
            }

            var erroresValidacion = new List<Error>();
            var existeNombre = await _userManager.FindByNameAsync(profesorAltaDto.NombreUsuario);
            if (existeNombre != null)
            {
                erroresValidacion.Add(new Error(
                    Error.Conflict.Codigo,
                    "El nombre de usuario ya está en uso."));
            }

            var existeEmail = await _userManager.FindByEmailAsync(profesorAltaDto.Correo);
            if (existeEmail != null)
            {
                erroresValidacion.Add(new Error(
                    Error.Conflict.Codigo,
                    "El correo electrónico ya está en uso."));
            }

            if (erroresValidacion.Any())
                return Resultado.Falla(erroresValidacion);
            

            var profesorNuevo = ProfesorAltaMapper.fromDto(profesorAltaDto);

            var resultadoCreacion = await _userManager.CreateAsync(profesorNuevo, profesorAltaDto.Contrasenia);
            if (!resultadoCreacion.Succeeded)
            {
                var erroresIdentity = resultadoCreacion.Errors
                    .Select(e => new Error("Error.Validation", e.Description));
                return Resultado.Falla(erroresIdentity);
            }


            var rolAsignado = await _userManager.AddToRoleAsync(profesorNuevo, "Profesor");
            if (!rolAsignado.Succeeded)
            {
                await _userManager.DeleteAsync(profesorNuevo);

                var erroresRol = rolAsignado.Errors
                    .Select(e => new Error("Error.Unexpected", $"Error de configuración al asignar rol: {e.Description}"));
                return Resultado.Falla(erroresRol);
            }


            return Resultado.Exitoso();
        }
    }
}
