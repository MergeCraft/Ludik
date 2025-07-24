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
        private readonly IRepositorioPerfilEstudianteGrupo _repoPerfilEstudianteGrupo;
        public AceptarSolicitudUnion(IRepositorioSolicitudesUnion repoSolicitudes,IRepositorioGrupos repositorioGrupos, IRepositorioPerfilEstudianteGrupo repoPerfilEstudianteGrupo)
        {
            _repoSolicitudes = repoSolicitudes;
            _repoGrupos = repositorioGrupos;
            _repoPerfilEstudianteGrupo = repoPerfilEstudianteGrupo;
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
            //TODO arreglar imagenes default con las que aparecera.
            var perfil = new Entidad.PerfilEstudiante
            {
                EstudianteId = solicitud.EstudianteId,
                NombreImagenCompleta = "ImagenAvatarPorDefecto",
                NombreImagenMiniatura= "ImagenAvatarPorDefectoMiniatura"
            };

            var resultadoPerfil = await _repoPerfilEstudianteGrupo.AddAsync(perfil);
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
