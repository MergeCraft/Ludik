using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaNegocio.Entidades;
using Entidad = LogicaNegocio.Entidades;
using LogicaAplicacion.DTOs.SolocitudUnionDTOs;
using LogicaAplicacion.InterfacesCasosUsos.SolicitudUnion;
using LogicaNegocio.Resultados;
using LogicaNegocio.ValueObject;

namespace LogicaAplicacion.ImplementacionCasosUsos.SolicitudUnion
{
    public class AceptarSolicitudUnion : IAceptarSolicitudUnion
    {
        private readonly IRepositorioSolicitudesUnion _repoSolicitudes;
        private readonly IRepositorioGrupos _repoGrupos; 
        public AceptarSolicitudUnion(IRepositorioSolicitudesUnion repoSolicitudes,IRepositorioGrupos repositorioGrupos)
        {
            _repoSolicitudes = repoSolicitudes;
            _repoGrupos = repositorioGrupos;
        }
        public async Task<Resultado> EjecutarAsync(int idSolicitud)
        {
            var solicitud = await _repoSolicitudes.GetSolicitudConEstudianteYGrupoPorIdAsync(idSolicitud);
            if (solicitud == null)
                return Resultado.Falla(new Error("Error.Validation", "La solicitud no existe."));

            if (solicitud.Estado != EstadoSolicitud.Pendiente)
                return Resultado.Falla(new Error("Error.Validation", "La solicitud ya fue procesada."));

            solicitud.Estado = EstadoSolicitud.Aceptada;

            var grupo = solicitud.Grupo;

            var perfil = new Entidad.PerfilEstudiante
            {
                EstudianteId = solicitud.EstudianteId,
                RutaImagenCompleta = "nomejodasporahora",
                RutaImagenMiniatura="tampocomejodasporahora"
            };

        
            grupo.Alumnos ??= new List<Entidad.PerfilEstudiante>();
            grupo.Alumnos.Add(perfil);

            var resultadoSolicitud = await _repoSolicitudes.UpdateAsync(solicitud);
            if (resultadoSolicitud.EsFallo) return resultadoSolicitud;

            var resultadoGrupo = await _repoGrupos.UpdateAsync(grupo);
            if (resultadoGrupo.EsFallo) return resultadoGrupo;

            return Resultado.Exitoso();
        }
    }
}
