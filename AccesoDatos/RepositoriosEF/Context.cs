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
        public DbSet<PIN> Pines { get; set; }
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

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>()
                .HasIndex(a => new { a.Nombre, a.Codigo })
                .IsUnique();
            modelBuilder.Entity<Articulo>()
                .HasIndex(a => a.Codigo)
            .IsUnique();

            modelBuilder.Entity<Configuracion>()
                .HasIndex(p => p.Nombre)
                .IsUnique();

        }
        */
    }
}
