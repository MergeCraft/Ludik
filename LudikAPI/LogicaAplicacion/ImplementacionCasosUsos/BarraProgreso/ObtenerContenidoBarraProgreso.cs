using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.BarraProgresoDTOs;
using LogicaAplicacion.DTOsMappers.MedallaMappers;
using LogicaAplicacion.InterfacesCasosUsos.BarraProgreso;
using LogicaAplicacion.InterfacesCasosUsos.Imagenes;
using Entidades = LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.BarraProgreso
{
    public class ObtenerContenidoBarraProgreso: IObtenerContenidoBarraProgreso
    {
        private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilesEstudiantes;

        public ObtenerContenidoBarraProgreso(
            IRepositorioPerfilEstudianteGrupo repositorioPerfilesEstudiantes)
        {
            _repositorioPerfilesEstudiantes = repositorioPerfilesEstudiantes;
        }
        public async Task<Resultado<BarraProgresoDto>> EjecutarAsync(int perfilEstudianteId, string idUsuarioAutenticado)
        {
            var resultadoPerfil = await _repositorioPerfilesEstudiantes.GetByIdAsync(perfilEstudianteId);
            if (resultadoPerfil == null || resultadoPerfil.EsFallo)
                return Resultado<BarraProgresoDto>.Falla(Error.NotFound);

            Entidades.PerfilEstudiante perfilEstudiante = resultadoPerfil.Valor;

            if (perfilEstudiante.EstudianteId != idUsuarioAutenticado)
                return Resultado<BarraProgresoDto>.Falla(Error.Forbidden);

            Entidades.TablaEquivalencia tablaEquivalencia = perfilEstudiante.Grupo.TablaEquivalencia;
            int notaActualDelPerfil = tablaEquivalencia.MaximaCalificacionSegun(perfilEstudiante.MedallasObtenidas);
            int notaMinimaDeTablaEquivalencia = tablaEquivalencia.ObtenerNotaMinima();
            int notaMaximaDeTablaEquivalencia = tablaEquivalencia.ObtenerNotaMaxima();
            List<Entidades.Medalla> medallasNecesariasParaSiguienteNota = tablaEquivalencia.ObtenerMedallasNecesariasParaSiguienteNota(notaActualDelPerfil);
            BarraProgresoDto barraProgresoDto = new BarraProgresoDto
            {
                CalificacionActual = notaActualDelPerfil,
                CalificacionMinima = notaMinimaDeTablaEquivalencia,
                CalificacionMaxima = notaMaximaDeTablaEquivalencia,
                MedallasNecesariasParaSiguienteNota = medallasNecesariasParaSiguienteNota.Select(m => MedallaBasicaMapper.toDto(m)).ToList()
            };
            return Resultado<BarraProgresoDto>.Exitoso(barraProgresoDto);
        }
    }
}
