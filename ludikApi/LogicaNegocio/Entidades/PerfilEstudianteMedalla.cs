namespace Dominio
{
    public class PerfilEstudianteMedalla
    {
        public int Id { get; set; }  // PK autoincremental
        public int PerfilEstudianteId { get; set; }
        public PerfilEstudiante PerfilEstudiante { get; set; }  // opcional si no la necesitas
        public int MedallaId { get; set; }
        public Medalla Medalla { get; set; }                   // opcional navegación inversa
        // Si quieres permitir duplicados idénticos, omite índice único.
        // Si prefieres llevar contador en lugar de varias filas, añade:
        // public DateTime FechaUltimaAsignacion { get; set; } = DateTime.UtcNow;
    }
}
