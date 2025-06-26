using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.DTOsMappers.RecompensaMappers;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Estudiantes
{
    public class ObtenerRecompensasInventarioPerfil : IObtenerRecompensasInventarioPerfil
    {
        private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilEstudiante;

        public ObtenerRecompensasInventarioPerfil(IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudiante)
        {
            _repositorioPerfilEstudiante = repositorioPerfilEstudiante;
        }
        public async Task<Resultado<List<RecompensaListadoDto>>> EjecutarAsync(int idPerfil,string idEstudiante)
        {
            // 1. Recuperar el perfil (con InventarioRecompensas ya incluido)
            var resultadoPerfil = await _repositorioPerfilEstudiante.GetByIdAsync(idPerfil);
            if (resultadoPerfil.EsFallo)
                return Resultado<List<RecompensaListadoDto>>.Falla(
                    new Error("Error.NotFound", "No se encontró el perfil del estudiante especificado."));

            var perfil = resultadoPerfil.Valor!;

            if(perfil.EstudianteId != idEstudiante)
                return Resultado<List<RecompensaListadoDto>>.Falla(
                    new Error("Error.Forbidden", "No tienes permiso para acceder a este perfil."));

            // 2. Mapear cada entidad de unión a su Recompensa correspondiente
            var dtos = perfil.InventarioRecompensas
                .Select(ir => RecompensaListadoMapper.ToDto(ir.Recompensa))
                .ToList();

            return Resultado<List<RecompensaListadoDto>>.Exitoso(dtos);
        }
    }
}
