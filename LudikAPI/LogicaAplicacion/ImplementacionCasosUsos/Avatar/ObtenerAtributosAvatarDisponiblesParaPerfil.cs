using LogicaAplicacion.DTOs.AvatarDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Avatar;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Avatar;

public class ObtenerAtributosAvatarDisponiblesParaPerfil: IObtenerAtributosAvatarDisponiblesParaPerfil
{
    /// <summary>
    /// Retorna los atributos de avatar disponibles para un perfil de estudiante.
    /// <summary>
    public async Task<Resultado<IEnumerable<AtributosDisponiblesAvatarDto>>> EjecutarAsync(int idPerfilEstudiante)
    {
        throw new NotImplementedException();
    }
}