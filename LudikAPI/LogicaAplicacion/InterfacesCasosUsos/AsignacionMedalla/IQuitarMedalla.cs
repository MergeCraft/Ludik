using LogicaNegocio.Resultados;

namespace LogicaAplicacion.InterfacesCasosUsos.AsignacionMedalla;

public interface IQuitarMedalla
{
    public Task<Resultado> EjecutarAsync(int idPerfilEstudiante, int idMedalla);
}