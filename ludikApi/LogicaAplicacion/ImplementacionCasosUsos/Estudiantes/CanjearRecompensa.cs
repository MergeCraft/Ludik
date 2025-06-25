using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
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

        public async Task<Resultado> EjecutarAsync(int recompensaId, int perfilEstudianteID)
        {

            var resultadoRecuperarRecompensa = _repositorioRecompensas.GetByIdAsync(recompensaId);
            if (resultadoRecuperarRecompensa.Result.EsFallo)
                return Resultado.Falla((new Error("Error.NotFound", "No se encontró la recompensa especificada.")));
            var recompensa = resultadoRecuperarRecompensa.Result.Valor!;
            
           var resultadoRecuperarPerfilEstudiante = _repositorioPerfilEstudianteGrupo.GetByIdAsync(perfilEstudianteID);
            if (resultadoRecuperarPerfilEstudiante.Result.EsFallo)
                return Resultado.Falla((new Error("Error.NotFound", "No se encontró el perfil del estudiante especificado.")));
            var perfilEstudiante = resultadoRecuperarPerfilEstudiante.Result.Valor!;

            if (perfilEstudiante.Monedas < recompensa.Precio)
                return Resultado.Falla((new Error("Error.Validation", "El estudiante no tiene suficientes puntos para canjear esta recompensa.")));
            // esto es opcional podria servir para los hitos ya que se adquieren una sola ves (habria que preguntar por el type si es hito)
            if (perfilEstudiante.Inventario != null && perfilEstudiante.Inventario.Any(r => r.Id == recompensa.Id))
                return Resultado.Falla(new Error("Error.Validation", "El estudiante ya posee esta recompensa."));

            perfilEstudiante.Monedas -= recompensa.Precio;

            if (perfilEstudiante.Inventario == null)
                perfilEstudiante.Inventario = new List<LogicaNegocio.Entidades.Recompensa>();
            perfilEstudiante.Inventario.Add(recompensa);

            var resultadoUpdate = await _repositorioPerfilEstudianteGrupo.UpdateAsync(perfilEstudiante);
            if (resultadoUpdate.EsFallo)
                return resultadoUpdate; 

            return Resultado.Exitoso();
        }
    }
}
