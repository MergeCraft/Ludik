using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Medallas;

public class ObtenerTodasLasMedallas : IObtenerTodasLasMedallas
{
    private readonly IRepositorioMedallas _repositorioMedallas;

    public ObtenerTodasLasMedallas(IRepositorioMedallas repositorioMedallas)
    {
        _repositorioMedallas = repositorioMedallas;
    }
    public Task<Resultado<IEnumerable<MedallaDto>>> EjecutarAsync()
    {
        throw new NotImplementedException();
    }
}