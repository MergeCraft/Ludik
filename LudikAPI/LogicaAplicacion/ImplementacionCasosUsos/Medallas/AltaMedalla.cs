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

        public AltaMedalla(IRepositorioMedallas repositorioMedallas)
        {
            _repositorioMedallas = repositorioMedallas;
        }
        //Pre: Recibe un DTO con los datos de la medalla a crear
        //Pos: Se crea una medalla en la base de datos
        public async Task<Resultado> EjecutarAsync(MedallaAltaDto medallaAltaDto)
        {
            if (medallaAltaDto == null)
                return Resultado.Falla(new Error("Validacion.DtoNulo", "Los datos para crear la medalla no pueden ser nulos."));
            
            Medalla medallaNueva = MedallaAltaMapper.fromDto(medallaAltaDto);

            var resultado = medallaNueva.esValido();
            if (resultado.EsFallo)
                return resultado;

            Resultado resultadoCreacion = await _repositorioMedallas.AddAsync(medallaNueva);
            return resultadoCreacion;
        }
    }
}
