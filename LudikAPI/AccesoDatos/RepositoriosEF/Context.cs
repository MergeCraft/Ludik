using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AccesoDatos.RepositoriosEF
{
    public class Context : DbContext
    {
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



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ludik;Integrated Security=True;Encrypt=False");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().UseTpcMappingStrategy();

            modelBuilder.Entity<Estudiante>().ToTable("Estudiantes");
            modelBuilder.Entity<Profesor>().ToTable("Profesores");

            //Configurar Owned types de Usuario en Profesor
            modelBuilder.Entity<Profesor>(entity =>
            {
                // Owned NombreCompleto (propio de Usuario)
                entity.OwnsOne(p => p.NombreCompleto, nc =>
                {
                    nc.Property(x => x.Nombre).HasColumnName("Nombre");
                    nc.Property(x => x.Apellido).HasColumnName("Apellido");
                });

                // Owned NombreUsuario (propio de Usuario)
                entity.OwnsOne(p => p.NombreUsuario, nu =>
                {
                    nu.Property(x => x.Valor).HasColumnName("NombreUsuario")
                                              .IsRequired();
                    nu.HasIndex(x => x.Valor).IsUnique();
                });

                // Owned Contrasenia (propio de Usuario)
                entity.OwnsOne(p => p.Contrasenia, c =>
                {
                    c.Property(x => x.Valor).HasColumnName("Contrasenia")
                                             .IsRequired();
                });

                // Owned Email (propio de Profesor)
                entity.OwnsOne(p => p.email, correo =>
                {
                    correo.Property(x => x.Valor).HasColumnName("Email")
                                                 .IsRequired();
                    correo.HasIndex(x => x.Valor).IsUnique();
                });
            });

            //Configura Owned types de Usuario en Estudiante
            modelBuilder.Entity<Estudiante>(entity =>
            {
                // Owned NombreCompleto
                entity.OwnsOne(e => e.NombreCompleto, nc =>
                {
                    nc.Property(x => x.Nombre).HasColumnName("Nombre");
                    nc.Property(x => x.Apellido).HasColumnName("Apellido");
                });

                // Owned NombreUsuario
                entity.OwnsOne(e => e.NombreUsuario, nu =>
                {
                    nu.Property(x => x.Valor).HasColumnName("NombreUsuario")
                                              .IsRequired();
                    nu.HasIndex(x => x.Valor).IsUnique();
                });

                // Owned Contrasenia
                entity.OwnsOne(e => e.Contrasenia, c =>
                {
                    c.Property(x => x.Valor).HasColumnName("Contrasenia")
                                             .IsRequired();
                });

            });


            modelBuilder.Entity<Estudiante>()
                .OwnsOne(e => e.NombreUsuario)
                .HasIndex(e => e.Valor)
                .IsUnique();

            modelBuilder.Entity<Grupo>()
                .HasIndex(g => g.nombre);

            // Índice no único para listar rápidamente todos los grupos de un profesor
            modelBuilder.Entity<Grupo>()
                .HasIndex(g => g.ProfesorId)
                .HasDatabaseName("IX_Grupo_ProfesorId");

            // (Opcional) Índice único compuesto para evitar que un mismo estudiante se una dos veces
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

        }
    }
}
