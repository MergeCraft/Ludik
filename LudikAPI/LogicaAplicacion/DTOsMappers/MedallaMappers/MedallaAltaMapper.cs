using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaAplicacion.DTOs.MedallaDTOs;

namespace LogicaAplicacion.DTOsMappers.MedallaMappers
{
    public class MedallaAltaMapper
    {

        public static MedallaAltaDto toDto(string urlImagen, string nombre, string descripcion,
            int cantidadMedallasBrinda, bool esAsignacionMutua)
        {
            return new MedallaAltaDto
            {
                UrlImagen = urlImagen,
                Nombre = nombre,
                Descripcion = descripcion,
                CantidadMonedasBrinda = cantidadMedallasBrinda,
                EsAsignacionMutua = esAsignacionMutua
            };
        }

        public static Medalla fromDto(MedallaAltaDto dto)
        {

            return new Medalla{
                Icono = dto.UrlImagen,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                MonedasOtorgadas = dto.CantidadMonedasBrinda,
                TieneAsignacionMutua = dto.EsAsignacionMutua
            };

        }
    }













}
