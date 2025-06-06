using Dominio;
using LogicaAplicacion.DTOs.MedallaDTOs;

namespace LogicaAplicacion.DTOsMappers.MedallaMappers;

public class MedallaMapper
{
    public static MedallaDto toDto(Medalla medalla)
    {
        return new MedallaDto
        {
            UrlImagen = medalla.icono,
            Nombre = medalla.Nombre,
            Descripcion = medalla.descripcion,
            CantidadMedallasBrinda = medalla.monedasOtorgadas,
            EsAsignacionMutua = medalla.tieneAsignacionMutua
        };
    }

    public static Medalla fromDto(MedallaDto dto)
    {

        return new Medalla
        {
            Id = dto.Id,
            icono = dto.UrlImagen,
            Nombre = dto.Nombre,
            descripcion = dto.Descripcion,
            monedasOtorgadas = dto.CantidadMedallasBrinda,
            tieneAsignacionMutua = dto.EsAsignacionMutua
        };

    }
}
