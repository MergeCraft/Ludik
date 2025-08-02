using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObject;

namespace LogicaNegocio.EntidadesAuxiliares;

public class RepresentacionImagen: IRepresentacionVisual
{
    public string NombreImagenCompleta { get; set; }
    public string NombreImagenMiniatura { get; set; }
    public InformacionVisual GetInformacionVisual()
    {
        var datos = new DatosImagen(
            NombreMiniatura: NombreImagenMiniatura,
            NombreCompleta: NombreImagenCompleta
        );
        return new InformacionVisual(datos, TipoVisual.Imagen);
    }
}