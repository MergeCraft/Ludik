using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class SolicitudPerfilMedalla
    {
        public int Id { get; set; }

        public int PerfilEstudianteMedallaId { get; set; }
        public PerfilEstudianteMedalla PerfilEstudianteMedalla { get; set; }

        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }
    }
}
