using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.MedallaDTOs;

namespace LogicaAplicacion.InterfacesCasosUsos.Medalla
{
    public interface IAltaMedalla
    {
        Task EjecutarAsync(MedallaAltaDto medallaAltaDto);

    }
}
