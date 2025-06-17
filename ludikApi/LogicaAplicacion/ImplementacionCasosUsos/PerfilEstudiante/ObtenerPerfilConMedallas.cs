using LogicaAplicacion.DTOs.PerfilEstudianteDTO;
using LogicaNegocio.Resultados;
using InterfacesRepositorio;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogicaAplicacion.DTOsMappers.MedallaMappers;



public class ObtenerPerfilConMedallas :IObtenerPerfilConMedallas
{
    private readonly IRepositorioPerfilEstudianteGrupo _repoPerfilGrupo;
    public ObtenerPerfilConMedallas(IRepositorioPerfilEstudianteGrupo repoPerfilGrupo)
    {
        _repoPerfilGrupo = repoPerfilGrupo;
    }

    public async Task<Resultado<PerfilConMedallasDto>> EjecutarAsync(string estudianteId, int grupoId)
    {
        // 1. Obtener perfil con medallas
        var resultadoPerfil = await _repoPerfilGrupo.GetByEstudianteYGrupoConMedallasAsync(estudianteId, grupoId);
        if (!resultadoPerfil.EsExitoso)
            return Resultado<PerfilConMedallasDto>.Falla(resultadoPerfil.Errores);

        var perfil = resultadoPerfil.Valor;

        // 2. Mapear a DTO con agrupamiento
        var dto = PerfilMapper.ToDtoConMedallas(perfil);

        return Resultado<PerfilConMedallasDto>.Exitoso(dto);
    }
}
