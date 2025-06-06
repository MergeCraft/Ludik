using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Medallas;

public class ModificarMedalla: IModificarMedalla
{
    private readonly IRepositorioMedallas _repositorioMedallas;

    public ModificarMedalla(IRepositorioMedallas repositorioMedallas)
    {
        _repositorioMedallas = repositorioMedallas;
    }

    public Task<Resultado> EjecutarAsync(MedallaAltaDto medallaAltaDto)
    {
        throw new NotImplementedException();
    }
}