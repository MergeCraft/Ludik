using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObject;
using Microsoft.Extensions.Logging;

namespace LogicaNegocio.Observer
{
    public class PacObserver : IObserver<PerfilEstudianteMedalla>
    {
        private readonly IRepositorioProyectoAulaColaborativo _repoPac;
        private readonly IRepositorioGrupos _repoGrupos;
        private readonly ILogger<PacObserver> _logger;

        public PacObserver(
            IRepositorioProyectoAulaColaborativo repoPac,
            IRepositorioGrupos repoGrupos,
            ILogger<PacObserver> logger)
        {
            _repoPac = repoPac;
            _repoGrupos = repoGrupos;
            _logger = logger;
        }

        public void OnNext(PerfilEstudianteMedalla evt)
        {
            try
            {
                var grupoId = evt.PerfilEstudiante.GrupoId;

                var grupoRes = _repoGrupos.GetByIdAsync(grupoId).GetAwaiter().GetResult();
                if (grupoRes.EsFallo) return;
                var grupo = grupoRes.Valor!;

                int totalMedallas = grupo.ContarMedallasTotales();

                var pacsRes = _repoPac.GetByGrupoAsync(grupoId).GetAwaiter().GetResult();
                if (pacsRes.EsFallo) return;
                var pac = pacsRes.Valor!
                             .FirstOrDefault(p => p.Estado == EstadoPAC.Activo);
                if (pac == null) return;

                pac.TotalContribuciones = Math.Min(totalMedallas, pac.CantidadMedallasNecesarias);

                if (pac.TotalContribuciones >= pac.CantidadMedallasNecesarias)
                    pac.Estado = EstadoPAC.Completado;

                var upd = _repoPac.UpdateAsync(pac).GetAwaiter().GetResult();
                if (upd.EsFallo)
                    _logger.LogError($"[PacObserver] Error al actualizar PAC {pac.Id}: {upd.Errores}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PacObserver] Excepción interna al procesar medalla.");
            }
        }

        public void OnError(Exception error)
        {
            _logger.LogError(error, "[PacObserver] recibió un error.");
        }

        public void OnCompleted()
        {
            // No implementado
        }
    }
}
