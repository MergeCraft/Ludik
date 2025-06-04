using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaAplicacion.DTOsMappers.GrupoMappers;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Grupos
{
    public class AltaGrupo: IAltaGrupo
    {
        private readonly IRepositorioGrupos _repositorioGrupo;
        private readonly IRepositorioTablasEquivalencia _repoTablasEquivalencia;
        


        public AltaGrupo(IRepositorioGrupos repo,IRepositorioTablasEquivalencia repoTablas)
        {
            _repositorioGrupo = repo;
            _repoTablasEquivalencia = repoTablas;

		}
		//TODO:refactirizar cuando se haga el requerimiento funcional de tienda y enlaces de union 


		public async Task<Resultado> EjecutarAsync(GrupoAltaDto grupoAltaDto)
		{
			if (grupoAltaDto == null)
				return Resultado.Falla(new Error("Validation", "No hay informacion para poder dar de alta el grupo."));

			var resultado = await _repoTablasEquivalencia.GetByIdAsync(grupoAltaDto.TablaEquivalenciaId);
			if (resultado.Valor == null)
				return Resultado.Falla(new Error("NotFound","No se encontró la tabla de equivalencia especificada."));

			var grupo = GrupoAltaMapper.fromDto(grupoAltaDto, resultado.Valor);

            grupo.tienda = new Tienda();

			await _repositorioGrupo.AddAsync(grupo);
			return Resultado.Exitoso();
        }


	}

}
