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
    public class ObtenerTodasLasTablasClasificacion : IObtenerTodasLasTablasClasificacion
    {
        private readonly IRepositorioTablasClasificacion _repo;

        public ObtenerTodasLasTablasClasificacion(IRepositorioTablasClasificacion repo)
        {
            _repo = repo;
        }

        public async Task<Resultado<IEnumerable<TablaClasificacionInfoDto>>> EjecutarAsync()
        {
            var res = await _repo.GetAllAsync();
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
