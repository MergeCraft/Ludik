using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.InterfacesCasosUsos.Tienda
{
    public interface IObtenerListadoRecompensa
    {
        Task<Resultado<IEnumerable<RecompensaListadoDto>>> EjecutarAsync(string tiendaIdString);
    }
}
