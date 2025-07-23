using LogicaAplicacion.DTOs.ProyectoAulaColaborativoDTOs;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.DTOsMappers.ProyectoAulaColaborativoMappers
{
    public static class ProyectoAulaColaborativoReadMapper
    {
        public static ProyectoAulaColaborativoDto ToDto(this ProyectoAulaColaborativo pac)
            => new ProyectoAulaColaborativoDto
            {
                Id = pac.Id,
                GrupoId = pac.GrupoId,
                Nombre = pac.Nombre,
                Visual = pac.Visual,
                CantidadMedallasNecesarias = pac.CantidadMedallasNecesarias,
                TotalContribuciones = pac.TotalContribuciones,
                RecompensaClaseId = pac.RecompensaClaseId,
                Estado = pac.Estado
            };
    }
}
