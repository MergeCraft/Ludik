using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaAplicacion.DTOsMappers.GrupoMappers;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Grupos
{
    public class EditarGrupo : IEditarGrupo
    {
        private readonly IRepositorioGrupos _repositorioGrupo;

        public EditarGrupo(IRepositorioGrupos repo)
        {
            _repositorioGrupo = repo;
        }
        public async Task<Resultado> EjecutarAsync(GrupoEditarDto grupoDto, string profesorId)
        {
            if (grupoDto == null)
                return Resultado.Falla(new Error("Error.Validation", "No hay informacion sobre el grupo."));

            var resultado = await _repositorioGrupo.GetByIdAsync(grupoDto.Id);
            if (resultado.Valor == null)
               return Resultado.Falla(new Error("Error.Validation", "No se encontró el grupo especificado."));

            if (resultado.Valor.ProfesorId != profesorId)
                throw new UnauthorizedAccessException("No tiene permiso para editar este grupo.");

            GrupoEditarDtoMapper.UpdateFromDto(grupoDto, resultado.Valor);
            var resultadoValidacion = resultado.Valor.esValido();
            if (resultadoValidacion.EsFallo)
                return resultadoValidacion;
            await _repositorioGrupo.UpdateAsync(resultado.Valor);
            return Resultado.Exitoso();
        }
    }
}
