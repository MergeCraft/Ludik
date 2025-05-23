using Dominio;
using LogicaAplicacion.DTOs.GrupoDTOs;

namespace LogicaAplicacion.DTOsMappers.GrupoMappers
{
    public class GrupoEditarDtoMapper
    {
        public static GrupoEditarDto toDto(Grupo grupo)
        {
            return new GrupoEditarDto
            {
                Id = grupo.Id,
                Nombre = grupo.Nombre,
                TablaEquivalenciaId = grupo.TablaEquivalenciaId,
                ProfesorId = grupo.ProfesorId,
                Institucion = grupo.Institucion,
                Materia = grupo.Materia
            };
        }

        public static Grupo FromDto(GrupoEditarDto grupoDto)
        {
            return new Grupo
            {
                Id = grupoDto.Id,
                Nombre = grupoDto.Nombre,
                TablaEquivalenciaId = grupoDto.TablaEquivalenciaId,
                ProfesorId = grupoDto.ProfesorId,
                Institucion = grupoDto.Institucion,
                Materia = grupoDto.Materia
            };
        }

        public static void UpdateFromDto(GrupoEditarDto grupoDto, Grupo grupoExistente)
        {
            grupoExistente.Nombre = grupoDto.Nombre;
            grupoExistente.TablaEquivalenciaId = grupoDto.TablaEquivalenciaId;
            grupoExistente.ProfesorId = grupoDto.ProfesorId;
            grupoExistente.Institucion = grupoDto.Institucion;
            grupoExistente.Materia = grupoDto.Materia;
        }
    }
}