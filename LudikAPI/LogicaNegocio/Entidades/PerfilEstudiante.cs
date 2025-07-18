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
        public List<PerfilEstudianteMedalla> PerfilMedallas { get; set; }
        public int CantidadKudosDisponibles { get; set; }
        public List<KudoOtorgado> KudosOtorgados { get; private set; }
        public List<KudoOtorgado> KudosRecibidos { get; private set; }

        [NotMapped]
        public IEnumerable<Medalla> MedallasObtenidas => PerfilMedallas.Select(pm => pm.Medalla);

        public List<RendimientoPeriodo> HistorialRendimientoPeriodos { get; set; }

        public int GrupoId { get; set; }
        [ForeignKey(nameof(GrupoId))]
        public Grupo Grupo { get; set; }

        public List<PerfilEstudianteRecompensa> InventarioRecompensas { get; set; }

        public List<TablaClasificacion> TablasClasificacion { get; set; }

        [NotMapped]
        public IEnumerable<Recompensa> Inventario =>
            InventarioRecompensas.Select(x => x.Recompensa);

        public BarraProgreso BarraProgreso { get; set; }

        public int? PotenciadorActivoId { get; set; }

        public Potenciador? PotenciadorActivo { get; set; }

        public PerfilEstudiante()
        {
            this.PerfilMedallas = new List<PerfilEstudianteMedalla>();
            this.KudosOtorgados = new List<KudoOtorgado>();
            this.KudosRecibidos = new List<KudoOtorgado>();
            this.InventarioRecompensas = new List<PerfilEstudianteRecompensa>();
            this.TablasClasificacion = new List<TablaClasificacion>();
        }

        public void ActivarPotenciador(Potenciador p)
        {
            p.FechaActivacion = DateTime.UtcNow;
            PotenciadorActivo = p;
            PotenciadorActivoId = p.Id;
        }
        public void LimpiarPotenciadorExpirado()
        {
            if (PotenciadorActivo != null && !PotenciadorActivo.EstaActivo)
                PotenciadorActivo = null;
        }
        public double ObtenerMultiplicadorMonedas()
        {
            return PotenciadorActivo?.Multiplicador ?? 1.0;
        }
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
        => Notificar(asignacion);

        /// <summary>
        /// Encapsula la lógica de negocio para otorgar un kudo.
        /// Verifica si hay kudos disponibles y descuenta uno.
        /// </summary>
        /// <returns>Un resultado exitoso si se pudo otorgar, o de falla en caso contrario.</returns>
        public Resultado<KudoOtorgado> OtorgarKudo(TipoKudo tipoKudo, PerfilEstudiante perfilReceptor)
        {
            if (CantidadKudosDisponibles <= 0)
                return Resultado<KudoOtorgado>.Falla(new Error("Error.Validation", "No tienes Kudos disponibles esta semana. Recibirás más el próximo lunes."));
            

            CantidadKudosDisponibles--;
            var kudoOtorgado = new KudoOtorgado(this, perfilReceptor, tipoKudo, DateTime.UtcNow);
            
            return Resultado<KudoOtorgado>.Exitoso(kudoOtorgado);
        }

        public void EvaluarAsignarMedallaPorKudos(UmbralParaMedallaPorKudos umbral)
        {
            // Obtener solo los kudos que no han sido usados para NINGUNA medalla.
            var kudosDisponibles = this.KudosRecibidos
                .Where(k => k.TipoKudoId == umbral.TipoKudoId && k.PerfilEstudianteMedallaId == null)
                .ToList();

            if (kudosDisponibles.Count >= umbral.CantidadKudos)
            {
                var nuevaAsignacionMedalla = new PerfilEstudianteMedalla
                {
                    PerfilEstudiante = this,
                    Medalla = umbral.Medalla,
                };

                var kudosAGastar = kudosDisponibles.Take(umbral.CantidadKudos).ToList();
                foreach (var kudo in kudosAGastar)
                {
                    kudo.AsignacionMedalla = nuevaAsignacionMedalla; 
                }

                this.PerfilMedallas.Add(nuevaAsignacionMedalla);

                //TODO: Notificar a los observadores.
                this.NotifyMedallaAsignada(nuevaAsignacionMedalla);
            }
        }
    }

}

