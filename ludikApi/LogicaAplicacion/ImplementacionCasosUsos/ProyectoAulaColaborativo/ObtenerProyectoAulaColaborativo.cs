using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.ProyectoAulaColaborativoDTOs;
using LogicaAplicacion.DTOsMappers.ProyectoAulaColaborativoMappers;
using LogicaAplicacion.InterfacesCasosUsos.ProyectoAulaColaborativo;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.ProyectoAulaColaborativo
{
    public class ObtenerProyectoAulaColaborativo : IObtenerProyectoAulaColaborativo
    {
        private readonly IRepositorioProyectoAulaColaborativo _repoPac;

        public ObtenerProyectoAulaColaborativo(IRepositorioProyectoAulaColaborativo repoPac)
        {
            _repoPac = repoPac;
        }

        public async Task<Resultado<IEnumerable<ProyectoAulaColaborativoDto>>> EjecutarAsync(int grupoId)
        {
            var resultadoRepo = await _repoPac.GetByGrupoAsync(grupoId);
            if (resultadoRepo.EsFallo)
                return Resultado<IEnumerable<ProyectoAulaColaborativoDto>>.Falla(resultadoRepo.Errores);

            var dtos = resultadoRepo.Valor
                .Select(pac => pac.ToDto());

            return Resultado<IEnumerable<ProyectoAulaColaborativoDto>>.Exitoso(dtos);
        }
    }
}
