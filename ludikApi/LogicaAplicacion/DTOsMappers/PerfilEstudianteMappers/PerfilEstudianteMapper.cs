using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using LogicaAplicacion.DTOs.PerfilEstudianteDTO;

public static class PerfilEstudianteMapper
{
    public static PerfilEstudianteInformacionDto ToDto(PerfilEstudiante perfil)
    {
        if (perfil == null) return null;

        return new PerfilEstudianteInformacionDto
        {
            Id = perfil.Id,
            AvatarGrupoId = perfil.AvatarGrupoId,
            EnlaceAvatar = perfil.EnlaceAvatar,
            MetaCalificacion = perfil.MetaCalificacion,
            EstudianteId = perfil.EstudianteId,
            Monedas = perfil.Monedas,
            GrupoId = perfil.GrupoId,
        };
    }
}
