using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Dominio;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace Dominio
{
	public class Equivalencia : IEntity, IValidable
    {
        public int Id { get; set; }

        public int Nota { get; set; }

        public List<Medalla> MedallasNecesarias { get; set; }

        public Equivalencia()
        {
        }

        public Equivalencia(int nota, List<Medalla> medallas)
        {
            Nota = nota;
            MedallasNecesarias = medallas ?? new List<Medalla>();
        }

        public Resultado esValido()
        {
            var errores = new List<Error>();

            if (Nota <= 0)
                errores.Add(new Error("Equivalencia.Nota.Invalida", "La nota debe ser un número positivo."));
            
            if (MedallasNecesarias == null || MedallasNecesarias.Count == 0)
                errores.Add(new Error("Equivalencia.Medallas.Vacias", "La equivalencia debe tener asociada al menos una medalla."));
            
            if (errores.Count > 0)
                return Resultado.Falla(errores);
            
            return Resultado.Exitoso();
        }
    }

}

