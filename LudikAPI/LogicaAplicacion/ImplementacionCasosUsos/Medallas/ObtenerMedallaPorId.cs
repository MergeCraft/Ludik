using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaAplicacion.DTOsMappers.MedallaMappers;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Medallas;

public class ObtenerMedallaPorId : IObtenerMedallaPorId
{
	private readonly IRepositorioMedallas _repositorioMedallas;

	public ObtenerMedallaPorId(IRepositorioMedallas repositorioMedallas)
	{
		_repositorioMedallas = repositorioMedallas;
	}

	public async Task<Resultado<MedallaDto>> EjecutarAsync(int idMedalla)
	{
		var resultadoRepo = await _repositorioMedallas.GetByIdAsync(idMedalla);

		if (resultadoRepo.EsFallo)
			return Resultado<MedallaDto>.Falla(resultadoRepo.Errores);
		//validar que solo el profesor que posee esa medalla pueda ver su informacion
		var medalla = resultadoRepo.Valor;

		var medallaDto = MedallaMapper.toDto(medalla);

		return Resultado<MedallaDto>.Exitoso(medallaDto);
	}
}