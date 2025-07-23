using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.DTOsMappers.RecompensaMappers
{
    public static class RecompensaMapper
    {
        public static RecompensaDto ToDto(Recompensa entidad)
        { 
            if (entidad == null) return null!; 

            return new RecompensaDto
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                EnlaceImagenCompleta = entidad.NombreImagenCompleta,
                EnlaceImagenMiniatura = entidad.NombreImagenMiniatura,
                Precio = entidad.Precio,
                RequiereImagen = entidad.RequiereImagen
            };
        }
    }
}
