using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class PerfilEstudiantePotenciadorConfiguracion
    : IEntityTypeConfiguration<PerfilEstudiantePotenciador>
{
    public void Configure(EntityTypeBuilder<PerfilEstudiantePotenciador> builder)
    {
        builder.HasKey(pa => pa.PerfilEstudianteId);

        builder.HasOne(pa => pa.PerfilEstudiante)
               .WithOne(p => p.PotenciadorActivo)
               .HasForeignKey<PerfilEstudiantePotenciador>(
                   pa => pa.PerfilEstudianteId
               );

        builder.HasOne(pa => pa.Potenciador)
               .WithMany()   // sin colección inversa
               .HasForeignKey(pa => pa.PotenciadorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
