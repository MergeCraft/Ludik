using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.TablaClasificacionDTOs;
using LogicaAplicacion.DTOsMappers.TablaClasificacionMappers;
using LogicaAplicacion.InterfacesCasosUsos.TablaClasificacion;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.TablaClasificacion
{
    public class ObtenerTodasLasTablasClasificacionGrupo:IObtenerTodasLasTablasClasificacionGrupo
    {
        private readonly IRepositorioTablasClasificacion _repo;
        private readonly IRepositorioGrupos _repositorioGrupos;

        public ObtenerTodasLasTablasClasificacionGrupo(IRepositorioTablasClasificacion repo, IRepositorioGrupos repositorioGrupos)
        {
            _repo = repo;
            _repositorioGrupos = repositorioGrupos;
        }
        public async Task<Resultado<IEnumerable<TablaClasificacionInfoDto>>> EjecutarAsync(int grupoId,string profesorId)
        {
            var grupos = await _repositorioGrupos.ObtenerGruposPorProfesorId(profesorId);
            if (!grupos.Any(g => g.Id == grupoId))
            {
                return Resultado<IEnumerable<TablaClasificacionInfoDto>>.Falla(
                    new Error("Grupo.NoPertenece", "El grupo no pertenece al profesor"));
            }

            var res = await _repo.GetAllByAsync(grupoId);
            if (res.EsFallo)
                return Resultado<IEnumerable<TablaClasificacionInfoDto>>.Falla(res.Errores);

            var tablas = res.Valor;

            foreach (var tabla in tablas)
            {
                tabla.OrdenarParticipantesPorMedallaAsociada();
            }

            var dtos = tablas
                .Select(TablaClasificacionInfoMapper.Map)
                .ToList();

            return Resultado<IEnumerable<TablaClasificacionInfoDto>>.Exitoso(dtos);
        }

    }
}
