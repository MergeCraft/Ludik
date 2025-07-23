using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.DTOsMappers.RecompensaMappers;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;
using LogicaAplicacion.Servicios;
using Entidades = LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Estudiantes
{
    public class ObtenerRecompensasInventarioPerfil : IObtenerRecompensasInventarioPerfil
    {
        private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilEstudiante;
        private readonly IGeneradorUrlImagen _generadorUrlImagen;

        public ObtenerRecompensasInventarioPerfil(IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudiante,
            IGeneradorUrlImagen generadorUrlImagen)
        {
            _repositorioPerfilEstudiante = repositorioPerfilEstudiante;
            _generadorUrlImagen = generadorUrlImagen;
        }
        public async Task<Resultado<List<RecompensaDto>>> EjecutarAsync(int idPerfil,string idEstudiante)
        {
            var resultadoPerfil = await _repositorioPerfilEstudiante.GetByIdAsync(idPerfil);
            if (resultadoPerfil.EsFallo)
                return Resultado<List<RecompensaDto>>.Falla(
                    new Error("Error.NotFound", "No se encontró el perfil del estudiante especificado."));

            Entidades.PerfilEstudiante perfil = resultadoPerfil.Valor!;

            if(perfil.EstudianteId != idEstudiante)
                return Resultado<List<RecompensaDto>>.Falla(
                    new Error("Error.Forbidden", "No tienes permiso para acceder a este perfil."));

            List<RecompensaDto> dtos = perfil.InventarioRecompensas
                .Select(ir => RecompensaListadoMapper.ToDto(ir.Recompensa))
                .ToList();
          
            dtos = await AgregarUrlSasADtos(dtos);

            return Resultado<List<RecompensaDto>>.Exitoso(dtos);
        }

        private async Task<List<RecompensaDto>> AgregarUrlSasADtos(List<RecompensaDto> dtos)
        {
            var tareasDeGeneracion = new List<Task<string?>>();
            foreach (var dto in dtos)
            {
                tareasDeGeneracion.Add(_generadorUrlImagen.GenerarUrlLecturaAsync(dto.RutaImagenMiniatura));
                tareasDeGeneracion.Add(_generadorUrlImagen.GenerarUrlLecturaAsync(dto.RutaImagenCompleta));
            }

            var urlsGeneradas = await Task.WhenAll(tareasDeGeneracion);

            int i = 0;
            foreach (var dto in dtos)
            {
                dto.RutaImagenMiniatura = urlsGeneradas[i++];
                dto.RutaImagenCompleta = urlsGeneradas[i++];
            }

            return dtos;
        }
    }
}
