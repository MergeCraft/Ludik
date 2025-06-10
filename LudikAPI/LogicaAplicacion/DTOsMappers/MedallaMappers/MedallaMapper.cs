using Dominio;
using LogicaAplicacion.DTOs.MedallaDTOs;

namespace LogicaAplicacion.DTOsMappers.MedallaMappers;

public class MedallaMapper
{
    public static MedallaDto toDto(Medalla medalla)
    {
        return new MedallaDto
        {
            UrlImagen = medalla.Icono,
            Nombre = medalla.Nombre,
            Descripcion = medalla.Descripcion,
            CantidadMedallasBrinda = medalla.MonedasOtorgadas,
            EsAsignacionMutua = medalla.TieneAsignacionMutua
        };
    }

    public static Medalla fromDto(MedallaDto dto)
    {

        return new Medalla
        {
            Id = dto.Id,
            Icono = dto.UrlImagen,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            MonedasOtorgadas = dto.CantidadMedallasBrinda,
            TieneAsignacionMutua = dto.EsAsignacionMutua
        };

    }
}
