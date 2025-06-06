using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.InterfacesCasosUsos.Medalla;

public interface IModificarMedalla
{
    Task<Resultado> EjecutarAsync(MedallaAltaDto medallaAltaDto);
}