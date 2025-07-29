using InterfacesRepositorio;
using LogicaAplicacion.DTOs.TablaClasificacionDTOs;
using LogicaAplicacion.DTOsMappers.TablaClasificacionMappers;
using LogicaAplicacion.InterfacesCasosUsos.TablaClasificacion;
using Entidades = LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.TablaClasificacion
{
    public class ObtenerTodasLasTablasClasificacionGrupo:IObtenerTodasLasTablasClasificacionGrupo
    {

        private readonly IRepositorioGrupos _repositorioGrupos;
        private readonly IRepositorioTablasClasificacion _repositorioTablasClasificacion;

        public ObtenerTodasLasTablasClasificacionGrupo( 
            IRepositorioGrupos repositorioGrupos, 
            IRepositorioTablasClasificacion repositorioTablasClasificacion)
        {
            _repositorioGrupos = repositorioGrupos;
            _repositorioTablasClasificacion = repositorioTablasClasificacion;
        }
        public async Task<Resultado<IEnumerable<TablaClasificacionInfoDto>>> EjecutarAsync(int grupoId, string usuarioId)
        {

            var resultadoGrupo = await _repositorioGrupos.GetByIdAsync(grupoId);
            if (resultadoGrupo.EsFallo)
                return Resultado<IEnumerable<TablaClasificacionInfoDto>>.Falla(Error.NotFound);
            
            var grupo = resultadoGrupo.Valor;

            bool esProfesorDelGrupo = grupo.ProfesorId == usuarioId;
            bool esAlumnoDelGrupo = grupo.Alumnos.Any(a => a.EstudianteId == usuarioId);

            if (!esProfesorDelGrupo && !esAlumnoDelGrupo)
                return Resultado<IEnumerable<TablaClasificacionInfoDto>>.Falla(Error.Forbidden);
            

            var resultadoTablas = await _repositorioTablasClasificacion.GetAllByAsync(grupoId);
            if (resultadoTablas.EsFallo)
                return Resultado<IEnumerable<TablaClasificacionInfoDto>>.Falla(resultadoTablas.Errores);
            IEnumerable <Entidades.TablaClasificacion > tablas = resultadoTablas.Valor;
            foreach (var tabla in tablas)
            {
                tabla.OrdenarParticipantesPorMedallaAsociada();
            }
            var dtos = tablas.Select(TablaClasificacionInfoMapper.Map).ToList();

            return Resultado<IEnumerable<TablaClasificacionInfoDto>>.Exitoso(dtos);
        }

    }
}
