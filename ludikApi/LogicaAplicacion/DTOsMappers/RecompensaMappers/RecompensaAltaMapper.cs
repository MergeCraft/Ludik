using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.DTOsMappers.RecompensaMappers
{
    public class RecompensaAltaMapper
    {
        public static RecompensaAltaDto toDto(Recompensa recompensa)
        {
            return new RecompensaAltaDto
            {
                Nombre = recompensa.Nombre,
                RutaImagenCompleta = recompensa.RutaImagenCompleta,
                RutaImagenMiniatura = recompensa.RutaImagenMiniatura,
                Precio = recompensa.Precio,
            };
        }

        public static Recompensa fromDto(RecompensaAltaDto dto, Tienda tienda)
        {
            return new RecompensaSimple
            {
                Nombre = dto.Nombre,
                RutaImagenCompleta = dto.RutaImagenCompleta ?? "",
                RutaImagenMiniatura = dto.RutaImagenMiniatura ?? "",
                Precio = dto.Precio,
                Tienda = tienda,
                TiendaId = tienda.Id
            };
        }
    }
}
