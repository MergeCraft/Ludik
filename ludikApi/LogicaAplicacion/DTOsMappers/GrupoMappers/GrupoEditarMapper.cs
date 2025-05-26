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
                Nombre = grupo.nombre,
                ProfesorId = grupo.ProfesorId,
                Institucion = grupo.institucion,
                Materia = grupo.materia
            };
        }

        public static Grupo FromDto(GrupoEditarDto grupoDto)
        {
            return new Grupo
            {
                Id = grupoDto.Id,
                nombre = grupoDto.Nombre,
                ProfesorId = grupoDto.ProfesorId,
                institucion = grupoDto.Institucion,
                materia = grupoDto.Materia
            };
        }

        public static void UpdateFromDto(GrupoEditarDto grupoDto, Grupo grupoExistente)
        {
            grupoExistente.nombre = grupoDto.Nombre;
            grupoExistente.ProfesorId = grupoDto.ProfesorId;
            grupoExistente.institucion = grupoDto.Institucion;
            grupoExistente.materia = grupoDto.Materia;
        }
    }
}