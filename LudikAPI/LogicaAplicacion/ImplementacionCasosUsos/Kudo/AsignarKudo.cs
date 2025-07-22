using System.ComponentModel;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.KudoDTOs;
using LogicaAplicacion.DTOsMappers.KudoMappers;
using LogicaAplicacion.InterfacesCasosUsos.Kudo;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Entidades = LogicaNegocio.Entidades;

namespace LogicaAplicacion.ImplementacionCasosUsos.Kudo;

public class AsignarKudo: IAsignarKudo
{
    private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfil;
    private readonly IRepositorioEstudiantes _repositorioEstudiantes;
    private readonly IRepositorioTiposKudo _repositorioTiposKudo;
    private readonly IRepositorioUmbralesParaMedallasPorKudos _repositorioUmbralesParaMedallas;
    private readonly IUnitOfWork _unitOfWork;

    public AsignarKudo(IRepositorioPerfilEstudianteGrupo repositorioPerfil, 
        IRepositorioTiposKudo repositorioTipoKudo, 
        IRepositorioEstudiantes repositorioEstudiantes,
        IRepositorioUmbralesParaMedallasPorKudos repositorioUmbrales,
        IUnitOfWork unitOfWork)
    {
        _repositorioPerfil = repositorioPerfil;
        _repositorioEstudiantes = repositorioEstudiantes;
        _repositorioTiposKudo = repositorioTipoKudo;
        _repositorioUmbralesParaMedallas = repositorioUmbrales;
        _unitOfWork = unitOfWork;
    }

    public async Task<Resultado> EjecutarAsync(string idEstudianteEmisor, AsignarKudoDto dto)
    {
        var resultadoEstudianteEmisor = await _repositorioEstudiantes.GetByStringIdAsync(idEstudianteEmisor);
        if (resultadoEstudianteEmisor.EsFallo)
            return Resultado.Falla(resultadoEstudianteEmisor.Errores);

        Estudiante estudianteEmisor = resultadoEstudianteEmisor.Valor;

        Resultado<Entidades.PerfilEstudiante> resultadoPerfilEstudianteEmisor= estudianteEmisor.ObtenerPerfilEstudiantePor(dto.IdPerfilEstudianteEmisor);

        if (resultadoPerfilEstudianteEmisor.EsFallo)
            return Resultado.Falla(resultadoPerfilEstudianteEmisor.Errores);
        
        Entidades.PerfilEstudiante perfilEstudianteEmisor = resultadoPerfilEstudianteEmisor.Valor;

        if (perfilEstudianteEmisor.Id == dto.IdPerfilEstudianteRecibe)
            return Resultado.Falla(Error.Forbidden);
        

        var resultadoPerfilReceptor = await _repositorioPerfil.GetByIdAsync(dto.IdPerfilEstudianteRecibe);
        if (resultadoPerfilReceptor.EsFallo)
            return Resultado.Falla(resultadoPerfilReceptor.Errores);
        

        Entidades.PerfilEstudiante perfilEstudianteReceptor = resultadoPerfilReceptor.Valor;

        var resultadoTipoKudo = await _repositorioTiposKudo.GetByIdAsync(dto.Kudo.Id);

        if (resultadoTipoKudo.EsFallo)
            return Resultado.Falla(resultadoTipoKudo.Errores);
        

        var resultadoOtorgar = perfilEstudianteEmisor.OtorgarKudo(resultadoTipoKudo.Valor, perfilEstudianteReceptor);
        if (resultadoOtorgar.EsFallo)
            return resultadoOtorgar;

        //En caso de que se haya otorgado un kudo, se evalúa si el perfil receptor cumple con algún umbral para obtener una medalla.
        var resultadoUmbrales = await _repositorioUmbralesParaMedallas.GetAllByProfesorAndGrupoIdAsync(perfilEstudianteReceptor.GrupoId, perfilEstudianteReceptor.Grupo.ProfesorId);
        if (resultadoUmbrales.EsFallo)
            return Resultado.Falla(resultadoUmbrales.Errores);
        var umbrales = resultadoUmbrales.Valor;

        var resultadoRecepcion = perfilEstudianteReceptor.RecibirKudoYEvaluarMedalla(resultadoOtorgar.Valor, umbrales.FirstOrDefault(u => u.TipoKudoId == dto.Kudo.Id));
        if (resultadoRecepcion.EsFallo)
            return resultadoRecepcion;

        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Resultado.Falla(new Error("Error.Unexpected", "Hubo un error: "+ ex.Message)); 
        }
        
        return Resultado.Exitoso();

    }

}