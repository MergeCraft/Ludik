using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.SolocitudUnionDTOs;
using LogicaAplicacion.InterfacesCasosUsos.SolicitudUnion;

namespace LogicaAplicacion.ImplementacionCasosUsos.SolicitudUnion
{
    public class RechazarSolicitudUnion : IRechazarSolicitudUnion
    {
        private readonly IRepositorioSolicitudesUnion _repoSolicitudes;
        public RechazarSolicitudUnion(IRepositorioSolicitudesUnion repoSolicitudes)
        {
            _repoSolicitudes = repoSolicitudes;
        }
        public Task EjecutarAsync(SolicitudUnionDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
