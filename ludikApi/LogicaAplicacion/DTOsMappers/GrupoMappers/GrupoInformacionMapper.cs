using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaAplicacion.DTOs.GrupoDTOs;

namespace LogicaAplicacion.DTOsMappers.GrupoMappers
{
    public static class GrupoInformacionMapper
    {
        public static GrupoInformacionDto ToDto(Grupo grupo)
        {
            return new GrupoInformacionDto
            {
                Nombre = grupo.Nombre,
                TablaEquivalenciaId = grupo.TablaEquivalencia.Id,
                ProfesorId = grupo.ProfesorId,
                Institucion = grupo.Institucion,
                Materia = grupo.Materia,
                fCreacion = grupo.FCreacion,
                UrlCompleta = grupo.EnlaceUnion?.UrlCompleta
            };
        }
    }
}
