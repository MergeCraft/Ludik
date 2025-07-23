using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosUsos.TablaClasificacion;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.TablaClasificacion
{
    public class BajaTablaClasificacion:IBajaTablaClasificacion
    {
        private readonly IRepositorioTablasClasificacion _repo;
        private readonly IRepositorioProfesores _repoProfesores;


        public BajaTablaClasificacion(IRepositorioTablasClasificacion repo,IRepositorioProfesores repositorioProfesores)
        {
            _repo = repo;
            _repoProfesores = repositorioProfesores;
        }

        public async Task<Resultado> EjecutarAsync(int tablaId, string profesorId)
        {
            var resBusqueda = await _repo.GetByIdAsync(tablaId);
            var tabla = resBusqueda.Valor;
            var resBusquedaProfesor = await _repoProfesores.GetByStringIdAsync(profesorId);
            var profesor = resBusquedaProfesor.Valor;

            bool tieneEseGrupo = profesor.Grupos
            .Any(g => g.Id == tabla.GrupoId);

            if (!tieneEseGrupo)
            {
                return Resultado.Falla(new Error(
                    "Error.Autorizacion",
                    "El grupo de la tabla no pertenece al profesor logueado."));
            }
            var res = await _repo.RemoveAsync(tablaId);
            if (res.EsFallo)
                return Resultado.Falla(res.Errores);
            

            return Resultado.Exitoso();
        }
    }
}
