using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using LogicaAplicacion.DTOs.GrupoDTOs;

namespace LogicaAplicacion.DTOsMappers.GrupoMappers
{
    public class GrupoMapper
    {
        public static GrupoDto toDto(Grupo grupo)
        {
            return new GrupoDto
            {
                Id = grupo.Id,
                Nombre = grupo.nombre,
                ProfesorId = grupo.ProfesorId,
                Institucion = grupo.institucion,
                Materia = grupo.materia
            };

        }
        /*
        public static Grupo fromDto(GrupoDto grupo)
        {
            return new Grupo
            {
               
            }
        }

        */

    }
}
