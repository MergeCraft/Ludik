using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;
using LogicaNegocio.Excepciones;

namespace LogicaAplicacion.ImplementacionCasosUsos.Grupos
{
    public class BajaGrupo: IBajaGrupo
    {
        private readonly IRepositorioGrupos _repositorioGrupo;
        public BajaGrupo(IRepositorioGrupos repo)
        {
            _repositorioGrupo = repo;
        }

        public async Task EjecutarAsync(int grupoId, string profesorId)
        {
            var grupo = await _repositorioGrupo.GetByIdAsync(grupoId);
            if (grupo == null)
                throw new GrupoNoValidoExeption("El grupo no existe.");

            if (grupo.ProfesorId != profesorId)
                throw new UnauthorizedAccessException("No tiene permiso para eliminar este grupo.");

            await _repositorioGrupo.RemoveAsync(grupoId);
        }
    }
}
