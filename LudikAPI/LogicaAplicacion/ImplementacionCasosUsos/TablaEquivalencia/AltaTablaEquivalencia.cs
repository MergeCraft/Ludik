using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.TablaEquivalenciaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.TablaEquivalencia;
using LogicaNegocio.Resultados;
using Entidad = LogicaNegocio.Entidades;

namespace LogicaAplicacion.ImplementacionCasosUsos.TablaEquivalencia;

public class AltaTablaEquivalencia: IAltaTablaEquivalencia
{
    private readonly IRepositorioTablasEquivalencia _repositorioTablasEquivalencia;
    private readonly IRepositorioMedallas _repositorioMedallas;

    public AltaTablaEquivalencia(
        IRepositorioTablasEquivalencia repositorioTablasEquivalencia,
        IRepositorioMedallas repositorioMedallas)
    {
        _repositorioTablasEquivalencia = repositorioTablasEquivalencia;
        _repositorioMedallas = repositorioMedallas;
    }

    public async Task<Resultado> EjecutarAsync(TablaEquivalenciaAltaDto tablaDto, string profesorId)
    {

        var tablaEquivalencia = new Entidad.TablaEquivalencia(tablaDto.Nombre, profesorId);

        // Procesar las equivalencias del DTO
        if (tablaDto.Equivalencias != null && tablaDto.Equivalencias.Any())
        {
            // Extraer todos los IDs de medallas únicos del DTO para una única consulta a la BD.
            var idsMedallasDto = tablaDto.Equivalencias
                .SelectMany(e => e.MedallasNecesarias.Select(m => m.Id))
                .Distinct()
                .ToList();

            // Buscar todas las medallas necesarias en la base de datos.
            var resultadoMedallasEntidades = await _repositorioMedallas.FindByIdsAsync(idsMedallasDto);

            // Validar que todas las medallas solicitadas existan.
            if (resultadoMedallasEntidades.EsFallo || resultadoMedallasEntidades.Valor.Count() != idsMedallasDto.Count)
                return Resultado.Falla(new Error("Validation.NotFound", "Una o más medallas especificadas no existen."));
            

            var medallasMap = resultadoMedallasEntidades.Valor.ToDictionary(m => m.Id);

            // Construir las entidades Equivalencia y agregarlas a la tabla.
            foreach (var equivalenciaDto in tablaDto.Equivalencias)
            {
                var medallasParaEquivalencia = equivalenciaDto.MedallasNecesarias
                    .Select(mDto => medallasMap[mDto.Id])
                    .ToList();

                var nuevaEquivalencia = new Equivalencia(equivalenciaDto.Nota, medallasParaEquivalencia);
                tablaEquivalencia.AgregarEquivalencia(nuevaEquivalencia); // Usando el método del agregado.
            }
        }


        var resultadoValidacion = tablaEquivalencia.esValido();
        if (resultadoValidacion.EsFallo)
            return resultadoValidacion;
        

        await _repositorioTablasEquivalencia.AddAsync(tablaEquivalencia);

        return Resultado.Exitoso();
    }

}

