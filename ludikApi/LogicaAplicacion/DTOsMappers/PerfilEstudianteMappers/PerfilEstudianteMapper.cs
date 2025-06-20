using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaAplicacion.DTOs.PerfilEstudianteDTO;

public static class PerfilEstudianteMapper
{
    public static PerfilEstudianteInformacionDto ToDto(PerfilEstudiante perfil)
    {
        if (perfil == null) return null;

        // Construir nombre completo si NombreCompleto existe en Estudiante
       

        return new PerfilEstudianteInformacionDto
        {
            Id = perfil.Id,
            EnlaceAvatarMiniatura = perfil.RutaImagenMiniatura,
            EnlaceAvatarCompleto = perfil.RutaImagenCompleta,
            MetaCalificacion = perfil.MetaCalificacion,
            EstudianteId = perfil.EstudianteId,
            NombreEstudiante = perfil.Estudiante.NombreCompleto.Nombre,
            Monedas = perfil.Monedas,
            GrupoId = perfil.GrupoId,
            NombreGrupo = perfil.Grupo.Nombre
        };
    }
}
