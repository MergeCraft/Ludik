using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF.Configuraciones
{
    public class SolicitudPerfilMedallaConfiguracion
        : IEntityTypeConfiguration<SolicitudPerfilMedalla>
    {
        public void Configure(EntityTypeBuilder<SolicitudPerfilMedalla> builder)
        {
            builder.HasKey(spm => spm.Id);

            builder.HasIndex(spm => spm.PerfilEstudianteMedallaId);
            builder.HasIndex(spm => spm.GrupoId);

            builder.HasOne(spm => spm.PerfilEstudianteMedalla)
                .WithMany() 
                .HasForeignKey(spm => spm.PerfilEstudianteMedallaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(spm => spm.Grupo)
                .WithMany(gr => gr.SolicitudesPerfilMedalla)
                .HasForeignKey(spm => spm.GrupoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(spm => spm.Descripcion)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(spm => spm.Fecha)
                .IsRequired();
        }
    }
}
