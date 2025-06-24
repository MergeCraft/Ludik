using LogicaNegocio.Entidades;
using LogicaAplicacion.DTOs.MedallaDTOs;

namespace LogicaAplicacion.DTOsMappers.MedallaMappers;

public class MedallaMapper
{
    public static MedallaDto toDto(Medalla medalla)
    {
        return new MedallaDto
        {
            Id = medalla.Id,
            UrlImagen = medalla.UrlImagenMiniatura,
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
            UrlImagenMiniatura = dto.UrlImagen,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            MonedasOtorgadas = dto.CantidadMedallasBrinda,
            TieneAsignacionMutua = dto.EsAsignacionMutua
        };

    }
}
