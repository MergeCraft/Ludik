using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Estudiantes
{
    public class CanjearRecompensa:ICanjearRecompensa
    {
        private readonly IRepositorioRecompensas _repositorioRecompensas;
        private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilEstudianteGrupo;

        public CanjearRecompensa(
            IRepositorioRecompensas repositorioRecompensas,
            IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudianteGrupo)
        {
            _repositorioRecompensas = repositorioRecompensas;
            _repositorioPerfilEstudianteGrupo = repositorioPerfilEstudianteGrupo;
        }

        public async Task<Resultado> EjecutarAsync(int recompensaId, int perfilEstudianteID,string estudianteId)
        {
            var resultadoRecuperarRecompensa = await _repositorioRecompensas.GetByIdAsync(recompensaId);
            if (resultadoRecuperarRecompensa.EsFallo)
                return Resultado.Falla(new Error("Error.NotFound", "No se encontró la recompensa especificada."));
            var recompensa = resultadoRecuperarRecompensa.Valor!;

            var resultadoRecuperarPerfil = await _repositorioPerfilEstudianteGrupo.GetByIdAsync(perfilEstudianteID);
            if (resultadoRecuperarPerfil.EsFallo)
                return Resultado.Falla(new Error("Error.NotFound", "No se encontró el perfil del estudiante especificado."));
            var perfil = resultadoRecuperarPerfil.Valor!;

            if (perfil.Monedas < recompensa.Precio)
                return Resultado.Falla(new Error("Error.Validation", "El estudiante no tiene suficientes puntos."));

            if(perfil.EstudianteId != estudianteId)
                return Resultado.Falla(new Error("Error.Forbidden", "No tienes permiso para canjear recompensas en este perfil."));

            perfil.Monedas -= recompensa.Precio;
            perfil.InventarioRecompensas.Add(new PerfilEstudianteRecompensa
            {
                PerfilEstudianteId = perfil.Id,
                RecompensaId = recompensa.Id
            });

            var resultadoUpdate = await _repositorioPerfilEstudianteGrupo.UpdateAsync(perfil);
            if (resultadoUpdate.EsFallo)
                return resultadoUpdate;

            return Resultado.Exitoso();
        }
    }
}
