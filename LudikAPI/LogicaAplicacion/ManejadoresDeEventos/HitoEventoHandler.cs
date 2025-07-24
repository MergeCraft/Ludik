using InterfacesRepositorio;
using LogicaAplicacion.Eventos;
using LogicaNegocio.InterfacesRepositorios;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LogicaAplicacion.ManejadoresDeEventos;

public class HitoEventoHandler : INotificationHandler<AsignacionMedallaCompletadaEvento>
{
    private readonly IRepositorioHitos _repoHitos;
    private readonly IRepositorioPerfilEstudianteGrupo _repoPerfiles;
    private readonly ILogger<HitoEventoHandler> _logger;
    private readonly IRepositorioPerfilEstudianteRecompensa _repoRecompensas;
    private readonly IUnitOfWork _unitOfWork;

    public HitoEventoHandler(
        IRepositorioHitos repoHitos,
        IRepositorioPerfilEstudianteGrupo repoPerfiles,
        ILogger<HitoEventoHandler> logger,
        IRepositorioPerfilEstudianteRecompensa repoRecompensas,
        IUnitOfWork unitOfWork)
    {
        _repoHitos = repoHitos;
        _repoPerfiles = repoPerfiles;
        _logger = logger;
        _repoRecompensas = repoRecompensas;
        _unitOfWork = unitOfWork;
    }

   

    public async Task Handle(AsignacionMedallaCompletadaEvento notification, CancellationToken cancellationToken)
    {
        try
        {
            var estudiante = notification.Estudiante;
            int totalMedallas = estudiante.ContarCantidadMedallasTotales();
            var resultadoTodosHitos = await _repoHitos.GetAllAsync();

            if (resultadoTodosHitos.EsFallo)
            {
                _logger.LogError($"[HitoObserver] Error al leer hitos: {resultadoTodosHitos.EsFallo}");
                return;
            }

            var hitosPendientes = resultadoTodosHitos.Valor!
                .Where(h => !h.Otorgado && h.Cumple(totalMedallas))
                .ToList();

            foreach (var hito in hitosPendientes)
            {
                var perfilesResultado = await _repoPerfiles.GetPerfilesPorEstudianteAsync(notification.Estudiante.Id);
                if (perfilesResultado.EsFallo)
                {
                    _logger.LogError("No se pudieron obtener los perfiles para el estudiante {EstudianteId}", notification.Estudiante.Id);
                    continue; 
                }

                foreach (var perfil in perfilesResultado.Valor)
                {

                    hito.Recompensa.Otorgar(perfil);
                }

                hito.Otorgado = true;
                await _repoHitos.UpdateAsync(hito); 
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[HitoEventHandler] Excepción interna al procesar hitos.");
        }
    }

}

