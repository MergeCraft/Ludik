using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.UsuarioDTOs;

namespace LogicaAplicacion.InterfacesCasosUsos.Estudiante
{
    public interface IAlta
    {
        void Ejecutar(EstudianteAltaDto estudianteAltaDto);
    }
}
