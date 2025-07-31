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
        private readonly IGeneradorUrlsParaColeccionesImagenes _generadorUrlsParaColecciones;

        public ObtenerRecompensasInventarioPerfil(
            IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudiante,
            IGeneradorUrlsParaColeccionesImagenes generadorUrlsImagenes)
        {
            _repositorioPerfilEstudiante = repositorioPerfilEstudiante;
            _generadorUrlsParaColecciones = generadorUrlsImagenes;
        }
        public async Task<Resultado<List<RecompensaDto>>> EjecutarAsync(int idPerfil,string idEstudiante)
        {
            var resultadoPerfil = await _repositorioPerfilEstudiante.GetByIdAsync(idPerfil);
            var resultadoRecompensas = await _repositorioPerfilEstudiante.ObtenerRecompensasInventarioAsync(idPerfil);

            if (resultadoPerfil.EsFallo)
                return Resultado<List<RecompensaDto>>.Falla(resultadoPerfil.Errores);
            if(resultadoRecompensas.EsFallo)
                return Resultado<List<RecompensaDto>>.Falla(resultadoRecompensas.Errores);


            Entidades.PerfilEstudiante perfil = resultadoPerfil.Valor;
            IEnumerable<Entidades.Recompensa> recompensas = resultadoRecompensas.Valor;

            if (perfil.EstudianteId != idEstudiante)
                return Resultado<List<RecompensaDto>>.Falla(
                    new Error("Error.Forbidden", "No tienes permiso para acceder a este perfil."));

            List<RecompensaDto> dtos = recompensas
                .Select(r => RecompensaMapper.ToDto(r))
                .ToList();

            await _generadorUrlsParaColecciones.EjecutarProcesarUrlsAsync(dtos,
                (dto => dto.NombreImagenMiniatura, (dto, url) => dto.EnlaceImagenMiniatura = url),
                (dto => dto.NombreImagenCompleta, (dto, url) => dto.EnlaceImagenCompleta = url)
            );

            return Resultado<List<RecompensaDto>>.Exitoso(dtos);
        }

    }
}
