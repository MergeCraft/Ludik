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
        //PreCondificon : el usuario no se encuentra registrado en la base de datos
        //PostCondicion : el usuario se encuentra registrado en la base de datos
        public void Ejecutar(ProfesorAltaDto profesorAltaDto)
        {
            if (profesorAltaDto == null)
                throw new ArgumentNullException(nameof(profesorAltaDto), "El DTO no puede ser nulo.");

            if (_repositorioProfesores.ExisteNombreUsuario(profesorAltaDto.NombreUsuario))
                throw new Exception("El nombre de usuario ya está en uso.");

            if(_repositorioProfesores.ExisiteMailProfesor(profesorAltaDto.Email))
                throw new Exception("El email de usuario ya está en uso.");


            Profesor profesorNuevo = ProfesorAltaMapper.fromDto(profesorAltaDto);
            profesorNuevo.Contrasenia.Clave = EncriptarContrasenia(profesorNuevo.Contrasenia.Clave);
            _repositorioProfesores.Add(profesorNuevo);
        }

        public string EncriptarContrasenia(string contrasenia)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasenia);
        }
    }
}
