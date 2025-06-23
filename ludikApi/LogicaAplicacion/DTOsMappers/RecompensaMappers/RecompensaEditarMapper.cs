using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.DTOsMappers.RecompensaMappers
{
    public class RecompensaEditarMapper
    {
        public static void Update(Recompensa entidad, RecompensaEditarDto dto)
        {
            entidad.Nombre = dto.Nombre;
            entidad.RutaImagenCompleta = dto.RutaImagenCompleta ?? entidad.RutaImagenCompleta;
            entidad.RutaImagenMiniatura = dto.RutaImagenMiniatura ?? entidad.RutaImagenMiniatura;
            entidad.Precio = dto.Precio;
        }
    }
}
