namespace LogicaAplicacion.DTOs.GrupoDTOs
{
    public class GrupoAltaDto
    {

        public string Nombre { get; set; }
        public int TablaEquivalenciaId { get; set; }
        public int ProfesorId { get; set; }
        public string? Institucion { get; set; }        
        public string? Materia { get; set; }            
    }
}