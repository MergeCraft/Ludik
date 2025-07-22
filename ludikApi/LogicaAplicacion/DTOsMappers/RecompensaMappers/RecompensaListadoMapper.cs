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
        public static RecompensaDto ToDto(Recompensa entidad)
        {
            if (entidad == null) return null!; 
            return new RecompensaDto
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                RutaImagenCompleta = entidad.NombreImagenCompleta,
                RutaImagenMiniatura = entidad.NombreImagenMiniatura,
                Precio = entidad.Precio,
                RequiereImagen = entidad.RequiereImagen
            };
        }
    }
}
