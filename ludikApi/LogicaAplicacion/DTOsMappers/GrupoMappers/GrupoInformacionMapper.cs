using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using LogicaAplicacion.DTOs.GrupoDTOs;

namespace LogicaAplicacion.DTOsMappers.GrupoMappers
{
    public static class GrupoInformacionMapper
    {
        public static GrupoInformacionDto ToDto(Grupo grupo)
        {
            return new GrupoInformacionDto
            {
                Nombre = grupo.nombre,
                TablaEquivalenciaId = grupo.tablaEquivalencia.Id,
                ProfesorId = grupo.ProfesorId,
                Institucion = grupo.institucion,
                Materia = grupo.materia,
                fCreacion = grupo.fCreacion,
                UrlCompleta = grupo.enlaceUnion?.urlCompleta
            };
        }
    }
}
