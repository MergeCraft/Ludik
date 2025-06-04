using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Medallas;

public class ObtenerMedallaPorId: IObtenerMedallaPorId
{
    private readonly IRepositorioMedallas _repositorioMedallas;

    public ObtenerMedallaPorId(IRepositorioMedallas repositorioMedallas)
    {
        _repositorioMedallas = repositorioMedallas;
    }
    public Task<Resultado<MedallaDto>> EjecutarAsync(int idMedalla)
    {
        throw new NotImplementedException();
    }
}