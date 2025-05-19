using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.ProfesorDTOs;
using LogicaAplicacion.DTOs.UsuarioDTOs;

namespace LogicaAplicacion.InterfacesCasosUsos.Profesor
{
    public interface IAltaProfesor
    {
        void Ejecutar(ProfesorAltaDto profesorAltaDto);
    }
}
