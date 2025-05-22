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
        public DbSet<Usuario> Usuarios { get; set; }
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


        //Configurar las entidades de la base de datos 
        //me gustaria agregar al nombreUsuario que sea unico con Data Annotations (en la entidad)eso se puede ?
        //En C# y Entity Framework, la unicidad no se puede garantizar directamente con Data Annotations, pero sí puedes hacerlo formas complementarias->
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.OwnsOne(p => p.correo, correo =>
                {
                    correo.HasIndex(c => c.Correo).IsUnique();
                });

                entity.OwnsOne(p => p.NombreUsuario, nombreUsuario =>
                {
                    nombreUsuario.HasIndex(nu => nu.Nombre).IsUnique();
                });
            });

            // Estudiante - NombreUsuario único
            modelBuilder.Entity<Estudiante>()
                .OwnsOne(e => e.NombreUsuario)
                .HasIndex(e => e.Nombre)
                .IsUnique();

            // Grupo - Índices
            modelBuilder.Entity<Grupo>()
                .HasIndex(g => g.Nombre);
            modelBuilder.Entity<Grupo>()
                .HasIndex(g => g.ProfesorId)
                .HasDatabaseName("IX_Grupo_ProfesorId");

            // PerfilEstudiante - clave compuesta única
            modelBuilder.Entity<PerfilEstudiante>()
                .HasIndex(pe => new { pe.GrupoId, pe.EstudianteId })
                .IsUnique()
                .HasDatabaseName("UX_PerfilEstudiante_GrupoId_EstudianteId");

            // SolicitudUnion
            modelBuilder.Entity<SolicitudUnion>()
                .HasOne(s => s.Estudiante)
                .WithMany()
                .HasForeignKey(s => s.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudUnion>()
                .HasOne(s => s.Grupo)
                .WithMany(g => g.Solicitudes)
                .HasForeignKey(s => s.GrupoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Grupo -> TablaEquivalencia
            modelBuilder.Entity<Grupo>()
                .HasOne(g => g.TablaEquivalencia)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); 

            // Grupo -> Tienda
            modelBuilder.Entity<Grupo>()
                .HasOne(g => g.Tienda)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Grupo -> EnlaceUnion
            modelBuilder.Entity<Grupo>()
                .HasOne(g => g.EnlaceUnion)
                .WithOne()
                .HasForeignKey<Grupo>(g => g.EnlaceUnionId)
                .OnDelete(DeleteBehavior.Restrict);

            // PerfilEstudiante
            modelBuilder.Entity<PerfilEstudiante>()
                .HasOne(pe => pe.Estudiante)
                .WithMany(e => e.perfiles)
                .HasForeignKey(pe => pe.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PerfilEstudiante>()
                .HasOne(pe => pe.Grupo)
                .WithMany(g => g.Alumnos)
                .HasForeignKey(pe => pe.GrupoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Otros índices
            modelBuilder.Entity<TablaEquivalencia>().HasIndex(te => te.Nombre);
            modelBuilder.Entity<TablaClasificacion>().HasIndex(tc => tc.Nombre);
            modelBuilder.Entity<Medalla>().HasIndex(m => m.Nombre);
            modelBuilder.Entity<Recompensa>().HasIndex(r => r.Nombre);

        }
    }
}
