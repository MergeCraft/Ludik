using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.DTOsMappers.RecompensaMappers;
using LogicaAplicacion.InterfacesCasosUsos.Tienda;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Tienda
{
    public class ObtenerListadoRecompensa:IObtenerListadoRecompensa
    {
        private readonly IRepositorioRecompensas _repositorioRecompensas;
        private readonly IRepositorioTiendas _repositorioTiendas;
        private readonly IGeneradorUrlsParaColeccionesImagenes _generadorUrlsParaColecciones;
        public ObtenerListadoRecompensa(
            IRepositorioRecompensas repositorioRecompensas,
            IRepositorioTiendas repositorioTiendas,
            IGeneradorUrlsParaColeccionesImagenes generadorUrlsImagenes)
        {
            _repositorioRecompensas = repositorioRecompensas;
            _repositorioTiendas = repositorioTiendas;
            _generadorUrlsParaColecciones = generadorUrlsImagenes;
        }

        public async Task<Resultado<IEnumerable<RecompensaDto>>> EjecutarAsync(string tiendaIdString)
        {
            if (!int.TryParse(tiendaIdString, out int tiendaId))
                return Resultado<IEnumerable<RecompensaDto>>.Falla(
                    new Error("Error.InvalidId", $"ID de tienda inválido: '{tiendaIdString}'"));

            var resultadoTienda = await _repositorioTiendas.GetByIdAsync(tiendaId);
            if (resultadoTienda.EsFallo)
                return Resultado<IEnumerable<RecompensaDto>>.Falla(
                    new Error("Error.NotFound", "No se encontró la tienda especificada."));

            var resultadoLista = await _repositorioRecompensas.GetByTiendaIdAsync(tiendaId);
            if (resultadoLista.EsFallo)
                return Resultado<IEnumerable<RecompensaDto>>.Falla(
                    new Error("Error.Unexpected", "Error al obtener recompensas: "));

            var entidades = resultadoLista.Valor!;

            IEnumerable<RecompensaDto> dtos = entidades
                .Select(r => RecompensaMapper.ToDto(r))
                .ToList();

            await _generadorUrlsParaColecciones.EjecutarProcesarUrlsAsync(dtos,
                (dto => dto.EnlaceImagenMiniatura, (dto, url) => dto.EnlaceImagenMiniatura = url), (dto => dto.EnlaceImagenCompleta, (dto, url) => dto.EnlaceImagenCompleta = url)
                );

            return Resultado<IEnumerable<RecompensaDto>>.Exitoso(dtos);
        }

    }
}
