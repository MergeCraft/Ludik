using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.SolocitudUnionDTOs;
using LogicaAplicacion.InterfacesCasosUsos.SolicitudUnion;
using LogicaNegocio.ValueObject;

namespace LogicaAplicacion.ImplementacionCasosUsos.SolicitudUnion
{
    public class CrearSolicitudUnion : ICrearSolicitudUnion
    {
        private readonly IRepositorioEnlacesUnionGrupo _repoEnlaces;
        private readonly IRepositorioEstudiantes _repoEstudiantes;
        private readonly IRepositorioGrupos _repoGrupos;
        private readonly IRepositorioSolicitudesUnion _repoSolicitudes;

        public CrearSolicitudUnion(
            IRepositorioEnlacesUnionGrupo repoEnlaces,
            IRepositorioEstudiantes repoEstudiantes,
            IRepositorioGrupos repoGrupos,
            IRepositorioSolicitudesUnion repoSolicitudes)
        {
            _repoEnlaces = repoEnlaces;
            _repoEstudiantes = repoEstudiantes;
            _repoGrupos = repoGrupos;
            _repoSolicitudes = repoSolicitudes;
        }
        
        public async Task EjecutarAsync(SolicitudUnionDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            /*
            var enlace = await _repoEnlaces.ObtenerPorCodigoAsync(dto.CodigoEnlace);
            if (enlace == null || enlace.expiracion < DateTime.UtcNow)
                throw new KeyNotFoundException("El enlace es inválido o ha expirado.");
            //TODO: refactorizar para buscar estudiante por id del tipo string
            var estudiante = await _repoEstudiantes.GetByIdAsync(dto.IdEstudiante);
            if (estudiante == null)
                throw new KeyNotFoundException("El estudiante no existe.");

            var grupo = await _repoGrupos.ObtenerPorEnlaceAsync(enlace.codigoBase);
            if (grupo == null)
                throw new KeyNotFoundException("No se encontró el grupo asociado al enlace.");

            // Validación: ya tiene solicitud pendiente
            bool yaExiste = await _repoSolicitudes.ExisteSolicitudPendiente(dto.IdEstudiante, grupo.Id);
            if (yaExiste)
                throw new InvalidOperationException("Ya existe una solicitud pendiente para este estudiante y grupo.");

            var solicitud = new Dominio.SolicitudUnion
            {
                estudiante = estudiante,
                grupoId = grupo.Id,
                fecha = DateOnly.FromDateTime(DateTime.UtcNow),
                Estado = EstadoSolicitud.Pendiente
            };

            solicitud.EsValido(); // Validación de dominio

            await _repoSolicitudes.AddAsync(solicitud);
            */
        }
    }
}
