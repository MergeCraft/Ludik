using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Medallas
{
    public class BajaMedalla: IBajaMedalla
    {
        private readonly IRepositorioMedallas _repoMedallas;

        public BajaMedalla(IRepositorioMedallas repoMedallas)
        {
            _repoMedallas = repoMedallas;
        }

        public Task<Resultado> EjecutarAsync(int idMedalla)
        {
            throw new NotImplementedException();
        }
    }
}
