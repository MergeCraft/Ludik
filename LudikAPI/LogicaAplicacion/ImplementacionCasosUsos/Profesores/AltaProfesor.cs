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
using Microsoft.AspNetCore.Identity;

namespace LogicaAplicacion.ImplementacionCasosUsos.Profesores
{
    public class AltaProfesor : IAltaProfesor
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IRepositorioProfesores _repositorioProfesores;

        public AltaProfesor(
            UserManager<Usuario> userManager,
            IRepositorioProfesores repositorioProfesores
        )
        {
            _userManager = userManager;
            _repositorioProfesores = repositorioProfesores;
        }

        // Pre: el DTO no puede ser nulo.
        // Pos: crea un nuevo usuario-Profesor en Identity y persiste sus datos extra en la tabla Profesores.
        public async Task EjecutarAsync(ProfesorAltaDto profesorAltaDto)
        {
            //TODO: Verificar metodo.
            if (profesorAltaDto == null)
                throw new ArgumentNullException(nameof(profesorAltaDto), "El DTO no puede ser nulo.");

            var profesorNuevo = ProfesorAltaMapper.fromDto(profesorAltaDto);

            var existeNombre = await _userManager.FindByNameAsync(profesorAltaDto.NombreUsuario);
            if (existeNombre != null)
                throw new InvalidOperationException("El nombre de usuario ya está en uso.");

            var existeEmail = await _userManager.FindByEmailAsync(profesorAltaDto.Email);
            if (existeEmail != null)
                throw new InvalidOperationException("El correo electrónico ya está en uso.");

            var resultadoCreacion = await _userManager.CreateAsync(profesorNuevo, profesorAltaDto.Contrasenia);
            if (!resultadoCreacion.Succeeded)
            {
                // Recolectar todos los errores de Identity y lanzarlos
                var errores = string.Join("; ", resultadoCreacion.Errors);
                throw new InvalidOperationException($"No se pudo crear el usuario: {errores}");
            }

            var rolAsignado = await _userManager.AddToRoleAsync(profesorNuevo, "Profesor");
            if (!rolAsignado.Succeeded)
            {
                // Si falla la asignación de rol,  eliminar el usuario para no dejar datos huérfanos
                await _userManager.DeleteAsync(profesorNuevo);
                var erroresRol = string.Join("; ", rolAsignado.Errors);
                throw new InvalidOperationException($"No se pudo asignar el rol: {erroresRol}");
            }

            // Persistir datos extra de la entidad Profesor en su tabla específica
            //    Recordar que Profesor hereda de Usuario, y en Identity se guardó la información básica.
            //    Aquí simplemente guardamos los campos adicionales (medallas, tablasEquivalencia, etc).
            _repositorioProfesores.Add(profesorNuevo);
        }
    }
}
