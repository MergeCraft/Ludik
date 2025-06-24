using System;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public abstract class Recompensa : IEntity, IValidable
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre Recompensa  debe tener entre 3 y 50 caracteres.")]
        public string Nombre { get; set; }
        
        public string RutaImagenCompleta { get; set; }
        public string RutaImagenMiniatura { get; set; }

        public int Precio { get; set; }
        public int TiendaId { get; set; }
        public Tienda Tienda { get; set; }
        public Resultado esValido()
        {
            var errores = new List<Error>();
            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3 || Nombre.Length > 50)
                errores.Add(new Error("Error.Validation", "El nombre de la recompensa debe tener entre 3 y 50 caracteres."));
            if (Precio < 0)
                errores.Add(new Error("Error.Validation", "El precio de la recompensa debe ser >= 0."));
            // Opcional: validar que rutas no sean nulas o que tengan formato de URL:
            if (string.IsNullOrWhiteSpace(RutaImagenCompleta))
                errores.Add(new Error("Error.Validation", "Debe especificar la ruta de la imagen completa."));
            if (string.IsNullOrWhiteSpace(RutaImagenMiniatura))
                errores.Add(new Error("Error.Validation", "Debe especificar la ruta de la miniatura."));
            // Podrías validar formato de URL con Uri.IsWellFormedUriString, si requiere:
            // if (!Uri.IsWellFormedUriString(RutaImagenCompleta, UriKind.Absolute)) ...
            if (errores.Any())
                return Resultado.Falla(errores);
            return Resultado.Exitoso();
        }

        public void otorgar(PerfilEstudiante pEstudiante)
		{

		}

		public void pagar(int precio)
		{

		}

	}

}

