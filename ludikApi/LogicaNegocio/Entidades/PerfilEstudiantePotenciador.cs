using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.Entidades
{
    public class PerfilEstudiantePotenciador
    {
        [Key]
        [ForeignKey(nameof(PerfilEstudiante))]
        public int PerfilEstudianteId { get; set; }

        public PerfilEstudiante PerfilEstudiante { get; set; }

        public int PotenciadorId { get; set; }
        public Potenciador Potenciador { get; set; }

        // Datos de la activación única
        public DateTime FechaActivacion { get; set; }
        public TimeSpan Duracion { get; set; }
        public double Multiplicador { get; set; }

        [NotMapped]
        public bool EstaActivo =>
            DateTime.UtcNow >= FechaActivacion &&
            DateTime.UtcNow <= FechaActivacion + Duracion;
    }
}
