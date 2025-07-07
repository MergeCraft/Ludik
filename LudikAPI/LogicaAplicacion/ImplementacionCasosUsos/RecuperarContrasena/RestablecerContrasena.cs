using LogicaAplicacion.DTOs.RestablecerContrasenaDTO;
using LogicaAplicacion.InterfacesCasosUsos.RecuperarContrasena;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.RecuperarContrasena;

public class RestablecerContrasena: IRestablecerContrasena
{
    public async Task<Resultado> EjecutarAsync(InformacionParaRestablecerContrasenaDto dto)
    {
        throw new NotImplementedException();
    }

}