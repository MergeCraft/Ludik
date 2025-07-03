using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.TablaClasificacionDTOs;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.InterfacesCasosUsos.TablaClasificacion
{
    public interface IObtenerTodasLasTablasClasificacion
    {
        Task<Resultado<IEnumerable<TablaClasificacionInfoDto>>> EjecutarAsync();
    }
}
