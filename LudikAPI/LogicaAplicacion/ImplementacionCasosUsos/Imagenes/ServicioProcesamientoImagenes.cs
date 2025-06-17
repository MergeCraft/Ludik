using LogicaAplicacion.InterfacesCasosUsos.Imagenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Imagenes
{
    public class ServicioProcesamientoImagenes: IServicioProcesamientoImagenes
    {
        public Task<Resultado<Stream>> ProcesarImagenPerfilAsync(Stream streamOriginal)
        {
            throw new NotImplementedException();
        }
    }
}
