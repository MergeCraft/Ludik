using LogicaAplicacion.DTOs.AvatarDTOs;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.InterfacesCasosUsos.Avatar;

public interface IObtenerAtributosAvatarDisponiblesParaPerfil
{
    public Task<Resultado<IEnumerable<AtributosDisponiblesAvatarDto>>> EjecutarAsync(int idPerfilEstudiante);
}