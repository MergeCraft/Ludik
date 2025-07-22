using LogicaNegocio.Entidades;
using LogicaAplicacion.DTOs.MedallaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOsMappers.MedallaMappers
{
    public class MedallaBasicaMapper
    {
        public static MedallaBasicaDto toDto(Medalla medalla)
        {
            return new MedallaBasicaDto
            {
                Id = medalla.Id,
                UrlImagen = medalla.NombreImagenMiniatura,
                Nombre = medalla.Nombre,
            };
        }

        public static Medalla fromDto(MedallaBasicaDto dto)
        {

            return new Medalla
            {
                Id = dto.Id,
                NombreImagenMiniatura = dto.UrlImagen,
                Nombre = dto.Nombre,
            };

        }
    }
}
