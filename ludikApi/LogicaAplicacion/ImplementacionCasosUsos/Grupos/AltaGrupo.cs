using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaAplicacion.DTOsMappers.GrupoMappers;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;

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


		public async Task EjecutarAsync(GrupoAltaDto grupoAltaDto)
		{
			if (grupoAltaDto == null)
				throw new ArgumentNullException(nameof(grupoAltaDto), "El DTO no puede ser nulo.");

			var tabla = await _repoTablasEquivalencia.GetByIdAsync(grupoAltaDto.TablaEquivalenciaId);
			if (tabla == null)
				throw new Exception("No se encontró la tabla de equivalencia especificada.");

			var grupo = GrupoAltaMapper.fromDto(grupoAltaDto, tabla);

            grupo.tienda = new Tienda();

			await _repositorioGrupo.AddAsync(grupo);
		}


	}

}
