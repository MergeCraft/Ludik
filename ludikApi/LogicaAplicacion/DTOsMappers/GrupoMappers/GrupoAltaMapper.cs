using System;
using System;
using Dominio;
using LogicaAplicacion.DTOs.GrupoDTOs;

namespace LogicaAplicacion.DTOsMappers.GrupoMappers
{
    public class GrupoAltaMapper
    {
        public static GrupoAltaDto toDto(Grupo grupo)
        {
            return new GrupoAltaDto
            {
                Nombre = grupo.nombre,
                TablaEquivalenciaId = grupo.tablaEquivalencia.Id,
                ProfesorId =grupo.ProfesorId,
                Institucion = grupo.institucion,
                Materia = grupo.materia
            };
        }
        public static Grupo fromDto(GrupoAltaDto dto, TablaEquivalencia tabla)
        {
            var grupo = new Grupo
            {
                nombre = dto.Nombre,
                tablaEquivalencia = tabla,
                ProfesorId = dto.ProfesorId,
                institucion = dto.Institucion ?? "",
                materia = dto.Materia ?? "",
                fCreacion = DateTime.Now,
                tienda = new Tienda()
            };

            // Si los datos del enlace están disponibles, creamos el EnlaceUnion
            if (!string.IsNullOrEmpty(dto.CodigoEnlace) && !string.IsNullOrEmpty(dto.UrlCompleta))
            {
                grupo.enlaceUnion = new EnlaceUnion
                {
                    codigoBase = dto.CodigoEnlace,
                    urlCompleta = dto.UrlCompleta,
                    expiracion = DateTime.UtcNow.AddDays(300)
                };
            }

            return grupo;
        }
    }
}
