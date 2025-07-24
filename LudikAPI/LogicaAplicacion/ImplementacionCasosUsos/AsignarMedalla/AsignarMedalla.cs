using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.Eventos;
using LogicaAplicacion.InterfacesCasosUsos.AsignacionMedalla;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades = LogicaNegocio.Entidades;

namespace LogicaAplicacion.ImplementacionCasosUsos.AsignarMedalla
{
    public class AsignarMedalla : IAsignarMedalla
    {
        private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilEstudiantes;
        private readonly IRepositorioMedallas _repositorioMedallas;
        private readonly IRepositorioProfesores _repositorioProfesores;
        private readonly IRepositorioPerfilEstudianteMedalla _repositorioPerfilEstudianteMedalla;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        public AsignarMedalla(
            IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudiante,
            IRepositorioMedallas repositorioMedalla,
            IRepositorioProfesores repositorioProfesor,
            IRepositorioPerfilEstudianteMedalla repositorioPerfilEstudianteMedalla,
            IMediator mediator,
            IUnitOfWork unitOfWork)
        {
            _repositorioPerfilEstudiantes = repositorioPerfilEstudiante;
            _repositorioMedallas = repositorioMedalla;
            _repositorioProfesores = repositorioProfesor;
            _repositorioPerfilEstudianteMedalla = repositorioPerfilEstudianteMedalla;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado> EjecutarAsync(string profesorId, int idPerfilEstudiante, int idMedalla)
        {

            var perteneceResultado = await _repositorioProfesores.PerteneceGrupoAsync(profesorId, idPerfilEstudiante);
            if (perteneceResultado.EsFallo || !perteneceResultado.Valor)
                return Resultado.Falla(Error.Forbidden);
            

            var poseeResultado = await _repositorioProfesores.PoseeMedallaAsync(profesorId, idMedalla);
            if (poseeResultado.EsFallo || !poseeResultado.Valor)
                return Resultado.Falla(Error.Forbidden);
            

            var medallaResultado = await _repositorioMedallas.GetByIdAsync(idMedalla);
            if (medallaResultado.EsFallo)
                return Resultado.Falla(Error.NotFound);
            

            var perfilResultado = await _repositorioPerfilEstudiantes.GetParaAsignacionMedallaAsync(idPerfilEstudiante);
            if (perfilResultado.EsFallo)
                return Resultado.Falla(Error.NotFound);
            

            var medalla = medallaResultado.Valor;
            var perfilEstudiante = perfilResultado.Valor;


            double factor = perfilEstudiante.ObtenerMultiplicadorMonedas();
            int monedasGanadas = (int)(medalla.MonedasOtorgadas * factor);
            perfilEstudiante.Monedas += monedasGanadas;


            var nuevaAsignacion = new PerfilEstudianteMedalla
            {
                PerfilEstudianteId = perfilEstudiante.Id,
                MedallaId = medalla.Id,
                FechaObtencion = System.DateTime.UtcNow
            };


            var addResultado = await _repositorioPerfilEstudianteMedalla.AddAsync(nuevaAsignacion);
            if (addResultado.EsFallo)
                return addResultado;
            


            var evento = new AsignacionMedallaCompletadaEvento(
                perfilEstudiante.Id,
                medalla.Id, 
                perfilEstudiante.Estudiante
            );

            await _mediator.Publish(evento);

            await _unitOfWork.SaveChangesAsync();

            return Resultado.Exitoso();
        }
        
    }
}
