using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.Eventos;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObject;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LogicaAplicacion.ManejadoresDeEventos
{
    public class PacEventoHandler : INotificationHandler<AsignacionMedallaCompletadaEvento>
    {
        private readonly IRepositorioProyectoAulaColaborativo _repoPac;
        private readonly IRepositorioGrupos _repoGrupos;
        private readonly IRepositorioPerfilEstudianteRecompensa _repoRecompensa;
        private readonly ILogger<PacEventoHandler> _logger;

        public PacEventoHandler(
            IRepositorioProyectoAulaColaborativo repoPac,
            IRepositorioGrupos repoGrupos,
            IRepositorioPerfilEstudianteRecompensa repoRecompensa,
            ILogger<PacEventoHandler> logger)
        {
            _repoPac = repoPac;
            _repoGrupos = repoGrupos;
            _repoRecompensa = repoRecompensa;
            _logger = logger;
        }

        public async Task Handle(AsignacionMedallaCompletadaEvento notification,
                                 CancellationToken cancellationToken)
        {
            try
            {
                var perfil = notification.Estudiante
                                   .Perfiles
                                   .FirstOrDefault(p => p.Id == notification.PerfilEstudianteId);
                if (perfil == null) return;

                var grupoId = perfil.GrupoId;

                var grupoRes = await _repoGrupos.GetByIdAsync(grupoId);
                if (grupoRes.EsFallo) return;
                var grupo = grupoRes.Valor!;

                int totalMedallas = grupo.ContarMedallasTotales();

                var pacsRes = await _repoPac.GetByGrupoAsync(grupoId);
                if (pacsRes.EsFallo) return;
                var pac = pacsRes.Valor!
                                 .FirstOrDefault(pac => pac.Estado == EstadoPAC.Activo);
                if (pac == null) return;

                pac.TotalContribuciones = Math.Min(
                    totalMedallas,
                    pac.CantidadMedallasNecesarias);

                if (pac.TotalContribuciones >= pac.CantidadMedallasNecesarias)
                    pac.Estado = EstadoPAC.Completado;

                var updPac = await _repoPac.UpdateAsync(pac);
                if (updPac.EsFallo)
                {
                    _logger.LogError(
                        "[PacEventoHandler] No se pudo actualizar PAC {PacId}: {Errores}",
                        pac.Id, updPac.Errores);
                    return;
                }

                if (pac.Estado == EstadoPAC.Completado)
                {
                    foreach (var alumnoPerfil in grupo.Alumnos)
                    {
                        var otorgarRes = pac.RecompensaClase.Otorgar(alumnoPerfil);
                        if (otorgarRes.EsFallo)
                        {
                            _logger.LogError(
                                "[PacEventoHandler] Falló al otorgar recompensa {RecompensaId} " +
                                "a perfil {PerfilId}: {Errores}",
                                pac.RecompensaClase.Id,
                                alumnoPerfil.Id,
                                otorgarRes.Errores);
                        }
                        else
                        {
                            _logger.LogInformation(
                                "[PacEventoHandler] Recompensa {RecompensaId} aplicada " +
                                "a perfil {PerfilId}.",
                                pac.RecompensaClase.Id,
                                alumnoPerfil.Id);

                            // Persisto la asignación de recompensa
                            var addRec = await _repoRecompensa.AddAsync(
                                new PerfilEstudianteRecompensa
                                {
                                    PerfilEstudianteId = alumnoPerfil.Id,
                                    RecompensaId = pac.RecompensaClase.Id
                                });

                            if (addRec.EsFallo)
                            {
                                _logger.LogError(
                                  "[PacEventoHandler] No se pudo registrar recompensa {RecompensaId} " +
                                  "para perfil {PerfilId}: {Errores}",
                                  pac.RecompensaClase.Id,
                                  alumnoPerfil.Id,
                                  addRec.Errores);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "[PacEventoHandler] Excepción interna al procesar evento PAC.");
            }
        }
    }
}