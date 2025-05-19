using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.UsuarioDTOs
{
    public class UsuarioConRolDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Rol { get; set; }
    }
}
