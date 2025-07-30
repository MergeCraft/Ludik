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
        private readonly IRepositorioTiendas _repositorioTiendas;
        private readonly IGeneradorUrlsParaColeccionesImagenes _generadorUrlsParaColecciones;
        public ObtenerListadoRecompensa(
            IRepositorioTiendas repositorioTiendas,
            IGeneradorUrlsParaColeccionesImagenes generadorUrlsImagenes)
        {
            _repositorioTiendas = repositorioTiendas;
            _generadorUrlsParaColecciones = generadorUrlsImagenes;
        }

        public async Task<Resultado<IEnumerable<RecompensaDto>>> EjecutarAsync(int tiendaId)
        {

            var resultadoTienda = await _repositorioTiendas.GetByIdAsync(tiendaId);
            if (resultadoTienda.EsFallo)
                return Resultado<IEnumerable<RecompensaDto>>.Falla(resultadoTienda.Errores);

            var recompensas = resultadoTienda.Valor.Recompesas;

            IEnumerable<RecompensaDto> dtos = recompensas
                .Select(r => RecompensaMapper.ToDto(r))
                .ToList();

            await _generadorUrlsParaColecciones.EjecutarProcesarUrlsAsync(dtos,
                (dto => dto.EnlaceImagenMiniatura, (dto, url) => dto.EnlaceImagenMiniatura = url), (dto => dto.EnlaceImagenCompleta, (dto, url) => dto.EnlaceImagenCompleta = url)
                );

            return Resultado<IEnumerable<RecompensaDto>>.Exitoso(dtos);
        }

    }
}
