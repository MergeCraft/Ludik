using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccesoDatos.RepositoriosEF.Configuraciones;

public class PerfilEstudianteConfiguracion : IEntityTypeConfiguration<PerfilEstudiante>
{
    public void Configure(EntityTypeBuilder<PerfilEstudiante> builder)
    {
        throw new NotImplementedException();
    }
}