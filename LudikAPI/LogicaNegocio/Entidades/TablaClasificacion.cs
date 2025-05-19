
using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
	public class TablaClasificacion : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la TablaClasificacion  debe tener entre 3 y 50 caracteres.")]
        public String Nombre;

        public Medalla medallaAsociada;

        public List<PerfilEstudiante> participantes;

        [ForeignKey(nameof(Grupo))]
        public int grupoId { get; set; } // Clave foránea

        /// <see>vistaCompleta.observer.Observador#actualizar(vistaCompleta.observer.Observable, vistaCompleta.observer.Evento)</see>
        ///  
        public void actualizar()
		{

		}

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}

