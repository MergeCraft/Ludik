using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.DTOsMappers.RecompensaMappers
{
    public static class RecompensaListadoMapper
    {
        public static RecompensaListadoDto ToDto(Recompensa entidad)
        {
            if (entidad == null) return null!; 
            return new RecompensaListadoDto
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                RutaImagenCompleta = entidad.RutaImagenCompleta,
                RutaImagenMiniatura = entidad.RutaImagenMiniatura,
                Precio = entidad.Precio,
                RequiereImagen = entidad.RequiereImagen
            };
        }
    }
}
