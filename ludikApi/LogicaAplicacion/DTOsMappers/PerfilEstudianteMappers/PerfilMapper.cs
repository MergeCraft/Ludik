using Dominio;
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
            AvatarGrupoId = perfil.AvatarGrupoId,
            EnlaceAvatar = perfil.EnlaceAvatar,
            MetaCalificacion = perfil.MetaCalificacion,
            EstudianteId = perfil.EstudianteId,
            Monedas = perfil.Monedas,
            GrupoId = perfil.GrupoId,
            Medallas = MedallaCantidadMapper.AgruparMedallas(perfil.MedallasObtenidas)
        };
        return dto;
    }
}
