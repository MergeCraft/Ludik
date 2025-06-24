using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.DTOsMappers.RecompensaMappers;
using LogicaAplicacion.InterfacesCasosUsos.Tienda;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Tienda
{
    public class ObtenerListadoRecompensa:IObtenerListadoRecompensa
    {
        private readonly IRepositorioRecompensas _repositorioRecompensas;
        private readonly IRepositorioTiendas _repositorioTiendas;
        public ObtenerListadoRecompensa(
            IRepositorioRecompensas repositorioRecompensas,
            IRepositorioTiendas repositorioTiendas)
        {
            _repositorioRecompensas = repositorioRecompensas;
            _repositorioTiendas = repositorioTiendas;
        }

        public async Task<Resultado<IEnumerable<RecompensaListadoDto>>> EjecutarAsync(string tiendaIdString)
        {
            if (!int.TryParse(tiendaIdString, out int tiendaId))
                return Resultado<IEnumerable<RecompensaListadoDto>>.Falla(
                    new Error("Error.InvalidId", $"ID de tienda inválido: '{tiendaIdString}'"));

            var resultadoTienda = await _repositorioTiendas.GetByIdAsync(tiendaId);
            if (resultadoTienda.EsFallo)
                return Resultado<IEnumerable<RecompensaListadoDto>>.Falla(
                    new Error("Error.NotFound", "No se encontró la tienda especificada."));

            var resultadoLista = await _repositorioRecompensas.GetByTiendaIdAsync(tiendaId);
            if (resultadoLista.EsFallo)
                return Resultado<IEnumerable<RecompensaListadoDto>>.Falla(
                    new Error("Error.Unexpected", "Error al obtener recompensas: "));

            var entidades = resultadoLista.Valor!;

            var dtos = entidades
                .Select(r => RecompensaListadoMapper.ToDto(r))
                .ToList();

            return Resultado<IEnumerable<RecompensaListadoDto>>.Exitoso(dtos);
        }
    }
}
