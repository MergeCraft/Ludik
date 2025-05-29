using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.DTOsMappers.EstudianteMappers;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesEntidades;
using Microsoft.AspNetCore.Identity;

namespace LogicaAplicacion.ImplementacionCasosUsos.Estudiantes
{
    public class AltaEstudiante : IAltaEstudiante
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IRepositorioEstudiantes _repositorioEstudiantes;

        public AltaEstudiante(
            UserManager<Usuario> userManager,
            IRepositorioEstudiantes repositorioEstudiantes
        )
        {
            _userManager = userManager;
            _repositorioEstudiantes = repositorioEstudiantes;
        }

        // Pre:  el DTO no puede ser nulo.
        // Pos:  crea un nuevo usuario-Estudiante en Identity y persiste sus datos extra en la tabla Estudiantes.
        public async Task EjecutarAsync(EstudianteAltaDto estudianteAltaDto)
        {
            if (estudianteAltaDto == null)
                throw new ArgumentNullException(nameof(estudianteAltaDto),
                    "No se puede dar de alta un estudiante si no se tienen los datos necesarios.");

            var estudianteNuevo = EstudianteAltaMapper.fromDto(estudianteAltaDto);

            var existeNombre = await _userManager.FindByNameAsync(estudianteAltaDto.NombreUsuario);
            if (existeNombre != null)
                throw new InvalidOperationException("El nombre de usuario ya está en uso.");

            var resultadoCreacion = await _userManager.CreateAsync(estudianteNuevo, estudianteAltaDto.Contrasenia);
            if (!resultadoCreacion.Succeeded)
            {
                var errores = string.Join("; ", resultadoCreacion.Errors);
                throw new InvalidOperationException($"No se pudo crear el usuario: {errores}");
            }

            var rolAsignado = await _userManager.AddToRoleAsync(estudianteNuevo, "Estudiante");
            if (!rolAsignado.Succeeded)
            {
                // Si falla la asignación de rol, eliminamos el usuario para no dejar datos huérfanos
                await _userManager.DeleteAsync(estudianteNuevo);
                var erroresRol = string.Join("; ", rolAsignado.Errors);
                throw new InvalidOperationException($"No se pudo asignar el rol: {erroresRol}");
            }

            _repositorioEstudiantes.Add(estudianteNuevo);
        }
    

    }
}
