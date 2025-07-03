using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.Profesor;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Profesores
{
    public class ReinicioLogrosDeUnGrupo:IReinicioLogrosDeUnGrupo
    {
        private readonly IRepositorioGrupos _repoGrupos;
        private readonly IRepositorioRendimientoPeriodos _repoRendimientos;
        private readonly IRepositorioPerfilEstudianteGrupo _repoPerfiles;

        public ReinicioLogrosDeUnGrupo(
            IRepositorioGrupos repoGrupos,
            IRepositorioRendimientoPeriodos repoRendimientos,
            IRepositorioPerfilEstudianteGrupo repoPerfiles)
        {
            _repoGrupos = repoGrupos;
            _repoRendimientos = repoRendimientos;
            _repoPerfiles = repoPerfiles;
        }
        public async Task<Resultado> EjecutarAsync(int grupoId, string profesorId)
        {
            var grupoRes = await _repoGrupos.GetByIdAsync(grupoId);
            if (grupoRes.EsFallo)
                return Resultado.Falla(grupoRes.Errores);

            var grupo = grupoRes.Valor;
            if(grupo.ProfesorId != profesorId)
                return Resultado.Falla(new Error("Error.Validation", "El profesor no tiene permiso para reiniciar los logros de este grupo."));

            var rendimientos = grupo.ReiniciarMedallasEstudiantes(grupo.FCreacion, DateTime.Now);

            foreach (var rendimiento in rendimientos)
            {
                var addRes = await _repoRendimientos.AddAsync(rendimiento);
                if (addRes.EsFallo)
                    return Resultado.Falla(addRes.Errores);
            }

            var saveRes = await _repoPerfiles.SaveCambiosAsync(); 
            if (saveRes.EsFallo)
                return Resultado.Falla(saveRes.Errores);

            return Resultado.Exitoso();
        }
    }
}
