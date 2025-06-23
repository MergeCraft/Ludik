using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public class TablaEquivalencia : IEntity, IValidable
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la tabla debe tener entre 3 y 50 caracteres.")]
        public string Nombre { get; set; }

        public string ProfesorId { get; set; }
        public List<Equivalencia> Equivalencias { get; set; }

        public TablaEquivalencia()
        {
        }

        public TablaEquivalencia(string nombre, string profesorId)
        {
            Nombre = nombre;
            ProfesorId = profesorId;
            Equivalencias = new List<Equivalencia>();
        }

		public Equivalencia siguienteEquivalencia(List<Medalla> medallas)
		{
			return null;
		}

		public int hallarValorMaxDeTabla()
		{
			return 0;
		}


        public Resultado esValido()
        {
            var errores = new List<Error>();


            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3 || Nombre.Length > 50)
                errores.Add(new Error("Error.Validation", "El nombre de la Tabla debe tener entre 3 y 50 caracteres."));
            

            // Si no hay equivalencias, la tabla está "vacía" pero es válida en ese estado.
            // Las reglas de negocio complejas aplican cuando hay al menos una equivalencia.
            if (Equivalencias == null || !Equivalencias.Any())
                return errores.Any() ? Resultado.Falla(errores) : Resultado.Exitoso();
            

            // Validar que no haya notas repetidas
            var notasSet = new HashSet<int>();
            if (Equivalencias.Any(e => !notasSet.Add(e.Nota)))
                errores.Add(new Error("Error.Validation", "No pueden existir valores de nota repetidos en la tabla."));
            

            var equivalenciasOrdenadas = Equivalencias.OrderBy(e => e.Nota).ToList();

            // Validar que las notas son progresivas y secuenciales desde 1
            for (int i = 0; i < equivalenciasOrdenadas.Count; i++)
            {
                if (equivalenciasOrdenadas[i].Nota != i + 1)
                {
                    errores.Add(new Error("Error.Validation", "Las notas deben ser una secuencia progresiva iniciando en 1 (1, 2, 3...)."));
                    break;
                }
            }

            // Validar que las medallas son acumulativas
            if (!errores.Any(e => e.Codigo == "Error.Validation"))
            {
                for (int i = 1; i < equivalenciasOrdenadas.Count; i++)
                {
                    var medallasAnteriores = new HashSet<Medalla>(equivalenciasOrdenadas[i - 1].MedallasNecesarias);
                    var medallasActuales = new HashSet<Medalla>(equivalenciasOrdenadas[i].MedallasNecesarias);

                    // La lista de medallas actual debe contener todas las medallas de la nota anterior.
                    if (!medallasAnteriores.IsSubsetOf(medallasActuales))
                        errores.Add(new Error("Error.Validation", $"La nota {equivalenciasOrdenadas[i].Nota} debe incluir todas las medallas de la nota {equivalenciasOrdenadas[i - 1].Nota}."));
                    
                }
            }


            foreach (var equivalencia in Equivalencias)
            {
                var resultadoValidacionEquivalencia = equivalencia.esValido();

                if (resultadoValidacionEquivalencia.EsFallo)
                    errores.AddRange(resultadoValidacionEquivalencia.Errores);
                
            }

            if (errores.Count > 0)
                return Resultado.Falla(errores.Distinct());
            

            return Resultado.Exitoso();
        }

        public void AgregarEquivalencia(Equivalencia nuevaEquivalencia)
        {
            Equivalencias.Add(nuevaEquivalencia);
        }

        public void Actualizar(string nuevoNombre, List<Equivalencia> nuevasEquivalencias)
        {
            this.Nombre = nuevoNombre;

            // La forma más robusta de manejar actualizaciones de colecciones con EF Core
            // es limpiar la colección existente y agregar los nuevos elementos.
            // EF Core detectará los cambios (eliminados y agregados) al guardar.
            this.Equivalencias.Clear();

            if (nuevasEquivalencias != null)
            {
                foreach (var eq in nuevasEquivalencias)
                {
                    this.Equivalencias.Add(eq);
                }
            }
        }

        public int MaximaCalificacionSegun(IEnumerable<Medalla> medallasObtenidas)
        {
            Equivalencias.Sort((a, b) => b.Nota.CompareTo(a.Nota)); // Ordenar de mayor a menor
            foreach (var eq in Equivalencias)
            {
                if (eq.CumpleMedallasNecesarias(medallasObtenidas))
                    return eq.Nota;
            }

            // Si no se cumple ninguna equivalencia, retornar 0
            return 0;

        }
    }

}

