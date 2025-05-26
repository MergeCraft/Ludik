using System;
using System.Collections.Generic;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.DTOsMappers.MedallaMappers;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;


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
        public async Task EjecutarAsync(MedallaAltaDto medallaAltaDto)
        {
            if (medallaAltaDto == null)
                throw new ArgumentNullException(nameof(medallaAltaDto), "No se puede crear una medalla sin tener datos.");

            Medalla medallaNueva = MedallaAltaMapper.fromDto(medallaAltaDto);
            await _repositorioMedallas.AddAsync(medallaNueva);
        }
    }
}
