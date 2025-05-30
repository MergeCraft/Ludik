using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioSolocitudesUnionEF : IRepositorioSolicitudesUnion
    {
        private readonly ContextoDb _db;
        public RepositorioSolocitudesUnionEF(ContextoDb db)
        {
            _db = db;
        }

        public async Task AddAsync(SolicitudUnion unObjeto)
        {
            if (unObjeto == null)
                throw new ArgumentNullException(nameof(unObjeto), "La solicitud de unión no puede ser nula.");

            await _db.SolicitudesUnion.AddAsync(unObjeto);
            await _db.SaveChangesAsync();
        }

        //TODO: evaluar hacer metodo que compare si dos strings son iguales
        public async Task<bool> ExisteSolicitudPendiente(string idEstudiante, int idGrupo)
        {
            return await _db.SolicitudesUnion
                .AnyAsync(s => s.estudianteId == idEstudiante
                            && s.grupoId == idGrupo
                            && s.Estado == EstadoSolicitud.Pendiente);
        }

        public Task<IEnumerable<SolicitudUnion>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SolicitudUnion> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(SolicitudUnion unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SolicitudUnion unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}
