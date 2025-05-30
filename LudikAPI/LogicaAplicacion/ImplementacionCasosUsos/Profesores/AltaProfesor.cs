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
        public async Task<IdentityResult> EjecutarAsync(ProfesorAltaDto profesorAltaDto)
        {
            if (profesorAltaDto == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "ArgNull",
                        Description = "Se deben de brindar los datos para poder registrarse."
                    }
                );
            }

            var profesorNuevo = ProfesorAltaMapper.fromDto(profesorAltaDto);

            var erroresValidacion = new List<IdentityError>();
            var existeNombre = await _userManager.FindByNameAsync(profesorAltaDto.NombreUsuario);
            if (existeNombre != null)
            {
                erroresValidacion.Add(new IdentityError
                {
                    Code = "DuplicateUserName",
                    Description = "El nombre de usuario ya está en uso."
                });
            }

            var existeEmail = await _userManager.FindByEmailAsync(profesorAltaDto.Correo);
            if (existeEmail != null)
            {
                erroresValidacion.Add(new IdentityError
                {
                    Code = "DuplicateEmail",
                    Description = "El correo electrónico ya está en uso."
                });
            }

            if (erroresValidacion.Count > 0)
            {
                return IdentityResult.Failed(erroresValidacion.ToArray());
            }

            var resultadoCreacion = await _userManager.CreateAsync(profesorNuevo, profesorAltaDto.Contrasenia);
            if (!resultadoCreacion.Succeeded)
                return resultadoCreacion;

            var rolAsignado = await _userManager.AddToRoleAsync(profesorNuevo, "Profesor");
            if (!rolAsignado.Succeeded)
            {
                // Si falla la asignación de rol,  eliminar el usuario para no dejar datos huérfanos
                await _userManager.DeleteAsync(profesorNuevo);
                return IdentityResult.Failed(rolAsignado.Errors.ToArray());
            }
            
            try
            {
                // Persistir datos extra de la entidad Profesor en su tabla específica
                //    Recordar que Profesor hereda de Usuario, y en Identity se guardó la información básica.
                await _repositorioProfesores.AddAsync(profesorNuevo);
            }
            catch (Exception ex)
            {
                // Si falla el guardado en la tabla Profesores, revertimos el usuario
                await _userManager.DeleteAsync(profesorNuevo);

                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DbError",
                    Description = $"No se pudo persistir datos en la tabla Profesores. {ex.Message}"
                });
            }


            return IdentityResult.Success;
            
        }
    }
}
