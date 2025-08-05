using LogicaNegocio.Entidades;
using LogicaAplicacion.DTOs.PerfilEstudianteDTO;
using LogicaAplicacion.DTOsMappers.MedallaMappers;

public static class PerfilMapper
{
    public static PerfilConMedallasDto ToDtoConMedallas(PerfilEstudiante perfil)
    {
        if (perfil == null) return null;

        var dto = new PerfilConMedallasDto
        {
            Id = perfil.Id,
            EnlaceAvatar = perfil.NombreImagenMiniatura,
            MetaCalificacion = perfil.MetaCalificacion,
            EstudianteId = perfil.EstudianteId,
            NombreEstudiante = perfil.Estudiante?.NombreCompleto?.Nombre,
            Monedas = perfil.Monedas,
            GrupoId = perfil.GrupoId,
            NombreGrupo = perfil.Grupo?.Nombre,
            Medallas = MedallaCantidadMapper
                                   .AgruparMedallas(perfil.MedallasObtenidas?
                                                         .Select(pm => pm.Medalla)
                                                         .ToList())
        };

        var p = perfil.PotenciadorActivo;
        if (p != null)//&& p.EstaActivo
        {
            dto.MultiplicadorPotenciador = p.Multiplicador;
            var fin = p.FechaActivacion + p.Duracion;
            dto.TiempoRestantePotenciador = fin - DateTime.UtcNow;
        }
        else
        {
            dto.MultiplicadorPotenciador = null;
            dto.TiempoRestantePotenciador = null;
        }

        return dto;
    }

}
