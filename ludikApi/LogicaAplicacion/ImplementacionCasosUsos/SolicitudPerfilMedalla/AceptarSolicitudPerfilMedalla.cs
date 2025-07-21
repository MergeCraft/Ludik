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
        private readonly IEnumerable<IObserver<PerfilEstudianteMedalla>> _obsMedalla;

        public AceptarSolicitudPerfilMedalla(IRepositorioSolicitudPerfilMedalla repositorio,IRepositorioPerfilEstudianteMedalla repositorioPerfilEstudianteMedalla, IEnumerable<IObserver<Entidades.SolicitudPerfilMedalla>> observers, IEnumerable<IObserver<PerfilEstudianteMedalla>> obsMedalla)
        {
            _repositorio = repositorio;
            _repositorioPerfilEstudianteMedalla = repositorioPerfilEstudianteMedalla;
            _observers = observers.ToList();
            _obsMedalla = obsMedalla.ToList();
        }

        public async Task<Resultado> EjecutarAsync(int idSolicitudPerfil)
        {
            var solRes = await _repositorio.GetByIdAsync(idSolicitudPerfil);
            if (solRes.EsFallo || solRes.Valor == null)
                return Resultado.Falla(new Error("Error.NotFound", "La solicitud no existe."));

            var solicitud = solRes.Valor;
            if (solicitud.Estado != EstadoSolicitud.Pendiente)
                return Resultado.Falla(new Error("Error.Validation", "La solicitud ya fue procesada."));

            // 2) Marco la solicitud como aceptada
            solicitud.Estado = EstadoSolicitud.Aceptada;
            var updSol = await _repositorio.UpdateAsync(solicitud);
            if (updSol.EsFallo)
                return updSol;

            // 3) Notifico a los observers de solicitud
            foreach (var obs in _observers)
                obs.OnNext(solicitud);

            // 4) Creo la asignación mínima de medalla
            var asignMin = new PerfilEstudianteMedalla
            {
                PerfilEstudianteId = solicitud.PerfilEstudianteId,
                MedallaId = solicitud.MedallaId
            };
            var addRes = await _repositorioPerfilEstudianteMedalla.AddAsync(asignMin);
            if (addRes.EsFallo)
                return Resultado.Falla(new Error("Error.Validation", "No se pudo asignar la medalla."));

            // 5) Recargo la asignación completa con sus relaciones
            var recRes = await _repositorioPerfilEstudianteMedalla
                .GetByIdConPerfilYMedallaAsync(asignMin.Id);
            if (recRes.EsFallo || recRes.Valor == null)
                return Resultado.Falla(new Error("Error.Unexpected", "No se pudo recargar la asignación de medalla."));

            var asignacionCompleta = recRes.Valor;

            // 6) Notifico a los observers de medalla
            foreach (var obs in _obsMedalla)
                obs.OnNext(asignacionCompleta);

            return Resultado.Exitoso();
        }
    }
}
