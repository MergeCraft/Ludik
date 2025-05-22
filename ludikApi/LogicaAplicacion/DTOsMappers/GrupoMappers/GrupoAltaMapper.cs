using System;
using System;
using Dominio;
using LogicaAplicacion.DTOs.GrupoDTOs;

namespace LogicaAplicacion.DTOsMappers.GrupoMappers
{
    public class GrupoAltaMapper
    {
        public static GrupoAltaDto toDto(string nombre, int tablaEquivalenciaId, int profesorId, string? institucion = null, string? materia = null)
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
        public static Grupo fromDto(GrupoAltaDto dto,TablaEquivalencia tabla)
        {
            return new Grupo
            {
                Nombre = dto.Nombre,
                TablaEquivalenciaId = tabla.Id,
                TablaEquivalencia = tabla,
                ProfesorId = dto.ProfesorId,
                Institucion = dto.Institucion ?? "",
                Materia = dto.Materia ?? "",
                FCreacion = DateTime.Now 
            };
        }
    }
}
