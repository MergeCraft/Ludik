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
using LogicaNegocio.InterfacesEntidades;

namespace LogicaAplicacion.ImplementacionCasosUsos.Estudiantes
{
    public class AltaEstudiante : IAltaEstudiante,IEncriptacion
    {
        private readonly IRepositorioEstudiantes _repositorioEstudiantes;
        public AltaEstudiante(IRepositorioEstudiantes repo)
        {
            _repositorioEstudiantes = repo;
        }
        //PreCondificon : el usuario no se encuentra registrado en la base de datos
        //PostCondicion : el usuario se encuentra registrado en la base de datos
        public void Ejecutar(EstudianteAltaDto estudianteAltaDto)
        {
            if (estudianteAltaDto == null)
                throw new ArgumentNullException(nameof(estudianteAltaDto), "El DTO no puede ser nulo.");

            if (_repositorioEstudiantes.ExisteNombreUsuario(estudianteAltaDto.NombreUsuario))
                throw new Exception("El nombre de usuario ya está en uso.");

            Estudiante estudianteNuevo = EstudianteAltaMapper.fromDto(estudianteAltaDto);
            estudianteNuevo.contrasenia.Clave = EncriptarContrasenia(estudianteNuevo.contrasenia.Clave);
            _repositorioEstudiantes.Add(estudianteNuevo);
        }

        public string EncriptarContrasenia(string contrasenia)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasenia);
        }
    }
}
