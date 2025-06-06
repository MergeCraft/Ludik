using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using LogicaAplicacion.DTOs.SolocitudUnionDTOs;

namespace LogicaAplicacion.DTOsMappers.SolicitudMappers
{
    public static class SolicitudUnionListadoMapper
    {
        public static SolicitudUnionListadoDto Mapear(SolicitudUnion solicitud)
        {
            return new SolicitudUnionListadoDto
            {
                IdEstudiante = solicitud.estudianteId,
                fecha = solicitud.fecha
            };
        }

        public static List<SolicitudUnionListadoDto> MapearLista(IEnumerable<SolicitudUnion> solicitudes)
        {
            return solicitudes.Select(Mapear).ToList();
        }
    }
}
