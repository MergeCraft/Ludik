using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.SolocitudUnionDTOs;

namespace LogicaAplicacion.InterfacesCasosUsos.SolicitudUnion
{
    public interface ICrearSolicitudUnion
    {
        Task EjecutarAsync(SolicitudUnionDto dto);
    }
}
