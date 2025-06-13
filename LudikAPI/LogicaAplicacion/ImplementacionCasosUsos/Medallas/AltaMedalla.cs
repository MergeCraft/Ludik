using System;
using System.Collections.Generic;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.DTOsMappers.MedallaMappers;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaNegocio.Resultados;


namespace LogicaAplicacion.ImplementacionCasosUsos.Medallas
{
    public class AltaMedalla: IAltaMedalla
    {
        private readonly IRepositorioMedallas _repositorioMedallas;
        private readonly IRepositorioProfesores _repositorioProfesores;

        public AltaMedalla(IRepositorioMedallas repositorioMedallas, IRepositorioProfesores repositorioProfesores)
        {
            _repositorioMedallas = repositorioMedallas;
            _repositorioProfesores = repositorioProfesores;
        }
        //Pre: Recibe un DTO con los datos de la medalla a crear
        //Pos: Se crea una medalla en la base de datos
        public async Task<Resultado> EjecutarAsync(MedallaAltaDto medallaAltaDto, string profesorId)
        {
            if (medallaAltaDto == null)
                return Resultado.Falla(new Error("Error.Validation", "Los datos para crear la medalla no pueden ser nulos."));
            
            Medalla medallaNueva = MedallaAltaMapper.fromDto(medallaAltaDto);

            var resultado = medallaNueva.esValido();
            if (resultado.EsFallo)
                return resultado;
            var profesor = await _repositorioProfesores.GetByStringId(profesorId);
            profesor.Valor.Medallas.Add(medallaNueva);

            //var resultadoProfesor = await _repositorioProfesores.UpdateAsync(profesor);
            //if (resultadoProfesor.EsFallo) return resultadoProfesor;

            Resultado resultadoCreacion = await _repositorioMedallas.AddAsync(medallaNueva);
            return resultadoCreacion;
        }
    }
}
