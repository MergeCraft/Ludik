using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Medallas
{
    public class BajaMedalla : IBajaMedalla
    {
        private readonly IRepositorioMedallas _repoMedallas;
        private readonly IRepositorioProfesores _repoProfesores;
        public BajaMedalla(IRepositorioMedallas repoMedallas, IRepositorioProfesores repoProfesores)
        {
            _repoMedallas = repoMedallas;
            _repoProfesores = repoProfesores;
        }

        public async Task<Resultado> EjecutarAsync(int idMedalla, string profesorId)
        {
            if (idMedalla <= 0)
                return Resultado.Falla(new Error("Error.Validation", "El ID de la medalla debe ser un entero positivo."));

            Resultado<Medalla> resultadoObtener = await _repoMedallas.GetByIdAsync(idMedalla);
            if (resultadoObtener.EsFallo)
            {
                return Resultado.Falla(new Error("Error.NotFound", $"No se encontró ninguna medalla con ID {idMedalla}."));
            }

            Medalla existente = resultadoObtener.Valor;
            if (existente == null)
            {
                return Resultado.Falla(new Error("Error.NotFound", $"No se encontró ninguna medalla con ID {idMedalla}."));
            }
            
            
            if (existente.ProfesorId!=profesorId)
            {
                return Resultado.Falla(new Error("Error.Validation", $"Esa Medalla No se encuentra dentro de la lista de medallas {idMedalla}."));
            }
            Resultado resultadoRemove = await _repoMedallas.RemoveAsync(existente);
            return resultadoRemove;
        }
    }
}
