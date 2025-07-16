using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.SolicitudPerfilMedalla;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using LogicaNegocio.ValueObject;
using Entidades = LogicaNegocio.Entidades;

namespace LogicaAplicacion.ImplementacionCasosUsos.SolicitudPerfilMedalla
{
    public class AceptarSolicitudPerfilMedalla : IAceptarSolicitudPerfilMedalla
    {
        private readonly IRepositorioSolicitudPerfilMedalla _repositorio;
        private readonly IRepositorioPerfilEstudianteMedalla _repositorioPerfilEstudianteMedalla;
        private readonly List<IObserver<Entidades.SolicitudPerfilMedalla>> _observers;

        public AceptarSolicitudPerfilMedalla(IRepositorioSolicitudPerfilMedalla repositorio,IRepositorioPerfilEstudianteMedalla repositorioPerfilEstudianteMedalla, IEnumerable<IObserver<Entidades.SolicitudPerfilMedalla>> observers)
        {
            _repositorio = repositorio;
            _repositorioPerfilEstudianteMedalla = repositorioPerfilEstudianteMedalla;
            _observers = observers.ToList();
        }

        public async Task<Resultado> EjecutarAsync(int idSolicitudPerfil)
        {
            var solicitud = await _repositorio.GetByIdAsync(idSolicitudPerfil);
            var solicitudValor = solicitud.Valor;
            if (solicitud.EsFallo || solicitudValor == null)
            {
                return Resultado.Falla(new Error("Error.NotFound", "La solicitud de perfil medalla no existe."));
            }
            if (solicitudValor.Estado != EstadoSolicitud.Pendiente)
            {
                return Resultado.Falla(new Error("Error.Validation", "La solicitud ya fue procesada."));
            }
            solicitudValor.Estado = EstadoSolicitud.Aceptada;
            var resultadoActualizacion = await _repositorio.UpdateAsync(solicitudValor);

            var asignacionAutomatica = new PerfilEstudianteMedalla
            {
                PerfilEstudianteId = solicitudValor.PerfilEstudianteId,
                MedallaId = solicitudValor.MedallaId
            };
            var resultadoAsignacion = await _repositorioPerfilEstudianteMedalla.AddAsync(asignacionAutomatica);
            if(resultadoAsignacion.EsFallo)
            {
                return Resultado.Falla(new Error("Error.Validation", "No se pudo asignar la medalla al perfil del estudiante."));
            }
            foreach (var obs in _observers)
                obs.OnNext(solicitudValor);
            return Resultado.Exitoso();
        }
    }
}
