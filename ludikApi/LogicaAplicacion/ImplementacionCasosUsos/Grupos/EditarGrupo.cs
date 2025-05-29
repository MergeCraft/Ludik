using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaAplicacion.DTOsMappers.GrupoMappers;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;

namespace LogicaAplicacion.ImplementacionCasosUsos.Grupos
{
    public class EditarGrupo : IEditarGrupo
    {
        private readonly IRepositorioGrupos _repositorioGrupo;

        public EditarGrupo(IRepositorioGrupos repo)
        {
            _repositorioGrupo = repo;
        }
        public async Task EjecutarAsync(GrupoEditarDto grupoDto)
        {
            if (grupoDto == null)
                throw new ArgumentNullException(nameof(grupoDto), "El DTO no puede ser nulo.");

            var grupo = await _repositorioGrupo.GetByIdAsync(grupoDto.Id);
            if (grupo == null)
                throw new Exception("No se encontró el grupo especificado.");

            GrupoEditarDtoMapper.UpdateFromDto(grupoDto, grupo);
            await _repositorioGrupo.UpdateAsync(grupo);
        }
    }
}
