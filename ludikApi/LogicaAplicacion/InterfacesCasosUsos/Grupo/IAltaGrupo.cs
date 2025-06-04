using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaAplicacion.DTOs.ProfesorDTOs;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.InterfacesCasosUsos.Grupo
{
    public interface IAltaGrupo
    {
        Task<Resultado> EjecutarAsync(GrupoAltaDto grupoAltaDto);

    }
}
