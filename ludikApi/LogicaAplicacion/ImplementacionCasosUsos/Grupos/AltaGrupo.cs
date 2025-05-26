using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaAplicacion.DTOs.ProfesorDTOs;
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


        public void Ejecutar(GrupoAltaDto grupoAltaDto)
        {
            if (grupoAltaDto == null)
                throw new ArgumentNullException(nameof(grupoAltaDto), "El DTO no puede ser nulo.");

            var tabla = _repoTablasEquivalencia.GetById(grupoAltaDto.TablaEquivalenciaId);
            if (tabla == null)
                throw new Exception("No se encontró la tabla de equivalencia especificada.");

            var grupo = GrupoAltaMapper.fromDto(grupoAltaDto, tabla);

            var enlace = new EnlaceUnion
            {
                codigoBase = "soyLaUrlDeUnion",
                expiracion = DateTime.UtcNow.AddDays(7)
            };

            grupo.enlaceUnion = enlace;
            var tienda = new Tienda();
            grupo.tienda = tienda;

            _repositorioGrupo.Add(grupo);

        }

        
    }
    
}
