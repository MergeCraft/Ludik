using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.PerfilEstudianteDTO;
using LogicaAplicacion.InterfacesCasosUsos.PerfilEstudiante;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.PerfilEstudiante
{
    public class ObtenerPerfilesPorGrupo : IObtenerPerfilesPorGrupo
    {
        private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfil;

        public ObtenerPerfilesPorGrupo(IRepositorioPerfilEstudianteGrupo repositorioPerfil)
        {
            _repositorioPerfil = repositorioPerfil;
        }

        public async Task<Resultado<List<PerfilEstudianteInformacionDto>>> EjecutarAsync(int grupoId)
        {
            var resultadoPerfiles = await _repositorioPerfil.ObtenerPorGrupoIdAsync(grupoId);
            if (resultadoPerfiles.EsFallo)
                return Resultado<List<PerfilEstudianteInformacionDto>>.Falla(resultadoPerfiles.Errores);

            var perfiles = resultadoPerfiles.Valor!;
            var dtos = new List<PerfilEstudianteInformacionDto>();
            foreach (var perfil in perfiles)
            {
                int nota = perfil.CalcularNotaActual();
                var dto = PerfilEstudianteMapper.ToDto(perfil, nota);
                dtos.Add(dto);
            }
            return Resultado<List<PerfilEstudianteInformacionDto>>.Exitoso(dtos);
        }
    }
}
