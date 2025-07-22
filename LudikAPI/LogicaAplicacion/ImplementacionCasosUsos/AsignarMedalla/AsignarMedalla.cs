using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.Eventos;
using LogicaAplicacion.InterfacesCasosUsos.AsignacionMedalla;
using Entidades = LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.AsignarMedalla
{
    public class AsignarMedalla : IAsignarMedalla
    {
        private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilEstudiantes;
        private readonly IRepositorioMedallas _repositorioMedallas;
        private readonly IRepositorioProfesores _repositorioProfesores;
        private readonly IRepositorioPerfilEstudianteMedalla _repositorioPerfilEstudianteMedalla;
        

        private readonly IMediator _mediator;
        public AsignarMedalla(
            IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudiante,
            IRepositorioMedallas repositorioMedalla,
            IRepositorioProfesores repositorioProfesor,
            IRepositorioPerfilEstudianteMedalla repositorioPerfilEstudianteMedalla,
            IMediator mediator)
        {
            _repositorioPerfilEstudiantes = repositorioPerfilEstudiante;
            _repositorioMedallas = repositorioMedalla;
            _repositorioProfesores = repositorioProfesor;
            _repositorioPerfilEstudianteMedalla = repositorioPerfilEstudianteMedalla;
            _mediator = mediator;
        }

        public async Task<Resultado> EjecutarAsync(string profesorId, int idPerfilEstudiante, int idMedalla)
        {
            var profesorResultado = await _repositorioProfesores.GetByStringIdAsync(profesorId);
            var perfilResultado = await _repositorioPerfilEstudiantes.GetByIdAsync(idPerfilEstudiante);
            var medallaResultado = await _repositorioMedallas.GetByIdAsync(idMedalla);

            if (profesorResultado.EsFallo) return Resultado.Falla(Error.NotFound);
            if (perfilResultado.EsFallo) return Resultado.Falla(Error.NotFound);
            if (medallaResultado.EsFallo) return Resultado.Falla(Error.NotFound);

            var profesor = profesorResultado.Valor;
            Entidades.PerfilEstudiante perfilEstudiante = perfilResultado.Valor;
            var medalla = medallaResultado.Valor;

            // Aplicar potenciador si existe
            double factor = perfilEstudiante.ObtenerMultiplicadorMonedas();
            int monedasGanadas = (int)(medalla.MonedasOtorgadas * factor);
            perfilEstudiante.Monedas += monedasGanadas;

            if (!profesor.Grupos.Any(g => g.Id == perfilEstudiante.GrupoId))
                return Resultado.Falla(Error.Forbidden);

            if (!profesor.Medallas.Any(m => m.Id == medalla.Id))
                return Resultado.Falla(Error.Forbidden);

            var nuevaAsignacion = new Entidades.PerfilEstudianteMedalla
            {
                PerfilEstudianteId = perfilEstudiante.Id,
                MedallaId = medalla.Id,
            };

            var addResultado = await _repositorioPerfilEstudianteMedalla.AddAsync(nuevaAsignacion);
            if (addResultado.EsFallo) return addResultado;

            var evento = new AsignacionMedallaCompletadaEvento(idPerfilEstudiante, idMedalla, perfilEstudiante.Estudiante);
            await _mediator.Publish(evento);
            
            return Resultado.Exitoso();
        }
    }
}
