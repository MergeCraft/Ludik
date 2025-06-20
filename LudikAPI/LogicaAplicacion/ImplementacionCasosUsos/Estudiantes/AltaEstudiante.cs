using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.DTOsMappers.EstudianteMappers;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;
using LogicaNegocio.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace LogicaAplicacion.ImplementacionCasosUsos.Estudiantes
{
    public class AltaEstudiante : IAltaEstudiante
    {
        private readonly UserManager<Usuario> _userManager;

        public AltaEstudiante(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Crea un nuevo usuario de tipo Estudiante. No lanza excepciones por fallos de validación o negocio,
        /// en su lugar, retorna un objeto Resultado que encapsula el éxito o el fallo de la operación.
        /// </summary>
        /// <param name="estudianteAltaDto">DTO con los datos para el alta.</param>
        /// <returns>Un objeto Resultado que indica el éxito o contiene los errores de la operación.</returns>
        public async Task<Resultado> EjecutarAsync(EstudianteAltaDto estudianteAltaDto)
        {

            if (estudianteAltaDto == null)
                return Resultado.Falla(new Error("Validation", "Los datos del estudiante no pueden ser nulos."));
            
            var resultadoNombre = NombreCompleto.Crear(estudianteAltaDto.Nombre, estudianteAltaDto.Apellido);
            if (resultadoNombre.EsFallo)
                return Resultado.Falla(resultadoNombre.Errores);
            
            var estudianteNuevo = EstudianteAltaMapper.fromDto(estudianteAltaDto, resultadoNombre.Valor);

            var existeNombre = await _userManager.FindByNameAsync(estudianteAltaDto.NombreUsuario);
            if (existeNombre != null)
                return Resultado.Falla(new Error("Conflict", "El nombre de usuario ya está en uso."));
            

            var resultadoCreacion = await _userManager.CreateAsync(estudianteNuevo, estudianteAltaDto.Contrasenia);
            if (!resultadoCreacion.Succeeded)
            {
                var errores = resultadoCreacion.Errors
                    .Select(e => new Error("Error.Validation", e.Description));
                return Resultado.Falla(errores);
            }

            var rolAsignado = await _userManager.AddToRoleAsync(estudianteNuevo, "Estudiante");
            if (!rolAsignado.Succeeded)
            {

                await _userManager.DeleteAsync(estudianteNuevo);

                return Resultado.Falla(new Error("Internal","Ocurrió un error crítico al procesar el registro. No se pudo asignar el rol."));
            }

            return Resultado.Exitoso();
        }


    }
}
