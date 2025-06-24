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
        public async Task<Resultado> EjecutarAsync(RecompensaAltaDto recompensaDto, string tiendaId,string profesorId)
        {
            if (recompensaDto == null)
                return Resultado.Falla(new Error("Error.Validation", "No hay información para poder dar de alta la recompensa."));

            var resultadoTienda = await _repositorioTiendas.GetByStringIdAsync(tiendaId);
            if (resultadoTienda.EsFallo)
                return Resultado.Falla(new Error("Error.Validation", "No se encontró la tienda especificada."));

            var tienda = resultadoTienda.Valor!;
            if (tienda.Grupo.ProfesorId != profesorId)
            {
                return Resultado.Falla(new Error("Error.Validation", "La tienda no pertenece a un grupo del profesor autenticado"));
            }

            var recompensasEnTienda = await _repositorioRecompensas.GetByTiendaIdAsync(tienda.Id);
            if (recompensasEnTienda.EsFallo)
                return Resultado.Falla(new Error("Error.Unexpected", "No se pudo verificar la unicidad del nombre de recompensa."));

            bool nombreYaExiste = recompensasEnTienda.Valor!
                .Any(r => r.Nombre.Trim().ToLower() == recompensaDto.Nombre.Trim().ToLower());

            if (nombreYaExiste)
                return Resultado.Falla(new Error("Error.Validation", "El nombre de recompensa ya está en uso en esta tienda."));

            var recompensa = RecompensaAltaMapper.fromDto(recompensaDto, tienda);
            var resultadoValidacion = recompensa.esValido();
            if (resultadoValidacion.EsFallo)
                return resultadoValidacion;

            await _repositorioRecompensas.AddAsync(recompensa);
            return Resultado.Exitoso();
        }
    }
}
