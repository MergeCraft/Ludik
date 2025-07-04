using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccesoDatos.RepositoriosEF.Configuraciones;

public class PerfilEstudianteRecompensaConfiguracion : IEntityTypeConfiguration<PerfilEstudianteRecompensa>
{
    public void Configure(EntityTypeBuilder<PerfilEstudianteRecompensa> builder)
    {
        builder.ToTable("PerfilEstudianteRecompensas");

        // Clave primaria compuesta
        builder.HasKey(x => new { x.PerfilEstudianteId, x.RecompensaId });

        // Si se borra el Perfil, se borran sus recompensas del inventario.
        builder.HasOne(x => x.PerfilEstudiante)
            .WithMany(pe => pe.InventarioRecompensas)
            .HasForeignKey(x => x.PerfilEstudianteId)
            .OnDelete(DeleteBehavior.Cascade);

        // No se puede borrar una Recompensa si está en el inventario de un perfil.
        builder.HasOne(x => x.Recompensa)
            .WithMany()
            .HasForeignKey(x => x.RecompensaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}