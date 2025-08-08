using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.SolicitudUnion;
using LogicaNegocio.Resultados;
using LogicaNegocio.ValueObject;
using System.Linq;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.SolicitudUnion
{
    public class RechazarSolicitudUnion : IRechazarSolicitudUnion
    {
        private readonly IRepositorioSolicitudesUnion _repoSolicitudes;

        public RechazarSolicitudUnion(IRepositorioSolicitudesUnion repoSolicitudes)
        {
            _repoSolicitudes = repoSolicitudes;
        }

        public async Task<Resultado> EjecutarAsync(int idSolicitud)
        {
            var solicitud = await _repoSolicitudes.GetSolicitudConEstudianteYGrupoPorIdAsync(idSolicitud);
            if (solicitud == null)
                return Resultado.Falla(new Error("Error.Validation", "La solicitud no existe."));

            if (solicitud.Estado != EstadoSolicitud.Pendiente)
                return Resultado.Falla(new Error("Error.Validation", "La solicitud ya fue procesada."));

            solicitud.Estado = EstadoSolicitud.Rechazada;

            var resultado = await _repoSolicitudes.UpdateAsync(solicitud);
            if (resultado.EsFallo) return resultado;

            return Resultado.Exitoso();
        }
    }
}
