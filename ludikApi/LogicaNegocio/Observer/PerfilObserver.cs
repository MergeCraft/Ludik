using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using Microsoft.Extensions.Logging;

namespace LogicaNegocio.Observer
{
    public class PerfilObserver : IObserver<PerfilEstudianteMedalla>
    {
        private readonly ILogger<PerfilObserver> _logger;

        public PerfilObserver(ILogger<PerfilObserver> logger)
        {
            _logger = logger;
        }

        public void OnNext(PerfilEstudianteMedalla value)
        {
            _logger.LogInformation($"[Observer] Se asignó la medalla {value.MedallaId} al perfil {value.PerfilEstudianteId}.");
        }

        public void OnError(Exception error)
        {
            _logger.LogError(error, "Ocurrió un error en PerfilObserver.");
        }

        public void OnCompleted()
        {
            _logger.LogInformation("[Observer] Notificación completada.");
        }
    }
}
