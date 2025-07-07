using System;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.Resultados;
using LogicaNegocio.Entidades;
using LogicaNegocio.Observer;

namespace LogicaNegocio.Entidades
{
	public class PerfilEstudiante : Observable<PerfilEstudianteMedalla>, IEntity, IValidable
    {
        public int Id { get; set; }

        public Avatar Avatar { get; private set; }

        public int MetaCalificacion { get; set; }

        public String EstudianteId { get; set; }

        [ForeignKey(nameof(EstudianteId))]
        public Estudiante Estudiante { get; set; }

        public int Monedas { get; set; }
        public string RutaImagenCompleta { get; set; }
        public string RutaImagenMiniatura { get; set; }
        public List<PerfilEstudianteMedalla> PerfilMedallas { get; set; } = new();

        [NotMapped]
        public IEnumerable<Medalla> MedallasObtenidas => PerfilMedallas.Select(pm => pm.Medalla);

        public List<RendimientoPeriodo> HistorialRendimientoPeriodos { get; set; }

        public int GrupoId { get; set; }
        [ForeignKey(nameof(GrupoId))]
        public Grupo Grupo { get; set; }

        public List<PerfilEstudianteRecompensa> InventarioRecompensas { get; set; } = new();

        public List<TablaClasificacion> TablasClasificacion { get; set; } = new();

        [NotMapped]
        public IEnumerable<Recompensa> Inventario =>
            InventarioRecompensas.Select(x => x.Recompensa);

        public BarraProgreso BarraProgreso { get; set; }



        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
        public int CalcularNotaActual()
        {
            if (Grupo == null)
                return 0;
            return Grupo.CalcularNotaDeEstudiante(MedallasObtenidas);
            
        }

        public List<PersonalizacionAvatar> ObtenerItemsAvatarDisponibles()
        {
            return Inventario?.OfType<PersonalizacionAvatar>().ToList() ?? new List<PersonalizacionAvatar>();
        }

        public Resultado EstablecerMetaDeCalificacion(int nuevaMeta)
        {
            int notaMaxDeTablaEquivalencia = Grupo.TablaEquivalencia.ObtenerNotaMaxima();
            if (nuevaMeta < 0 || notaMaxDeTablaEquivalencia < nuevaMeta)
            {
                return Resultado.Falla(new Error("Error.Validation", "La meta de calificación debe estar entre 0 y "+notaMaxDeTablaEquivalencia+"."));
            }


            this.MetaCalificacion = nuevaMeta;

            return Resultado.Exitoso();
        }
        public void NotifyMedallaAsignada(PerfilEstudianteMedalla asignacion)
        => Notify(asignacion);
    }

}

