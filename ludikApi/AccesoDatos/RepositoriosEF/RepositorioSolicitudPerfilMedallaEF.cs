using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioSolicitudPerfilMedallaEF : IRepositorioSolicitudPerfilMedalla
    {
        private readonly ContextoDb _db;
        public RepositorioSolicitudPerfilMedallaEF(ContextoDb db)
            => _db = db;

        public async Task<Resultado> AddAsync(SolicitudPerfilMedalla sol)
        {
            try
            {
                await _db.SolicitudesPerfilMedalla.AddAsync(sol);
                await _db.SaveChangesAsync();
                return Resultado.Exitoso();
            }
            catch (Exception ex)
            {
                return Resultado.Falla(
                    new Error("Error.Unexpected", ex.Message));
            }
        }

        public Task<Resultado<IEnumerable<SolicitudPerfilMedalla>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado<List<SolicitudPerfilMedalla>>> GetByGrupoAsync(int grupoId)
        {
            try
            {
                var lista = await _db.SolicitudesPerfilMedalla
                    .Where(s => s.GrupoId == grupoId)
                    .ToListAsync();

                return Resultado<List<SolicitudPerfilMedalla>>.Exitoso(lista);
            }
            catch (Exception ex)
            {
                return Resultado<List<SolicitudPerfilMedalla>>.Falla(
                    new Error("Error.Unexpected", ex.Message));
            }
        }

        public Task<Resultado<SolicitudPerfilMedalla>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(SolicitudPerfilMedalla unObjeto)
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado> UpdateAsync(SolicitudPerfilMedalla sol)
        {
            try
            {
                _db.SolicitudesPerfilMedalla.Update(sol);
                await _db.SaveChangesAsync();
                return Resultado.Exitoso();
            }
            catch (Exception ex)
            {
                return Resultado.Falla(
                    new Error("Error.Unexpected", ex.Message));
            }
        }
        public async Task<Resultado<List<SolicitudPerfilMedalla>>> GetByPerfilAsync(int perfilEstudianteId)
        {
            try
            {
                var lista = await _db.SolicitudesPerfilMedalla
                    .Where(s => s.PerfilEstudianteId == perfilEstudianteId)
                    .ToListAsync();

                return Resultado<List<SolicitudPerfilMedalla>>.Exitoso(lista);
            }
            catch (Exception ex)
            {
                return Resultado<List<SolicitudPerfilMedalla>>.Falla(
                    new Error("Error.Unexpected", ex.Message));
            }
        }
    }
}
