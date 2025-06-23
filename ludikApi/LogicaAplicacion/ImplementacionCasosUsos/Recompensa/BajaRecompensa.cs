using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.Recompensa;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Recompensa
{
    public class BajaRecompensa : IBajaRecompensa
    {
        private readonly IRepositorioRecompensas _repositorioRecompensas;
        public BajaRecompensa(IRepositorioRecompensas repositorioRecompensas)
        {
            _repositorioRecompensas = repositorioRecompensas;
        }
        public async Task<Resultado> EjecutarAsync(string recompensaIdString, string profesorId)
        {
            if (!int.TryParse(recompensaIdString, out int recompensaId))
                return Resultado.Falla(new Error("Error.InvalidId", $"ID de recompensa inválido: '{recompensaIdString}'"));

            var resultadoRecuperar = await _repositorioRecompensas.GetByIdAsync(recompensaId);
            if (resultadoRecuperar.EsFallo)
                return Resultado.Falla(new Error("Error.NotFound", "No se encontró la recompensa especificada."));

            var recompensa = resultadoRecuperar.Valor!;


            var resultadoEliminar = await _repositorioRecompensas.RemoveAsync(recompensaId);
            if (resultadoEliminar.EsFallo)
                return Resultado.Falla(new Error("Error.Unexpected", "No se pudo eliminar la recompensa. " ));

            return Resultado.Exitoso();
        }
    }
}
