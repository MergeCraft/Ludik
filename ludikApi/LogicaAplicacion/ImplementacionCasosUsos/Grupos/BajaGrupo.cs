using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;

namespace LogicaAplicacion.ImplementacionCasosUsos.Grupos
{
    public class BajaGrupo: IBajaGrupo
    {
        private readonly IRepositorioGrupos _repositorioGrupo;
        public BajaGrupo(IRepositorioGrupos repo)
        {
            _repositorioGrupo = repo;
        }

        public async Task EjecutarAsync(int grupoId)
        {
            await _repositorioGrupo.RemoveAsync(grupoId);
        }
    }
}
