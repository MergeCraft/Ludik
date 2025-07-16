using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using Microsoft.Extensions.Logging;

namespace LogicaNegocio.Observer
{
    public class ProfesorSolicitudPerfilMedallaObserver: IObserver<SolicitudPerfilMedalla>
    {
        private readonly ILogger<ProfesorSolicitudPerfilMedallaObserver> _logger;

        public ProfesorSolicitudPerfilMedallaObserver(ILogger<ProfesorSolicitudPerfilMedallaObserver> logger) => _logger = logger;

        public void OnNext(SolicitudPerfilMedalla s)
        {
            _logger.LogInformation(
                $"[Observer] Grupo {s.GrupoId}: nueva solicitud de medalla " +
                $"{s.MedallaId} por perfil {s.PerfilEstudianteId}.");
        }

        public void OnError(Exception error)
            => _logger.LogError(error, "[Observer] Error en notificar solicitud.");

        public void OnCompleted()
            => _logger.LogInformation("[Observer] Fin de notificaciones.");
    }
}
