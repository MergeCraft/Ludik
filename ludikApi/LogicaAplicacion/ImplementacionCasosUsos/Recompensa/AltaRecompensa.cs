using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.DTOsMappers.RecompensaMappers;
using LogicaAplicacion.InterfacesCasosUsos.Recompensa;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Recompensa
{
    public class AltaRecompensa:IAltaRecompensa
    {
        private readonly IRepositorioRecompensas _repositorioRecompensas;
        private readonly IRepositorioTiendas _repositorioTiendas;
        public AltaRecompensa(IRepositorioRecompensas repositorioRecompensas, IRepositorioTiendas repositorioTiendas)
        {
            _repositorioRecompensas = repositorioRecompensas;
            _repositorioTiendas = repositorioTiendas;
        }
        public async Task<Resultado> EjecutarAsync(RecompensaAltaDto recompensaDto, string profesorId)
        {
            if (recompensaDto == null)
                return Resultado.Falla(new Error("Error.Validation", "No hay información para poder dar de alta la recompensa."));

            var recompensa = RecompensaAltaMapper.fromDto(recompensaDto);
            var resultadoValidacion = recompensa.esValido();
            if (resultadoValidacion.EsFallo)
                return resultadoValidacion;

            await _repositorioRecompensas.AddAsync(recompensa);
            return Resultado.Exitoso();
        }
    }
}
