using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.SolicitudPerfilMedallaDTOs;
using LogicaAplicacion.DTOsMappers.SolicitudPerfilMedallaMappers;
using LogicaAplicacion.InterfacesCasosUsos.SolicitudPerfilMedalla;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.SolicitudPerfilMedalla
{
    public class ObtenerSolicitudesPerfilMedalla : IObtenerSolicitudPerfilMedalla 
    {
        private readonly IRepositorioSolicitudPerfilMedalla _repositorio;

        public ObtenerSolicitudesPerfilMedalla(IRepositorioSolicitudPerfilMedalla repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resultado<List<SolicitudPerfilMedallaDto>>> EjecutarAsync(int grupoId)
        {
            var resultado = await _repositorio.GetByGrupoAsync(grupoId);

            if (resultado.EsFallo)
                return Resultado<List<SolicitudPerfilMedallaDto>>.Falla(resultado.Errores);

            var dtos = resultado.Valor!.Select(SolicitudPerfilMedallaMapper.Map).ToList();
            return Resultado<List<SolicitudPerfilMedallaDto>>.Exitoso(dtos);
        }
    }
}
