using LogicaNegocio.InterfacesEntidades;

namespace LogicaNegocio.Entidades;

public class KudoOtorgado : IEntity
{
    public int Id { get; set; }

    public int PerfilEstudianteEmisorId { get; set; }
    public PerfilEstudiante Emisor { get; set; }

    public int PerfilEstudianteReceptorId { get; set; }
    public PerfilEstudiante Receptor { get; set; }

    public int TipoKudoId { get; set; }
    public TipoKudo TipoKudo { get; set; } // La razón del kudo

    public DateTime FechaOtorgamiento { get; set; }

    private KudoOtorgado() { } // Constructor para EF Core

    public KudoOtorgado(PerfilEstudiante emisor, PerfilEstudiante receptor, TipoKudo tipo, DateTime fecha)
    {
        Emisor = emisor;
        Receptor = receptor;
        TipoKudo = tipo;
        FechaOtorgamiento = fecha;
    }
}