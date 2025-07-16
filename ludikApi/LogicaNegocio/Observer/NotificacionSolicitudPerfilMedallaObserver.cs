using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObject;
using Microsoft.Extensions.Logging;

namespace LogicaNegocio.Observer
{
    public class NotificacionSolicitudPerfilMedallaObserver : IObserver<SolicitudPerfilMedalla>
    {
        private readonly ILogger<NotificacionSolicitudPerfilMedallaObserver> _logger;

        public NotificacionSolicitudPerfilMedallaObserver(
            ILogger<NotificacionSolicitudPerfilMedallaObserver> logger)
            => _logger = logger;

        public void OnNext(SolicitudPerfilMedalla s)
        {
            switch (s.Estado)
            {
                case EstadoSolicitud.Aceptada:
                    _logger.LogInformation(
                        $"[Observer] Notificar al estudiante del Perfil {s.PerfilEstudianteId}: " +
                        $"tu solicitud de medalla {s.MedallaId} fue ACEPTADA.");
                    break;

                case EstadoSolicitud.Rechazada:
                    _logger.LogInformation(
                        $"[Observer] Notificar al estudiante del Perfil {s.PerfilEstudianteId}: " +
                        $"tu solicitud de medalla {s.MedallaId} fue RECHAZADA.");
                    break;

                default:
                    // Solo notificar en los casos que te interesen
                    break;
            }
        }

        public void OnError(Exception error)
           => _logger.LogError(error, "[Observer] Error al notificar solicitud.");

        public void OnCompleted()
           => _logger.LogInformation("[Observer] Fin de notificaciones de solicitudes.");
    }
}
