using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.UsuarioDTOs
{
    public class LoginRespuestaDto
    {
        public string Token { get; set; }
        public string Rol { get; set; }
        public string NombreUsuario { get; set; }
        public int Id { get; set; }
    }
}
