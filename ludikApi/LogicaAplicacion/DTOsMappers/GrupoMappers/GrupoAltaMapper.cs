using System;
using System;
using Dominio;
using LogicaAplicacion.DTOs.GrupoDTOs;

namespace LogicaAplicacion.DTOsMappers.GrupoMappers
{
    public class GrupoAltaMapper
    {
        public static GrupoAltaDto toDto(string nombre, int tablaEquivalenciaId, string profesorId, string? institucion = null, string? materia = null)
        {
            return new GrupoAltaDto
            {
                Nombre = nombre,
                TablaEquivalenciaId = tablaEquivalenciaId,
                ProfesorId = profesorId,
                Institucion = institucion,
                Materia = materia
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
                    expiracion = DateTime.UtcNow.AddDays(7)
                };
            }

            return grupo;
        }
    }
}
