using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.PerfilEstudianteDTO
{
    public class PerfilEstudianteInformacionDto
    {
        public int Id { get; set; }

        public int AvatarGrupoId { get; set; }

        public string? EnlaceAvatar { get; set; }

        public int MetaCalificacion { get; set; }

        public string EstudianteId { get; set; }

        public int Monedas { get; set; }

        public int GrupoId { get; set; }

    }
}
