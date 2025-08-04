using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.DTOsMappers.RecompensaMappers;
using LogicaAplicacion.InterfacesCasosUsos.Recompensa;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Recompensa
{
    public class EditarRecompensa:IEditarRecompensa
    {
        private readonly IRepositorioRecompensas _repositorioRecompensas;
        private readonly IRepositorioTiendas _repositorioTiendas;
        public EditarRecompensa(IRepositorioRecompensas repositorioRecompensas, IRepositorioTiendas repositorioTiendas)
        {
            _repositorioRecompensas = repositorioRecompensas;
            _repositorioTiendas = repositorioTiendas;
        }
        public async Task<Resultado> EjecutarAsync(string recompensaIdString, RecompensaSimpleEditarDto dto, string profesorId)
        {
            if (!int.TryParse(recompensaIdString, out int recompensaId))
                return Resultado.Falla(new Error("Error.InvalidId", $"ID de recompensa inválido: '{recompensaIdString}'."));

            var resultadoRecuperar = await _repositorioRecompensas.GetByIdAsync(recompensaId);
            if (resultadoRecuperar.EsFallo)
                return Resultado.Falla(new Error("Error.NotFound", "No se encontró la recompensa especificada."));
            RecompensaSimple recompensa = (RecompensaSimple)resultadoRecuperar.Valor;

            RecompensaEditarMapper.Update(recompensa, dto);

            var validacion = recompensa.esValido();
            if (validacion.EsFallo)
                return validacion;

            var resultadoUpdate = await _repositorioRecompensas.UpdateAsync(recompensa);
            if (resultadoUpdate.EsFallo)
                return Resultado.Falla(new Error("Error.Unexpected", "No se pudo actualizar la recompensa. "));

            return Resultado.Exitoso();
        }
    }
}

