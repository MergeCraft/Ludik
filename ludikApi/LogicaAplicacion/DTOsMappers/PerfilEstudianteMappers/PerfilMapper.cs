using LogicaNegocio.Entidades;
using LogicaAplicacion.DTOs.PerfilEstudianteDTO;
using LogicaAplicacion.DTOsMappers.MedallaMappers;

public static class PerfilMapper
{
    public static PerfilConMedallasDto ToDtoConMedallas(PerfilEstudiante perfil)
    {
        if (perfil == null) return null;

        return new PerfilConMedallasDto
        {
            Id = perfil.Id,
            EnlaceAvatar = perfil.RutaImagenMiniatura,
            MetaCalificacion = perfil.MetaCalificacion,
            EstudianteId = perfil.EstudianteId,
            NombreEstudiante = perfil.Estudiante.NombreCompleto.Nombre,
            Monedas = perfil.Monedas,
            GrupoId = perfil.GrupoId,
            NombreGrupo = perfil.Grupo.Nombre,
            Medallas = MedallaCantidadMapper.AgruparMedallas(
                perfil.PerfilMedallas.Select(pm => pm.Medalla).ToList()
            )
        };
    }
}
