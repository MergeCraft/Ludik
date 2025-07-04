using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccesoDatos.RepositoriosEF.Configuraciones;

public class TablaClasificacionConfiguracion : IEntityTypeConfiguration<TablaClasificacion>
{
    public void Configure(EntityTypeBuilder<TablaClasificacion> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(x => x.Nombre);

        builder.Property(t => t.Nombre)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(t => t.MedallaAsociada)
            .WithMany()
            .HasForeignKey(t => t.MedallaAsociadaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Grupo)
            .WithMany(g => g.TablasClasificacion)
            .HasForeignKey(t => t.GrupoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación M:N con PerfilEstudiante (Participantes)
        builder.HasMany(t => t.Participantes)
            .WithMany(p => p.TablasClasificacion)
            .UsingEntity<Dictionary<string, object>>(
                "TablaClasificacionParticipantes",
                j => j
                    .HasOne<PerfilEstudiante>()
                    .WithMany()
                    .HasForeignKey("PerfilEstudianteId")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne<TablaClasificacion>()
                    .WithMany()
                    .HasForeignKey("TablaClasificacionId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("TablaClasificacionId", "PerfilEstudianteId");
                    j.ToTable("TablaClasificacionParticipantes");
                });
    }
}