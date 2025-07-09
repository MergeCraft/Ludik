using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Extensions.Logging;

namespace LogicaNegocio.Observer
{
    public class HitoObserver : IObserver<PerfilEstudianteMedalla>
    {
        private readonly IRepositorioHitos _repoHitos;
        private readonly IRepositorioPerfilEstudianteGrupo _repoPerfiles;
        private readonly ILogger<HitoObserver> _logger;
        private readonly IRepositorioPerfilEstudianteRecompensa _repoRecompensas;
        private readonly IRepositorioEstudiantes _repoEstudiantes;

        public HitoObserver(
            IRepositorioHitos repoHitos,
            IRepositorioPerfilEstudianteGrupo repoPerfiles,
            ILogger<HitoObserver> logger,
            IRepositorioPerfilEstudianteRecompensa repoRecompensas,
            IRepositorioEstudiantes repoEstudiantes)
        {
            _repoHitos = repoHitos;
            _repoPerfiles = repoPerfiles;
            _logger = logger;
            _repoRecompensas = repoRecompensas;
            _repoEstudiantes = repoEstudiantes;
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(PerfilEstudianteMedalla evt)
        {
            try
            {
                // 1) Perfil origen y carga completa del estudiante
                var perfilOrigen = evt.PerfilEstudiante;
                var estResult = _repoEstudiantes.GetByPerfilIdAsync(perfilOrigen.Id)
                                  .GetAwaiter().GetResult();
                if (estResult.EsFallo)
                {
                    _logger.LogError($"[HitoObserver] No se pudo cargar estudiante para perfil {perfilOrigen.Id}: {estResult.EsFallo}");
                    return;
                }
                var estudiante = estResult.Valor!;

                // 2) Calcular total de medallas en TODOS sus perfiles
                int totalMedallas = estudiante.ContarCantidadMedallasTotales();

                // 3) Traer hitos pendientes
                var allHitosResult = _repoHitos.GetAllAsync().GetAwaiter().GetResult();
                if (allHitosResult.EsFallo)
                {
                    _logger.LogError($"[HitoObserver] Error al leer hitos: {allHitosResult.EsFallo}");
                    return;
                }
                var hitosPendientes = allHitosResult.Valor!
                    .Where(h => !h.Otorgado)
                    .ToList();

                // 4) Procesar cada hito cumplido
                foreach (var hito in hitosPendientes)
                {
                    if (!hito.Cumple(totalMedallas))
                        continue;

                    // 4.1) Marcar como otorgado
                    hito.Otorgado = true;
                    var updHito = _repoHitos.UpdateAsync(hito).GetAwaiter().GetResult();
                    if (updHito.EsFallo)
                    {
                        _logger.LogError($"[HitoObserver] No se pudo marcar hito {hito.Id}: {updHito.EsFallo}");
                        continue;
                    }

                    // 4.2) Aplicar recompensa a **todos** los perfiles
                    foreach (var perfil in estudiante.Perfiles)
                    {
                        if (hito.Recompensa is Potenciador pot)
                        {
                            perfil.ActivarPotenciador(pot);
                            _logger.LogInformation($"[HitoObserver] Potenciador x{pot.Multiplicador} activado en perfil {perfil.Id}.");
                        }
                        else
                        {
                            var pr = new PerfilEstudianteRecompensa
                            {
                                PerfilEstudianteId = perfil.Id,
                                RecompensaId = hito.Recompensa.Id
                            };
                            var addRec = _repoRecompensas.AddAsync(pr).GetAwaiter().GetResult();
                            if (addRec.EsFallo)
                            {
                                _logger.LogError($"[HitoObserver] No se pudo agregar recompensa {hito.Recompensa.Id} al perfil {perfil.Id}: {addRec.EsFallo}");
                            }
                            else
                            {
                                perfil.InventarioRecompensas.Add(pr);
                                _logger.LogInformation($"[HitoObserver] Recompensa {hito.Recompensa.Id} añadida al inventario del perfil {perfil.Id}.");
                            }
                        }

                        // 4.3) Persistir cambios de cada perfil
                        var updPerfil = _repoPerfiles.UpdateAsync(perfil).GetAwaiter().GetResult();
                        if (updPerfil.EsFallo)
                            _logger.LogError($"[HitoObserver] Error al actualizar perfil {perfil.Id}: {updPerfil.EsFallo}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[HitoObserver] Excepción interna al procesar hitos.");
            }
        }
    }
}
