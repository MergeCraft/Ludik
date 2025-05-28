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

namespace LogicaAplicacion.ImplementacionCasosUsos.Profesores
{
    public class AltaProfesor : IAltaProfesor, IEncriptacion
    {
        private readonly IRepositorioProfesores _repositorioProfesores;
        public AltaProfesor(IRepositorioProfesores repo)
        {
            _repositorioProfesores = repo;
        }
        //Pre: el usuario no se encuentra registrado en la base de datos
        //Pos: se registra el profesor en la base de datos
        public async Task EjecutarAsync(ProfesorAltaDto profesorAltaDto)
        {
            if (profesorAltaDto == null)
                throw new ArgumentNullException(nameof(profesorAltaDto), "El DTO no puede ser nulo.");

            bool existeUsuario = await _repositorioProfesores.ExisteNombreUsuarioAsync(profesorAltaDto.NombreUsuario);
            if (existeUsuario)
                throw new UsuarioNoValidoException("El nombre de usuario ya está en uso.");

            bool existeMail = await _repositorioProfesores.ExisiteMailProfesorAsync(profesorAltaDto.Email);
            if (existeMail)
                throw new UsuarioNoValidoException("El mail de profesor ya está en uso.");

            Profesor profesorNuevo = ProfesorAltaMapper.fromDto(profesorAltaDto);
            profesorNuevo.Contrasenia.Valor = EncriptarContrasenia(profesorNuevo.Contrasenia.Valor);

            await _repositorioProfesores.AddAsync(profesorNuevo);
        }

        public string EncriptarContrasenia(string contrasenia)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasenia);
        }
    }
}
