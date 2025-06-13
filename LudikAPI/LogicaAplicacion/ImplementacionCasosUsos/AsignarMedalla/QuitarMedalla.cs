using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.AsignacionMedalla;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.AsignarMedalla
{
    public class QuitarMedalla: IQuitarMedalla
    {
        private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilEstudiantes;
        private readonly IRepositorioProfesores _repositorioProfesores;
        public QuitarMedalla(
            IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudiante,
            IRepositorioProfesores repositorioProfesor)
        {
            _repositorioPerfilEstudiantes = repositorioPerfilEstudiante;
            _repositorioProfesores = repositorioProfesor;
        }

        public async Task<Resultado> EjecutarAsync(string idProfesor, int idPerfilEstudiante, int idMedalla)
        {
            var profesorResultado = await _repositorioProfesores.GetByStringIdAsync(idProfesor);
            var perfilResultado = await _repositorioPerfilEstudiantes.GetByIdAsync(idPerfilEstudiante);

            if (profesorResultado.EsFallo || perfilResultado.EsFallo)
                return Resultado.Falla(Error.NotFound);

            var profesor = profesorResultado.Valor;
            var perfil = perfilResultado.Valor;

            // Validar que el estudiante pertenece a un grupo del profesor
            if (!profesor.Grupos.Any(g => g.Id == perfil.GrupoId))
                return Resultado.Falla(Error.Forbidden);
            

            // Validar que el estudiante tiene la medalla que se quiere quitar
            var medallaParaQuitar = perfil.MedallasObtenidas.FirstOrDefault(m => m.Id == idMedalla);
            if (medallaParaQuitar == null)
                return Resultado.Falla(new Error("Error.Validation", "El estudiante no posee la medalla que se intenta quitar."));
            
            perfil.MedallasObtenidas.Remove(medallaParaQuitar);

            var updateResultado = await _repositorioPerfilEstudiantes.UpdateAsync(perfil);

            return updateResultado;
        }
    }
}
