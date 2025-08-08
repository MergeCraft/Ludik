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
    public class ObtenerTablaClasificacion : IObtenerTablaClasificacion
    {
        private readonly IRepositorioTablasClasificacion _repoTablas;

        public ObtenerTablaClasificacion(IRepositorioTablasClasificacion repoTablas)
        {
            _repoTablas = repoTablas;
        }

        public async Task<Resultado<TablaClasificacionInfoDto>> EjecutarAsync(int tablaId)
        {
            var res = await _repoTablas.GetByIdAsync(tablaId);
            if (res.EsFallo)
                return Resultado<TablaClasificacionInfoDto>.Falla(res.Errores);

            var tabla = res.Valor;

            //solo deberia poder obtener la informacion de una tabla si el profesor id de el grupo de la tabla es igual al logueado
            if (tabla == null)
                return Resultado<TablaClasificacionInfoDto>.Falla(
                    new Error("TablaClasificacion.NoEncontrada", "No existe la tabla especificada.")
                );
            tabla.OrdenarParticipantesPorMedallaAsociada();
            var dto = TablaClasificacionInfoMapper.Map(tabla);

            return Resultado<TablaClasificacionInfoDto>.Exitoso(dto);
        }
    }
}
