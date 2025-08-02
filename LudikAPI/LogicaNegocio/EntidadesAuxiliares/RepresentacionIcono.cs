using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObject;

namespace LogicaNegocio.EntidadesAuxiliares;

public class RepresentacionIcono: IRepresentacionVisual
{
    public string NombreIcono { get; set; }
    public InformacionVisual GetInformacionVisual()
    {
       var datos = new DatosIcono(
            NombreIcono: NombreIcono
        );
        return new InformacionVisual(datos, TipoVisual.Icono);
    }
}