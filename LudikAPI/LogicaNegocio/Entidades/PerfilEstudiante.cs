using System;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.Resultados;
using LogicaNegocio.Entidades;

namespace LogicaNegocio.Entidades
{
	public class PerfilEstudiante : IEntity, IValidable
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
        // (Opcional) Para acceso directo a Medalla:
        [NotMapped]
        public IEnumerable<Medalla> MedallasObtenidas => PerfilMedallas.Select(pm => pm.Medalla);

        public List<RendimientoPeriodo> HistorialRendimientoPeriodos { get; set; }

        public int GrupoId { get; set; }
        [ForeignKey(nameof(GrupoId))]
        public Grupo Grupo { get; set; }

        public List<Recompensa> Inventario { get; set; }

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
            /*
            if (Grupo?.TablaEquivalencia?.Equivalencias == null)
                return 0;
            var medallasAlumno = this.MedallasObtenidas.ToList();
            // Agrupar para contar repeticiones si fuese necesario:
            var conteoAlumno = medallasAlumno
                .GroupBy(m => m.Id)
                .ToDictionary(g => g.Key, g => g.Count());

            var equivalencias = Grupo.TablaEquivalencia.Equivalencias
                .OrderByDescending(e => e.Nota)
                .ToList();
            foreach (var eq in equivalencias)
            {
                // Agrupar medallas necesarias por Id:
                var conteoNecesario = eq.MedallasNecesarias
                    .GroupBy(m => m.Id)
                    .ToDictionary(g => g.Key, g => g.Count());
                bool cumple = true;
                foreach (var kv in conteoNecesario)
                {
                    if (!conteoAlumno.TryGetValue(kv.Key, out int cant) || cant < kv.Value)
                    {
                        cumple = false;
                        break;
                    }
                }
                if (cumple)
                    return eq.Nota;
            }
            return 0;


            */
        }


    }

}

