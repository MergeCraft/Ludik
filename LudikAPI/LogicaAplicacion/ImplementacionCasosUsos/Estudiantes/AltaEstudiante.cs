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

namespace LogicaAplicacion.ImplementacionCasosUsos.Estudiantes
{
    public class AltaEstudiante : IAltaEstudiante,IEncriptacion
    {
        private readonly IRepositorioEstudiantes _repositorioEstudiantes;
        public AltaEstudiante(IRepositorioEstudiantes repo)
        {
            _repositorioEstudiantes = repo;
        }
        //Pre: el usuario no se encuentra registrado en la base de datos
        //Pos: el usuario se encuentra registrado en la base de datos
        public async Task EjecutarAsync(EstudianteAltaDto estudianteAltaDto)
        {
            if (estudianteAltaDto == null)
                throw new ArgumentNullException(nameof(estudianteAltaDto), "No se puede dar de alta un estudiante si no se tienen los datos necesarios.");

            bool existe = await _repositorioEstudiantes.ExisteNombreUsuarioAsync(estudianteAltaDto.NombreUsuario);
            if (existe)
                throw new UsuarioNoValidoException("El nombre de usuario ya está en uso.");

            Estudiante estudianteNuevo = EstudianteAltaMapper.fromDto(estudianteAltaDto);
            estudianteNuevo.Contrasenia.Valor = EncriptarContrasenia(estudianteNuevo.Contrasenia.Valor);

            await _repositorioEstudiantes.AddAsync(estudianteNuevo);
        }

        public string EncriptarContrasenia(string contrasenia)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasenia);
        }
    }
}
