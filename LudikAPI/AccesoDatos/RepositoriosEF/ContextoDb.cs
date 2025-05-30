using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AccesoDatos.RepositoriosEF
{
    public class ContextoDb : IdentityDbContext<Usuario>
    {
        public ContextoDb(DbContextOptions<ContextoDb> options) : base(options)
        {
        }

        // OnConfiguring queda de respaldo
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ludik;Integrated Security=True;Encrypt=False");
            }
        }

        //Aqui se definen las tablas de la base de datos
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Recompensa> Recompensas { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
        public DbSet<Medalla> Medallas { get; set; }
        public DbSet<PerfilEstudiante> PerfilesEstudiantes { get; set; }
        public DbSet<TablaClasificacion> TablasClasificacion { get; set; }
        public DbSet<TablaEquivalencia> TablasEquivalencia { get; set; }
        public DbSet<SolicitudUnion> SolicitudesUnion { get; set; }
        public DbSet<RendimientoPeriodo> RendimientosPeriodos { get; set; }
        public DbSet<PreguntaRespuestaSeguridad> PreguntasRespuestasSeguridad { get; set; }
        public DbSet<Potenciador> Potenciadores { get; set; }
        public DbSet<Pin> Pines { get; set; }
        public DbSet<Hito> Hitos { get; set; }
        public DbSet<Equivalencia> Equivalencias { get; set; }
        public DbSet<EnlaceUnion> EnlacesUnion { get; set; }
        public DbSet<BarraProgreso> BarrasProgreso { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().UseTptMappingStrategy();

            modelBuilder.Entity<Estudiante>().ToTable("Estudiantes");
            modelBuilder.Entity<Profesor>().ToTable("Profesores");

            modelBuilder.Entity<Estudiante>()
                .HasOne<Usuario>()
                .WithOne()
                .HasForeignKey<Estudiante>(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Profesor>()
                .HasOne<Usuario>()
                .WithOne()
                .HasForeignKey<Profesor>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<RendimientoPeriodo>(rp =>
            {
                // Le indicamos a EF que RangoFechas es un "owned type" de RendimientoPeriodo
                rp.OwnsOne(r => r.rangofecha, rf =>
                {
                    // Estas dos propiedades se incluirán como columnas en la tabla RendimientosPeriodos
                    rf.Property(x => x.fechaInicio)
                        .HasColumnName("FechaInicio")
                        .IsRequired();

                    rf.Property(x => x.fechaFin)
                        .HasColumnName("FechaFin")
                        .IsRequired();
                });
            });

            // CONFIGURACIÓN DE PERFIL ESTUDIANTE Y RELACIONES
            modelBuilder.Entity<PerfilEstudiante>(pe =>
            {
                // 1) Relación PerfilEstudiante → Estudiante (Uno a Muchos): 
                //    cuando se borre Estudiante, se eliminan sus perfiles.
                pe.HasOne<Estudiante>()
                  .WithMany(e => e.perfiles)
                  .HasForeignKey(p => p.EstudianteId)
                  .OnDelete(DeleteBehavior.Cascade);

                // 2) Relación 1:1 PerfilEstudiante → BarraProgreso: 
                //    La FK está en BarrasProgreso (perfilEstudianteId). 
                //    Usamos OnDelete(Cascade) aquí, para que al borrar el perfil también borre la barra.
                pe.HasOne(p => p.barraProgreso)
                    .WithOne()
                    .HasForeignKey<BarraProgreso>(b => b.perfilEstudianteId)
                    .OnDelete(DeleteBehavior.Cascade);

                // 3) Relación PerfilEstudiante → Grupo (Muchos a Uno):
                //    NO queremos cascada aquí (evita ciclos de múltiple cascada).
                pe.HasOne<Grupo>()
                  .WithMany(g => g.alumnos)
                  .HasForeignKey(p => p.GrupoId)
                  .OnDelete(DeleteBehavior.Restrict);             // SIN BORRADO EN CASCADA

                // 4)índice compuesto para evitar duplicados de GrupoId+EstudianteId
                pe.HasIndex(pe2 => new { pe2.GrupoId, pe2.EstudianteId })
                    .IsUnique()
                    .HasDatabaseName("UX_PerfilEstudiante_GrupoId_EstudianteId");



            });


            // Grupo - Índices
            modelBuilder.Entity<Grupo>()
                .HasIndex(g => g.nombre);
            modelBuilder.Entity<Grupo>()
                .HasIndex(g => g.ProfesorId)
                .HasDatabaseName("IX_Grupo_ProfesorId");

            // Índice único compuesto para evitar que un mismo estudiante se una dos veces
            modelBuilder.Entity<PerfilEstudiante>()
                .HasIndex(pe => new { pe.GrupoId, pe.EstudianteId })
                .IsUnique()
                .HasDatabaseName("UX_PerfilEstudiante_GrupoId_EstudianteId");

            modelBuilder.Entity<TablaEquivalencia>(te =>
            {
                te.HasIndex(x => x.Nombre);
            });

            modelBuilder.Entity<TablaClasificacion>(te =>
            {
                te.HasIndex(x => x.Nombre);
            });

            modelBuilder.Entity<Medalla>(m =>
            {
                m.HasIndex(x => x.Nombre);
            });

            modelBuilder.Entity<Recompensa>(r =>
            {
                r.HasIndex(x => x.Nombre);
            });

            modelBuilder.Entity<Usuario>(b =>
            {
                b.ToTable("Usuarios");                          // AspNetUsers → Usuarios
                b.Property(u => u.Id).HasColumnName("UsuarioId");
                b.Property(u => u.UserName).HasColumnName("NombreUsuario");
                b.Property(u => u.NormalizedUserName).HasColumnName("NombreUsuarioNormalizado");
                b.Property(u => u.Email).HasColumnName("Correo");
                b.Property(u => u.NormalizedEmail).HasColumnName("CorreoNormalizado");
                b.Property(u => u.EmailConfirmed).HasColumnName("CorreoConfirmado");
                b.Property(u => u.PasswordHash).HasColumnName("ContraseniaHash");
                b.Property(u => u.SecurityStamp).HasColumnName("EstampaSeguridad");
                b.Property(u => u.ConcurrencyStamp).HasColumnName("EstampaConcurrencia");
                b.Property(u => u.PhoneNumber).HasColumnName("Telefono");
                b.Property(u => u.PhoneNumberConfirmed).HasColumnName("TelefonoConfirmado");
                b.Property(u => u.TwoFactorEnabled).HasColumnName("AutenticacionDosFactores");
                b.Property(u => u.LockoutEnd).HasColumnName("FinBloqueo");
                b.Property(u => u.LockoutEnabled).HasColumnName("BloqueoHabilitado");
                b.Property(u => u.AccessFailedCount).HasColumnName("IntentosFallidos");
                
                //Indicamos que NombreCompleto es un objeto "owned" de Usuario,
                //    por lo que sus propiedades se incorporarán a la misma tabla "Usuarios".
                b.OwnsOne(u => u.NombreCompleto, nc =>
                {
                    nc.Property(x => x.Nombre)
                        .HasColumnName("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    nc.Property(x => x.Apellido)
                        .HasColumnName("Apellido")
                        .IsRequired()
                        .HasMaxLength(20);
                });
            });

            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.ToTable("Roles");                             // AspNetRoles → Roles
                b.Property(r => r.Id).HasColumnName("RolId");
                b.Property(r => r.Name).HasColumnName("NombreRol");
                b.Property(r => r.NormalizedName).HasColumnName("NombreRolNormalizado");
                b.Property(r => r.ConcurrencyStamp).HasColumnName("EstampaConcurrencia");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("UsuariosRoles");                     // AspNetUserRoles → UsuariosRoles
                b.Property(ur => ur.UserId).HasColumnName("UsuarioId");
                b.Property(ur => ur.RoleId).HasColumnName("RolId");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("ReclamacionesUsuario");               // AspNetUserClaims → ReclamacionesUsuario
                b.Property(uc => uc.Id).HasColumnName("ReclamacionUsuarioId");
                b.Property(uc => uc.UserId).HasColumnName("UsuarioId");
                b.Property(uc => uc.ClaimType).HasColumnName("TipoReclamacion");
                b.Property(uc => uc.ClaimValue).HasColumnName("ValorReclamacion");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("IniciosSesionUsuario");               // AspNetUserLogins → IniciosSesionUsuario
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
                b.Property(l => l.LoginProvider).HasColumnName("Proveedor");
                b.Property(l => l.ProviderKey).HasColumnName("ClaveProveedor");
                b.Property(l => l.ProviderDisplayName).HasColumnName("NombreProveedor");
                b.Property(l => l.UserId).HasColumnName("UsuarioId");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("ReclamacionesRoles");                 // AspNetRoleClaims → ReclamacionesRoles
                b.Property(rc => rc.Id).HasColumnName("ReclamacionRolId");
                b.Property(rc => rc.RoleId).HasColumnName("RolId");
                b.Property(rc => rc.ClaimType).HasColumnName("TipoReclamacion");
                b.Property(rc => rc.ClaimValue).HasColumnName("ValorReclamacion");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("TokensUsuario");                      // AspNetUserTokens → TokensUsuario
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
                b.Property(t => t.UserId).HasColumnName("UsuarioId");
                b.Property(t => t.LoginProvider).HasColumnName("Proveedor");
                b.Property(t => t.Name).HasColumnName("NombreToken");
                b.Property(t => t.Value).HasColumnName("ValorToken");
            });

        }
    }
}
