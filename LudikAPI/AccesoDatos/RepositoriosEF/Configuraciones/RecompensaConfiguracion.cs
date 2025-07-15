using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccesoDatos.RepositoriosEF.Configuraciones;

public class RecompensaConfiguracion : IEntityTypeConfiguration<Recompensa>
{
    public void Configure(EntityTypeBuilder<Recompensa> builder)
    {
        builder.HasIndex(x => x.Nombre);

        builder.HasDiscriminator<string>("RecompensaTipo")
            .HasValue<RecompensaSimple>("Simple")
            .HasValue<PersonalizacionAvatar>("PersonalizacionAvatar")
            .HasValue<Potenciador>("Potenciador");
    }
}