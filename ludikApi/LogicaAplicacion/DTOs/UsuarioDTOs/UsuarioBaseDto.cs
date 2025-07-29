using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.UsuarioDTOs
{
    public class UsuarioBaseDto
    {
        public string Id { get; set; }            
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
