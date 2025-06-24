using LogicaAplicacion.DTOs.AvatarDTOs;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObjects;

namespace LogicaAplicacion.DTOsMappers;

public class AvatarMappers
{
    /// <summary>
    /// Convierte una entidad de dominio Avatar a un AvatarDto.
    /// Se utiliza para enviar datos hacia el exterior (ej. respuestas de API).
    /// </summary>
    /// <param name="avatar">La entidad de dominio.</param>
    /// <returns>Un objeto de transferencia de datos (DTO).</returns>
    public static AvatarDto toDto(Avatar avatar)
    {
        if (avatar == null) return null;

        return new AvatarDto
        {
            Id = avatar.Id,

            // Atributos Generales
            Nombre = avatar.Nombre,
            ColorFondo = avatar.ColorFondo,
            Voltear = avatar.Voltear,
            Rotacion = avatar.Rotacion,
            Zoom = avatar.Zoom,

            // Atributos Faciales
            ColorPiel = avatar.ColorPiel,
            Cejas = avatar.Cejas,
            Ojos = avatar.Ojos,
            Boca = avatar.Boca,

            // Atributos de Vello Facial
            Barba = avatar.Barba,
            ColorBarba = avatar.ColorBarba,
            ProbabilidadBarba = avatar.ProbabilidadBarba,

            // Atributos de Accesorios y Pelo
            Gorro = avatar.Gorro,
            ColorSombrero = avatar.ColorSombrero,
            Pelo = avatar.Pelo,
            ColorPelo = avatar.ColorPelo,
            Gafas = avatar.Gafas,
            ColorGafas = avatar.ColorGafas,
            ProbabilidadGafas = avatar.ProbabilidadGafas,

            // Atributos de Vestimenta
            Ropa = avatar.Ropa,
            ColorRopa = avatar.ColorRopa,
            LogoRopa = avatar.LogoRopa
        };
    }

    /// <summary>
    /// Convierte un AvatarDto a una entidad de dominio Avatar.
    /// Se utiliza para procesar datos que vienen del exterior (ej. solicitudes de API).
    /// NO se debe usar para actualizar una entidad existente directamente desde la BD, solo para crear una nueva o transferir datos.
    /// </summary>
    /// <param name="dto">El objeto de transferencia de datos.</param>
    /// <returns>Una entidad de dominio Avatar.</returns>
    public static Avatar fromDto(AvatarDto dto)
    {
        if (dto == null) return null;

        return new Avatar
        {
            Id = dto.Id,

            // Atributos Generales
            Nombre = dto.Nombre,
            ColorFondo = dto.ColorFondo,
            Voltear = dto.Voltear,
            Rotacion = dto.Rotacion,
            Zoom = dto.Zoom,

            // Atributos Faciales
            ColorPiel = dto.ColorPiel,
            Cejas = dto.Cejas,
            Ojos = dto.Ojos,
            Boca = dto.Boca,

            // Atributos de Vello Facial
            Barba = dto.Barba,
            ColorBarba = dto.ColorBarba,
            ProbabilidadBarba = dto.ProbabilidadBarba,

            // Atributos de Accesorios y Pelo
            Gorro = dto.Gorro,
            ColorSombrero = dto.ColorSombrero,
            Pelo = dto.Pelo,
            ColorPelo = dto.ColorPelo,
            Gafas = dto.Gafas,
            ColorGafas = dto.ColorGafas,
            ProbabilidadGafas = dto.ProbabilidadGafas,

            // Atributos de Vestimenta
            Ropa = dto.Ropa,
            ColorRopa = dto.ColorRopa,
            LogoRopa = dto.LogoRopa
        };
    }
}