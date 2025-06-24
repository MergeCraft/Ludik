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
        private readonly IRepositorioEstudiantes _repositorioEstudiantes;
        private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilEstudianteGrupo;

        public CanjearRecompensa(
            IRepositorioRecompensas repositorioRecompensas,
            IRepositorioEstudiantes repositorioEstudiantes,
            IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudianteGrupo)
        {
            _repositorioRecompensas = repositorioRecompensas;
            _repositorioEstudiantes = repositorioEstudiantes;
            _repositorioPerfilEstudianteGrupo = repositorioPerfilEstudianteGrupo;
        }

        public Task<Resultado> EjecutarAsync(int recompensaId, int estudianteId)
        {

            var resultadoRecuperarRecompensa = _repositorioRecompensas.GetByIdAsync(recompensaId);
            if (resultadoRecuperarRecompensa.Result.EsFallo)
                return Task.FromResult(Resultado.Falla(new Error("Error.NotFound", "No se encontró la recompensa especificada.")));
            var recompensa = resultadoRecuperarRecompensa.Result.Valor!;
            
            throw new NotImplementedException();
        }
    }
}
