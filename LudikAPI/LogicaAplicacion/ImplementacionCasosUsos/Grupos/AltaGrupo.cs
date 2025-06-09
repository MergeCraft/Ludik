using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaAplicacion.DTOsMappers.GrupoMappers;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Http;



namespace LogicaAplicacion.ImplementacionCasosUsos.Grupos
{
    public class AltaGrupo: IAltaGrupo
    {
        private readonly IRepositorioGrupos _repositorioGrupos;
        private readonly IRepositorioTablasEquivalencia _repoTablasEquivalencia;
        private readonly IGeneradorEnlaceGrupo _generadorEnlace;


        public AltaGrupo(
            IRepositorioGrupos repositorioGrupo,
            IRepositorioTablasEquivalencia repoTablasEquivalencia,
            IGeneradorEnlaceGrupo generadorEnlace)
        {
            _repositorioGrupos = repositorioGrupo;
            _repoTablasEquivalencia = repoTablasEquivalencia;
            _generadorEnlace = generadorEnlace;
        }

        public async Task<Resultado> EjecutarAsync(GrupoAltaRequestDto grupoRequestDto, string profesorId)
        {
            if (grupoRequestDto == null)
                return Resultado.Falla(new Error("Error.Validation", "No hay información para poder dar de alta el grupo."));

            var resultadoTabla = await _repoTablasEquivalencia.GetByIdAsync(grupoRequestDto.TablaEquivalenciaId);
            if (resultadoTabla == null)
                return Resultado.Falla(new Error("Error.NotFound", "No se encontró la tabla de equivalencia especificada."));

            var codigoUnicoInvitacion = Guid.NewGuid().ToString("N");

            var resultadoUrlInvitacion = _generadorEnlace.GenerarEnlace(codigoUnicoInvitacion);

            if (string.IsNullOrEmpty(resultadoUrlInvitacion.Valor))
                return Resultado<GrupoDto>.Falla(new Error("Error.Unexpected", "No se pudo generar la URL de invitación para el grupo."));

            /*
            var codigoInvitacion = Guid.NewGuid().ToString("N");
            var urlInvitacion = _linkGenerator.GetUriByAction(
                httpContext,
                action: "UnirseAGrupo",  
                controller: "Estudiante",  
                values: new { codigo = codigoInvitacion });

            if (string.IsNullOrEmpty(urlInvitacion))
                return Resultado.Falla(new Error("Error.Unexpected", "No se pudo generar la URL de invitación para el grupo."));
            */

            var grupo =new Grupo
            {
                nombre = grupoRequestDto.Nombre,
                tablaEquivalencia = resultadoTabla.Valor,
                ProfesorId = profesorId,
                institucion = grupoRequestDto.Institucion,
                materia = grupoRequestDto.Materia,
                enlaceUnion = new EnlaceUnion(resultadoUrlInvitacion.Valor, codigoUnicoInvitacion),
                tienda = new Tienda()
            };

            var resultadoValidacion = grupo.esValido();
            if (resultadoValidacion.EsFallo)
                return resultadoValidacion;

            await _repositorioGrupos.AddAsync(grupo);

            return Resultado.Exitoso();

        }

    }

}
