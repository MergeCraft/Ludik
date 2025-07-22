using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.TablaClasificacion;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.TablaClasificacion
{
    public class BajaTablaClasificacion:IBajaTablaClasificacion
    {
        private readonly IRepositorioTablasClasificacion _repo;

        public BajaTablaClasificacion(IRepositorioTablasClasificacion repo)
        {
            _repo = repo;
        }

        public async Task<Resultado> EjecutarAsync(int tablaId)
        {
            //tiene que validar que el grupo de la tabla de clasificacion , su profesor Id es igual al profesor logueado
            var res = await _repo.RemoveAsync(tablaId);
            if (res.EsFallo)
                return Resultado.Falla(res.Errores);

            return Resultado.Exitoso();
        }
    }
}
