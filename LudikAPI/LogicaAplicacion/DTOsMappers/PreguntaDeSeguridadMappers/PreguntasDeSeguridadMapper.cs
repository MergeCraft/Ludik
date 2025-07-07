using LogicaAplicacion.DTOs.PreguntasDeSeguridadDTOs;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.DTOsMappers.PreguntaDeSeguridadMappers;

public class PreguntasDeSeguridadMapper
{
    public static PreguntasDto ToDto(IEnumerable<PreguntaRespuestaSeguridad> preguntas)
    {
        return new PreguntasDto
        {
            Preguntas = preguntas.Select(p => PreguntaDeSeguridadMapper.toDto(p)).ToList()
        };
    }
}