using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.TablaClasificacionDTOs;
using LogicaAplicacion.DTOsMappers.TablaClasificacionMappers;
using LogicaAplicacion.InterfacesCasosUsos.TablaClasificacion;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.TablaClasificacion
{
    public class AltaTablaClasificacion : IAltaTablaClasificacion
    {
        private readonly IRepositorioTablasClasificacion _repositorioTablasClasificacion;
        private readonly IRepositorioGrupos _repositorioGrupos;
        public AltaTablaClasificacion(IRepositorioTablasClasificacion repositorioTablasClasificacion, IRepositorioGrupos repositorioGrupos)
        {
            _repositorioTablasClasificacion = repositorioTablasClasificacion;
            _repositorioGrupos = repositorioGrupos;
        }
        public async Task<Resultado> EjecutarAsync(string profesorId, int grupoId, TablaClasificacionAltaDto dto)
        {
           
            var grupoResultado = await _repositorioGrupos.GetByIdAsync(grupoId);
            if (grupoResultado.EsFallo)
                return Resultado.Falla(new Error("Error.Validation", "El grupo no existe"));
            
            var grupoEncontrado = grupoResultado.Valor;

            if (grupoEncontrado.ProfesorId != profesorId)
                return Resultado.Falla(new Error("Error.Forbidden" ,"No tienes permiso para crear una tabla de clasificación en este grupo. Elige un grupo que te pertenezca."));
            

            var tablaClasificacion = TablaClasificacionMapper.MapAlta(dto, grupoEncontrado);

            var val = tablaClasificacion.esValido();
            if (val.EsFallo)
                return Resultado.Falla(val.Errores);

            var addRes = await _repositorioTablasClasificacion.AddAsync(tablaClasificacion);
            if (addRes.EsFallo)
                return Resultado.Falla(addRes.Errores);

            return Resultado.Exitoso();
        }
    }
}
