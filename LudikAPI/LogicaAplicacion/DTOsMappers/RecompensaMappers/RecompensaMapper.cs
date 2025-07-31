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
        public static RecompensaDto ToDto(Recompensa recompensa)
        { 
            if (recompensa == null) return null!; 

            return new RecompensaDto
            {
                Id = recompensa.Id,
                Nombre = recompensa.Nombre,
                Precio = recompensa.Precio,
                RequiereImagen = recompensa.RequiereImagen,
                NombreImagenCompleta = recompensa.NombreImagenCompleta,
                NombreImagenMiniatura = recompensa.NombreImagenMiniatura
            };
        }
    }
}
