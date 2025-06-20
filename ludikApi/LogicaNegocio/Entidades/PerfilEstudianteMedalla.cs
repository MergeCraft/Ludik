namespace Dominio
{
    public class PerfilEstudianteMedalla
    {
        public int Id { get; set; }  
        public int PerfilEstudianteId { get; set; }
        public PerfilEstudiante PerfilEstudiante { get; set; } 
        public int MedallaId { get; set; }
        public Medalla Medalla { get; set; }                  
       
    }
}
