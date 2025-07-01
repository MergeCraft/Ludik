using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.TablaClasificacionDTOs;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.DTOsMappers.TablaClasificacionMappers
{
    public class TablaClasificacionInfoMapper
    {
        public static TablaClasificacionInfoDto Map(TablaClasificacion tabla)
        {
            // Ordena usando el propio método de la entidad
            var ordenados = tabla.obtenerParticipantesOrdenadosPorCantidaDe(tabla.MedallaAsociada);

            return new TablaClasificacionInfoDto
            {
                Id = tabla.Id,
                Nombre = tabla.Nombre,
                MedallaAsociadaId = tabla.MedallaAsociadaId,
                MedallaAsociadaNombre = tabla.MedallaAsociada.Nombre,
                Participantes = ordenados
                    .Select(p => new ParticipanteTablaDto
                    {
                        PerfilEstudianteId = p.Id,
                        NombreEstudiante = p.Estudiante.NombreCompleto.Nombre, // ajústalo a tu propiedad
                        CantidadMedallas = p.MedallasObtenidas
                                                .Count(m => m.Id == tabla.MedallaAsociadaId)
                    })
                    .ToList()
            };
        }
    }
}
