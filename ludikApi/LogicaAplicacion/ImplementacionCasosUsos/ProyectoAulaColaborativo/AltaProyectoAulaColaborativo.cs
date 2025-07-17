using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.ProyectoAulaColaborativoDTOs;
using LogicaAplicacion.DTOsMappers.ProyectoAulaColaborativoMappers;
using LogicaAplicacion.InterfacesCasosUsos.ProyectoAulaColaborativo;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using LogicaNegocio.ValueObject;

namespace LogicaAplicacion.ImplementacionCasosUsos.ProyectoAulaColaborativo
{
    public class AltaProyectoAulaColaborativo : IAltaProyectoAulaColaborativo
    {
        private readonly IRepositorioProyectoAulaColaborativo _repoPac;
        private readonly IRepositorioGrupos _repoGrupos;

        public AltaProyectoAulaColaborativo(
            IRepositorioProyectoAulaColaborativo repoPac,
            IRepositorioGrupos repoGrupos)
        {
            _repoPac = repoPac;
            _repoGrupos = repoGrupos;
        }
        public async Task<Resultado> EjecutarAsync(int grupoId, AltaProyectoAulaColaborativoDto dto)
        {
            // 1) DTO no nulo
            if (dto == null)
                return Resultado.Falla(new Error("Error.Validation",
                    "No hay información para dar de alta el proyecto colaborativo."));

            // 2) Grupo existe
            var grupoRes = await _repoGrupos.GetByIdAsync(grupoId);
            if (grupoRes.EsFallo)
                return Resultado.Falla(new Error("Error.Validation",
                    $"No se encontró el grupo con ID {grupoId}."));

            // 3) Verificar que no haya ya un PAC activo para ese grupo
            var pacs = await _repoPac.GetByGrupoAsync(grupoId);
            if (pacs.EsFallo)
                return Resultado.Falla(new Error("Error.Unexpected",
                    "Error verificando proyectos existentes."));
            if (pacs.Valor!.Any(p => p.Estado == EstadoPAC.Activo))
                return Resultado.Falla(new Error("Error.Validation",
                    "Ya existe un proyecto colaborativo activo para este grupo."));

            var pac = ProyectoAulaColaborativoMapper.ToDomain(dto, grupoId);

            var valid = pac.esValido();
            if (valid.EsFallo)
                return valid;

            return await _repoPac.AddAsync(pac);
        }
    }
}
