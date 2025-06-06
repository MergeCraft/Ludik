namespace LogicaAplicacion.DTOs.MedallaDTOs;

public class MedallaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string UrlImagen { get; set; }
    public string Descripcion { get; set; }
    public int CantidadMedallasBrinda { get; set; }
    public bool EsAsignacionMutua { get; set; }
}