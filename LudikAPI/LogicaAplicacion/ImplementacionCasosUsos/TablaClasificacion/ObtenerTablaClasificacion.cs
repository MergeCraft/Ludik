using InterfacesRepositorio;
using LogicaAplicacion.DTOs.TablaClasificacionDTOs;
using LogicaAplicacion.DTOsMappers.TablaClasificacionMappers;
using LogicaAplicacion.InterfacesCasosUsos.TablaClasificacion;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    new Error("Error.NotFound", "No existe la tabla especificada.")
                );


            var participantes = tabla.Grupo.Alumnos;
            if (participantes == null || !participantes.Any())
                //No seria un error, es un resultado esperado que no halla un ranking creado
                return Resultado<TablaClasificacionInfoDto>.Falla(Error.NotFound);

            participantes = participantes
                .OrderByDescending(p =>
                    p.MedallasObtenidas.Count(pm => pm.MedallaId == tabla.MedallaAsociadaId)
                )
                .ToList();

            var dto = TablaClasificacionInfoMapper.Map(tabla, participantes);

            return Resultado<TablaClasificacionInfoDto>.Exitoso(dto);
        }
    }
}
