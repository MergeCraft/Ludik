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
        private readonly IRepositorioPerfilEstudianteRecompensa repoRecompensaPerfil;
        

        public HitoObserver(
            IRepositorioHitos repoHitos,
            IRepositorioPerfilEstudianteGrupo repoPerfiles,
            ILogger<HitoObserver> logger)
        {
            _repoHitos = repoHitos;
            _repoPerfiles = repoPerfiles;
            _logger = logger;
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
                var perfilOrigen = evt.PerfilEstudiante;
                var estudiante = perfilOrigen.Estudiante;
                int totalMedallas = estudiante.ContarCantidadMedallasTotales();
                var allHitosResult = _repoHitos.GetAllAsync().GetAwaiter().GetResult();

                if (allHitosResult.EsFallo)
                {
                    _logger.LogError($"[HitoObserver] Error al leer hitos: {allHitosResult.EsFallo}");
                    return;
                }

                var hitosPendientes = allHitosResult.Valor!
                    .Where(h => !h.Otorgado && h.Cumple(totalMedallas))
                    .ToList();

                foreach (var hito in hitosPendientes){
                    hito.Otorgado = true;
                    var updHito = _repoHitos.UpdateAsync(hito).GetAwaiter().GetResult();
                    if (updHito.EsFallo){
                        _logger.LogError($"[HitoObserver] No se pudo marcar hito {hito.Id}: {updHito.EsFallo}");
                        continue;
                    }

                    foreach (var perfil in estudiante.Perfiles)
                    {
                        var otorgarResultado =hito.Recompensa.Otorgar(perfil);

                        if (otorgarResultado.EsFallo){
                            _logger.LogError(
                                $"[HitoObserver] Falló al otorgar recompensa {hito.Recompensa.Id} " +$"en perfil {perfil.Id}: {otorgarResultado.EsFallo}");
                        }
                        else{
                            _logger.LogInformation(
                                $"[HitoObserver] Recompensa {hito.Recompensa.Id} aplicada " + $"en perfil {perfil.Id}.");
                        }

                        var updPerfil = _repoPerfiles.UpdateAsync(perfil).GetAwaiter().GetResult();
                        if (updPerfil.EsFallo){
                            _logger.LogError($"[HitoObserver] Error al actualizar perfil {perfil.Id}: {updPerfil.EsFallo}");
                        }
                    }
                }
            }
            catch (Exception ex){
                _logger.LogError(ex, "[HitoObserver] Excepción interna al procesar hitos.");
            }
        }
    }
}
