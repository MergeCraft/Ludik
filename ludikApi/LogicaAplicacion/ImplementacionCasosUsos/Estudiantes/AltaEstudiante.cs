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
    public class AltaEstudiante : IAlta,IEncriptacion
    {
        private readonly IRepositorioEstudiantes _repositorioEstudiantes;
        public AltaEstudiante(IRepositorioEstudiantes repo)
        {
            _repositorioEstudiantes = repo;
        }
        public void Ejecutar(EstudianteAltaDto estudianteAltaDto)
        {
            if (estudianteAltaDto != null)
            {
                Estudiante estudianteNuevo = EstudianteAltaMapper.fromDto(estudianteAltaDto);
                estudianteNuevo.contrasenia.Clave = EncriptarContrasenia(estudianteNuevo.contrasenia.Clave);
                _repositorioEstudiantes.Add(estudianteNuevo);
            }
        }

        public string EncriptarContrasenia(string contrasenia)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasenia);
        }
    }
}
