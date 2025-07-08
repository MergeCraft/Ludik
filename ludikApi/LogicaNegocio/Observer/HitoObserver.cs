using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.Extensions.Logging;

namespace LogicaNegocio.Observer
{
    public class HitoObserver : IObserver<PerfilEstudianteMedalla>
    {
        private readonly IRepositorioHitos _repoHitos;
        private readonly IRepositorioPerfilEstudianteGrupo _repoPerfiles;
        private readonly ILogger<HitoObserver> _logger;
        private readonly IRepositorioPerfilEstudianteRecompensa _repoRecompensas;

        public HitoObserver(
            IRepositorioHitos repoHitos,
            IRepositorioPerfilEstudianteGrupo repoPerfiles,
            ILogger<HitoObserver> logger,
            IRepositorioPerfilEstudianteRecompensa repoRecompensas)
        {
            _repoHitos = repoHitos;
            _repoPerfiles = repoPerfiles;
            _logger = logger;
            _repoRecompensas = repoRecompensas;
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
                var perfil = evt.PerfilEstudiante;

                // Total de medallas tras la asignación
                int totalMedallas = perfil.PerfilMedallas.Count;

                // 1) Obtener todos los hitos
                var allHitosResult = _repoHitos.GetAllAsync().GetAwaiter().GetResult();
                if (allHitosResult.EsFallo)
                {
                    _logger.LogError($"[HitoObserver] Error al leer hitos: {allHitosResult.EsFallo}");
                    return;
                }

                var hitosPendientes = allHitosResult.Valor!
                    .Where(h => !h.Otorgado)
                    .ToList();

                // 2) Para cada hito pendiente, verificar y asignar
                foreach (var hito in hitosPendientes)
                {
                    if (hito.Cumple(totalMedallas))
                    {
                        // Agregar recompensa al inventario del perfil
                        var perfilRecompensa = new PerfilEstudianteRecompensa
                        {
                            PerfilEstudianteId = perfil.Id,
                            RecompensaId = hito.Recompensa.Id
                        };
                        var addRecompensaResult = _repoRecompensas.AddAsync(perfilRecompensa).GetAwaiter().GetResult();
                        perfil.InventarioRecompensas.Add(perfilRecompensa);

                        // Marcar el hito como otorgado
                        hito.Otorgado = true;
                        var updHito = _repoHitos.UpdateAsync(hito).GetAwaiter().GetResult();
                        if (updHito.EsFallo)
                        {
                            _logger.LogError($"[HitoObserver] No se pudo actualizar hito {hito.Id}: {updHito.EsFallo}");
                        }
                        else
                        {
                            _logger.LogInformation($"[HitoObserver] Hito {hito.Id} cumplido para perfil {perfil.Id}.");
                        }
                    }
                }

                // 3) Persistir cambios en el perfil (nuevas recompensas)
                var updPerfil = _repoPerfiles.UpdateAsync(perfil).GetAwaiter().GetResult();
                if (updPerfil.EsFallo)
                    _logger.LogError($"[HitoObserver] Error al actualizar perfil {perfil.Id}: {updPerfil.EsFallo}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[HitoObserver] Excepción interna al procesar hitos.");
            }
        }
    }
}
