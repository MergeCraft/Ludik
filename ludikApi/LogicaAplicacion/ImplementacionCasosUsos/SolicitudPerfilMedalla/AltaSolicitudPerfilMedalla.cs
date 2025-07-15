using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.SolicitudPerfilMedallaDTOs;
using LogicaAplicacion.DTOsMappers.SolicitudPerfilMedallaMappers;
using LogicaAplicacion.InterfacesCasosUsos.SolicitudPerfilMedalla;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.SolicitudPerfilMedalla
{
    public class AltaSolicitudPerfilMedalla : IAltaSolicitudPerfilMedalla
    {
        private readonly IRepositorioSolicitudPerfilMedalla _repoSolicitudesPerfilMedalla;
        private readonly IRepositorioPerfilEstudianteGrupo _repoPerfilEstudianteGrupo;
        private readonly IRepositorioMedallas _repoMedallas;

        public AltaSolicitudPerfilMedalla(IRepositorioSolicitudPerfilMedalla repoSolicitudesPerfilMedalla,IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudianteGrupo,IRepositorioMedallas repositorioMedallas)
        {
            _repoSolicitudesPerfilMedalla = repoSolicitudesPerfilMedalla;
            _repoPerfilEstudianteGrupo = repositorioPerfilEstudianteGrupo;
            _repoMedallas = repositorioMedallas;

        }
        public async Task<Resultado> EjecutarAsync(AltaSolicitudPerfilMedallaDto dto)
        {
            var perfilResultado = await _repoPerfilEstudianteGrupo.GetByIdAsync(dto.PerfilEstudianteId);
            if (perfilResultado.EsFallo)
                return Resultado.Falla(new Error("Error.NotFound",
                    $"No existe perfil con Id {dto.PerfilEstudianteId}."));

            var perfil = perfilResultado.Valor!;

            var medallaResultado = await _repoMedallas.GetByIdAsync(dto.MedallaId);
            if (medallaResultado.EsFallo)
                return Resultado.Falla(new Error("Error.NotFound",
                    $"No existe medalla con Id {dto.MedallaId}."));

            if (perfil.GrupoId != dto.GrupoId)
                return Resultado.Falla(new Error("Error.Forbidden",
                    "El perfil de estudiante no pertenece a ese grupo."));

            var solicitud = AltaSolicitudPerfilMedallaMapper.Map(dto);

            var validacion = solicitud.esValido();
            if (validacion.EsFallo)
                return validacion;

            var addResultado = await _repoSolicitudesPerfilMedalla.AddAsync(solicitud);
            if (addResultado.EsFallo)
                return addResultado;

            return Resultado.Exitoso();
        }
    }
}
