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

    public HitoEventoHandler(
        IRepositorioHitos repoHitos,
        IRepositorioPerfilEstudianteGrupo repoPerfiles,
        ILogger<HitoEventoHandler> logger,
        IRepositorioPerfilEstudianteRecompensa repoRecompensas)
    {
        _repoHitos = repoHitos;
        _repoPerfiles = repoPerfiles;
        _logger = logger;
        _repoRecompensas = repoRecompensas;
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
                hito.Otorgado = true;
                var updHito = await _repoHitos.UpdateAsync(hito);
                if (updHito.EsFallo)
                {
                    _logger.LogError($"[HitoObserver] No se pudo marcar hito {hito.Id}: {updHito.EsFallo}");
                    continue;
                }

                foreach (var perfil in estudiante.Perfiles)
                {
                    var otorgarResultado = hito.Recompensa.Otorgar(perfil, _repoRecompensas);

                    if (otorgarResultado.EsFallo)
                    {
                        _logger.LogError(
                            $"[HitoObserver] Falló al otorgar recompensa {hito.Recompensa.Id} " + $"en perfil {perfil.Id}: {otorgarResultado.EsFallo}");
                    }
                    else
                    {
                        _logger.LogInformation(
                            $"[HitoObserver] Recompensa {hito.Recompensa.Id} aplicada " + $"en perfil {perfil.Id}.");
                    }

                    var updPerfil = await _repoPerfiles.UpdateAsync(perfil);
                    if (updPerfil.EsFallo)
                    {
                        _logger.LogError($"[HitoObserver] Error al actualizar perfil {perfil.Id}: {updPerfil.EsFallo}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[HitoEventHandler] Excepción interna al procesar hitos.");
        }
    }

}

