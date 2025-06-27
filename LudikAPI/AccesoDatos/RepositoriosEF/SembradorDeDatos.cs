using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public static class SembradorDeDatos
    {
        public static void Semilla(this ModelBuilder modelBuilder)
        {
            // =================================================================
            // --- INICIO DE LA PRECARGA DE DATOS (DATA SEEDING) ---
            // =================================================================

            // 1. DEFINICIÓN DE ROLES
            var rolProfesorId = "2c5e174e-3b0e-446f-86af-483d56fd7210";
            var rolEstudianteId = "3d5e174e-3b0e-446f-86af-483d56fd7211";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = rolProfesorId, Name = "Profesor", NormalizedName = "PROFESOR" },
                new IdentityRole { Id = rolEstudianteId, Name = "Estudiante", NormalizedName = "ESTUDIANTE" }
            );
            
            // 2. CREACIÓN DE PROFESORES Y ESTUDIANTES

            // Profesores
            var profesor1Id = "8e445865-a24d-4543-a6c6-9443d048cdb9";
            var profesor2Id = "9e445865-a24d-4543-a6c6-9443d048cdb0";

            modelBuilder.Entity<Profesor>().HasData(
                new Profesor
                {
                    Id = profesor1Id,
                    UserName = "cecilia",
                    NormalizedUserName = "CECILIA",
                    Email = "cecilia@gmail.com",
                    NormalizedEmail = "CECILIA@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==", //Cecilia1.
                    SecurityStamp = "STATIC_SECURITY_STAMP_1",
                    ConcurrencyStamp = "b0c8b6a8-8e6b-4e6a-9e1e-2e0b166a9c76"
                },
                new Profesor
                {
                    Id = profesor2Id,
                    UserName = "laura",
                    NormalizedUserName = "LAURA.FERNANDEZ",
                    Email = "laura.fernandez@ludik.edu.uy",
                    NormalizedEmail = "LAURA.FERNANDEZ@LUDIK.EDU.UY",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==",
                    SecurityStamp = "STATIC_SECURITY_STAMP_2",
                    ConcurrencyStamp = "a1d3b5e7-9f2d-4b8c-8a1e-3f0e2d5b4a6b"
                }
            );
            

            // Estudiantes
            var estudiante1Id = "a1445865-a24d-4543-a6c6-9443d048cdb1";
            var estudiante2Id = "b2445865-a24d-4543-a6c6-9443d048cdb2";
            var estudiante3Id = "c3445865-a24d-4543-a6c6-9443d048cdb3";
            var estudiante4Id = "d4445865-a24d-4543-a6c6-9443d048cdb4";
            var estudiante5Id = "e5445865-a24d-4543-a6c6-9443d048cdb5";

            modelBuilder.Entity<Estudiante>().HasData(
                new Estudiante { Id = estudiante1Id, UserName = "santiago", NormalizedUserName = "SANTIAGO", Email = null, NormalizedEmail = null, EmailConfirmed = false, PasswordHash = "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==", SecurityStamp = "STATIC_SECURITY_STAMP_3", ConcurrencyStamp = "c4b6e8a0-1d3f-4e9a-9c8e-5d2a4f6b8c0d" },
                new Estudiante { Id = estudiante2Id, UserName = "valentina", NormalizedUserName = "VALENTINA", Email = null, NormalizedEmail = null, EmailConfirmed = false, PasswordHash = "AQAAAAIAAYagAAAAENuS3fE5d1k/aN2zV8mY9wR8cI7qU5kY4tL6wP9eO3bF0dG1sS5nC2vX3jJ4oP7eWw==", SecurityStamp = "STATIC_SECURITY_STAMP_4", ConcurrencyStamp = "d5c7f9b1-2e4g-5f0b-a0d9-6e3b5g7c9d1e"},
                new Estudiante { Id = estudiante3Id, UserName = "matias", NormalizedUserName = "MATIAS", Email = null, NormalizedEmail = null, EmailConfirmed = false, PasswordHash = "AQAAAAIAAYagAAAAENuS3fE5d1k/aN2zV8mY9wR8cI7qU5kY4tL6wP9eO3bF0dG1sS5nC2vX3jJ4oP7eWw==", SecurityStamp = "STATIC_SECURITY_STAMP_5", ConcurrencyStamp = "e6d80ac2-3f5h-6g1c-b1e0-7f4c6h8d0e2f" },
                new Estudiante { Id = estudiante4Id, UserName = "camila", NormalizedUserName = "CAMILA", Email = null, NormalizedEmail = null, EmailConfirmed = false, PasswordHash = "AQAAAAIAAYagAAAAENuS3fE5d1k/aN2zV8mY9wR8cI7qU5kY4tL6wP9eO3bF0dG1sS5nC2vX3jJ4oP7eWw==", SecurityStamp = "STATIC_SECURITY_STAMP_6", ConcurrencyStamp = "f7e91bd3-4g6i-7h2d-c2f1-8g5d7i9e1f3g" },
                new Estudiante { Id = estudiante5Id, UserName = "lucas", NormalizedUserName = "LUCAS", Email = null, NormalizedEmail = null, EmailConfirmed = false, PasswordHash = "AQAAAAIAAYagAAAAENuS3fE5d1k/aN2zV8mY9wR8cI7qU5kY4tL6wP9eO3bF0dG1sS5nC2vX3jJ4oP7eWw==", SecurityStamp = "STATIC_SECURITY_STAMP_7", ConcurrencyStamp = "g8f02ce4-5h7j-8i3e-d3g2-9h6e8j0f2g4h" }
             );

            modelBuilder.Entity<Usuario>().OwnsOne(u => u.NombreCompleto).HasData(
                // Datos de Profesores
                new { UsuarioId = profesor1Id, Nombre = "Carlos", Apellido = "Rodríguez" },
                new { UsuarioId = profesor2Id, Nombre = "Laura", Apellido = "Fernández" },
                // Datos de Estudiantes
                new { UsuarioId = estudiante1Id, Nombre = "Santiago", Apellido = "Pérez" },
                new { UsuarioId = estudiante2Id, Nombre = "Valentina", Apellido = "Gómez" },
                new { UsuarioId = estudiante3Id, Nombre = "Matías", Apellido = "González" },
                new { UsuarioId = estudiante4Id, Nombre = "Camila", Apellido = "Martínez" },
                new { UsuarioId = estudiante5Id, Nombre = "Lucas", Apellido = "Silva" }
            );

            // 3. ASIGNACIÓN DE ROLES A USUARIOS
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = profesor1Id, RoleId = rolProfesorId }, 
                new IdentityUserRole<string> { UserId = profesor2Id, RoleId = rolProfesorId },
                new IdentityUserRole<string> { UserId = estudiante1Id, RoleId = rolEstudianteId }, 
                new IdentityUserRole<string> { UserId = estudiante2Id, RoleId = rolEstudianteId }, 
                new IdentityUserRole<string> { UserId = estudiante3Id, RoleId = rolEstudianteId }, 
                new IdentityUserRole<string> { UserId = estudiante4Id, RoleId = rolEstudianteId }, 
                new IdentityUserRole<string> { UserId = estudiante5Id, RoleId = rolEstudianteId }
            );
            
            // 4. CREACIÓN DE MEDALLAS
            var medalla1Id = 1;
            var medalla2Id = 2;
            var medalla3Id = 3;
            modelBuilder.Entity<Medalla>().HasData(
                new Medalla { Id = medalla1Id, Nombre = "Participación Perfecta", Descripcion = "Asistencia y participación en todas las clases del mes.", UrlImagenMiniatura = "icono_asistencia.png", MonedasOtorgadas = 30, ProfesorId = profesor1Id },
                new Medalla { Id = medalla2Id, Nombre = "Maestro de la Colaboración", Descripcion = "Ayuda destacada a compañeros en proyectos grupales.", UrlImagenMiniatura = "icono_colaboracion.png", MonedasOtorgadas = 25, ProfesorId = profesor1Id },
                new Medalla { Id = medalla3Id, Nombre = "Mente Curiosa", Descripcion = "Realización de preguntas perspicaces que enriquecen la clase.", UrlImagenMiniatura = "icono_pregunta.png", MonedasOtorgadas = 15, ProfesorId = profesor2Id }
            );

            // 5. CREACIÓN DE TABLAS DE EQUIVALENCIA (sin las equivalencias dentro aún)
            var tablaEq1Id = 1;
            var tablaEq2Id = 2;
            modelBuilder.Entity<TablaEquivalencia>().HasData(
                new TablaEquivalencia { Id = tablaEq1Id, Nombre = "Calificaciones Estándar (C. Rodríguez)", ProfesorId = profesor1Id },
                new TablaEquivalencia { Id = tablaEq2Id, Nombre = "Evaluación Continua (L. Fernández)", ProfesorId = profesor2Id }
            );

            // 6. CREACIÓN DE EQUIVALENCIAS (asociadas a una Tabla)
            var equivalencia1Id = 1;
            var equivalencia2Id = 2;
            var equivalencia3Id = 3;
            modelBuilder.Entity<Equivalencia>().HasData(
                // Equivalencias para la Tabla 1
                new Equivalencia { Id = equivalencia1Id, Nota = 1, TablaEquivalenciaId = tablaEq1Id },
                new Equivalencia { Id = equivalencia2Id, Nota = 2, TablaEquivalenciaId = tablaEq1Id },
                // Equivalencia para la Tabla 2
                new Equivalencia { Id = equivalencia3Id, Nota = 1, TablaEquivalenciaId = tablaEq2Id }
            );

            // 7. VINCULACIÓN DE EQUIVALENCIAS Y MEDALLAS (TABLA DE UNIÓN MANY-TO-MANY)
            modelBuilder.Entity("EquivalenciaMedallas").HasData(
                // Equivalencia 1 (Nota 1) -> Requiere Medalla 1
                new { EquivalenciaId = equivalencia1Id, MedallaId = medalla1Id },
                // Equivalencia 2 (Nota 2) -> Requiere Medalla 1 Y Medalla 2 (acumulativo)
                new { EquivalenciaId = equivalencia2Id, MedallaId = medalla1Id },
                new { EquivalenciaId = equivalencia2Id, MedallaId = medalla2Id },
                // Equivalencia 3 (Nota 1) -> Requiere Medalla 3
                new { EquivalenciaId = equivalencia3Id, MedallaId = medalla3Id }
            );

            // 8. CREACIÓN DE ENLACES DE UNIÓN
            var enlace1Id = 1;
            var enlace2Id = 2;
            var fechaCreacion = new DateTime(2025, 6, 19, 10, 30, 0, DateTimeKind.Utc);

            modelBuilder.Entity<EnlaceUnion>().HasData(
                new EnlaceUnion { Id = enlace1Id, CodigoUnico = "MAT1A25", UrlCompleta = "https://www.ludik.app/unirse/MAT1A25", Expiracion = fechaCreacion.AddDays(300) },
                new EnlaceUnion { Id = enlace2Id, CodigoUnico = "HISTU25", UrlCompleta = "https://www.ludik.app/unirse/HISTU25", Expiracion = fechaCreacion.AddDays(300) }
            );

            // 9. CREACIÓN DE GRUPOS
            var grupo1Id = 1;
            var grupo2Id = 2;
            modelBuilder.Entity<Grupo>().HasData(
                new Grupo { Id = grupo1Id, Nombre = "Matemática 1A - 2025", Institucion = "Liceo N°5", Materia = "Matemática", FCreacion = fechaCreacion, ProfesorId = profesor1Id, TablaEquivalenciaId = tablaEq1Id, EnlaceUnionId = enlace1Id },
                new Grupo { Id = grupo2Id, Nombre = "Historia Universal - 2025", Institucion = "Liceo N°5", Materia = "Historia", FCreacion = fechaCreacion, ProfesorId = profesor2Id, TablaEquivalenciaId = tablaEq2Id, EnlaceUnionId = enlace2Id }
            );

            // 10. CREACIÓN DE PERFILES DE ESTUDIANTE (VINCULANDO ESTUDIANTES Y GRUPOS)
            modelBuilder.Entity<PerfilEstudiante>().HasData(
                new PerfilEstudiante { Id = 1, Monedas = 120, MetaCalificacion = 8, EstudianteId = estudiante1Id, GrupoId = grupo1Id, RutaImagenCompleta = "default/avatar_full.jpg", RutaImagenMiniatura = "default/avatar_thumb.jpg" },
                new PerfilEstudiante { Id = 2, Monedas = 150, MetaCalificacion = 9, EstudianteId = estudiante2Id, GrupoId = grupo1Id, RutaImagenCompleta = "default/avatar_full.jpg", RutaImagenMiniatura = "default/avatar_thumb.jpg" },
                new PerfilEstudiante { Id = 3, Monedas = 95, MetaCalificacion = 7, EstudianteId = estudiante3Id, GrupoId = grupo1Id, RutaImagenCompleta = "default/avatar_full.jpg", RutaImagenMiniatura = "default/avatar_thumb.jpg" },
                new PerfilEstudiante { Id = 4, Monedas = 200, MetaCalificacion = 10, EstudianteId = estudiante4Id, GrupoId = grupo2Id, RutaImagenCompleta = "default/avatar_full.jpg", RutaImagenMiniatura = "default/avatar_thumb.jpg" },
                new PerfilEstudiante { Id = 5, Monedas = 180, MetaCalificacion = 8, EstudianteId = estudiante5Id, GrupoId = grupo2Id, RutaImagenCompleta = "default/avatar_full.jpg", RutaImagenMiniatura = "default/avatar_thumb.jpg" }
            );
            // =================================================================
            // --- INICIO DE LA PRECARGA DE ATRIBUTOS DE AVATAR ---
            // =================================================================
            PrecargarAtributosAvatar(modelBuilder);

        }
        private static void PrecargarAtributosAvatar(ModelBuilder modelBuilder)
        {
            int idCounter = 1;
            var atributos = new List<AtributoAvatar>();

            // Función auxiliar para capitalizar nombres
            Func<string, string> capitalizar = s => char.ToUpper(s[0]) + s.Substring(1);

            // Datos proporcionados
            var datos = new Dictionary<TipoAtributo, string[]>
            {
                { TipoAtributo.Pelo, new[] { "bigHair", "bob", "bun", "curly", "curvy", "dreads", "dreads01", "dreads02", "frida", "frizzle", "fro", "froBand", "hat", "hijab", "longButNotTooLong", "miaWallace", "shaggy", "shaggyMullet", "shavedSides", "shortCurly", "shortFlat", "shortRound", "shortWaved", "sides", "straight01", "straight02", "straightAndStrand", "theCaesar", "theCaesarAndSidePart", "turban", "winterHat1", "winterHat02", "winterHat03", "winterHat04" } },
                { TipoAtributo.Cejas, new[] { "angry", "angryNatural", "default", "defaultNatural", "flatNatural", "frownNatural", "raisedExcited", "raisedExcitedNatural", "sadConcerned", "sadConcernedNatural", "unibrowNatural", "upDown", "upDownNatural" } },
                { TipoAtributo.Ojos, new[] { "closed", "cry", "default", "eyeRoll", "happy", "hearts", "side", "squint", "surprised", "wink", "winkWacky", "xDizzy" } },
                { TipoAtributo.Boca, new[] { "concerned", "default", "disbelief", "eating", "grimace", "sad", "screamOpen", "serious", "smile", "tongue", "twinkle" } },
                { TipoAtributo.Barba, new[] { "beardLight", "beardMajestic", "beardMedium", "moustacheFancy", "moustacheMagnum" } },
                { TipoAtributo.Gafas, new[] { "eyepatch", "kurt", "prescription01", "prescription02", "round", "sunglasses", "wayfarers" } },
                { TipoAtributo.Ropa, new[] { "blazerAndShirt", "blazerAndSweater", "collarAndSweater", "graphicShirt", "hoodie", "overall", "shirtCrewNeck", "shirtScoopNeck", "shirtVNeck" } },
                { TipoAtributo.ColorPiel, new[] { "614335", "ae5d29", "d08b5b", "edb98a", "f8d25c", "fd9841", "ffdbb4" } },
                { TipoAtributo.ColorPelo, new[] { "2c1b18", "4a312c", "724133", "a55728", "b58143", "c93305", "d6b370", "e8e1e1", "ecdcbf", "f59797" } },
                { TipoAtributo.ColorBarba, new[] { "2c1b18", "4a312c", "724133", "a55728", "b58143", "c93305", "d6b370", "e8e1e1", "ecdcbf", "f59797" } },
                { TipoAtributo.ColorRopa, new[] { "3c4f5c", "65c9ff", "262e33", "5199e4", "25557c", "929598", "a7ffc4", "b1e2ff", "e6e6e6", "ff5c5c", "ff488e", "ffafb9", "ffffb1", "ffffff" } },
                { TipoAtributo.ColorGafas, new[] { "3c4f5c", "65c9ff", "262e33", "5199e4", "25557c", "929598", "a7ffc4", "b1e2ff", "e6e6e6", "ff5c5c", "ff488e", "ffafb9", "ffdeb5", "ffffb1", "ffffff" } }
            };

            foreach (var kvp in datos)
            {
                foreach (var codigo in kvp.Value)
                {
                    atributos.Add(new AtributoAvatar
                    {
                        Id = idCounter++,
                        Tipo = kvp.Key,
                        // Para los colores, el nombre y el código son el mismo. Para otros, se capitaliza.
                        Nombre = kvp.Key.ToString().Contains("Color") ? codigo : capitalizar(codigo),
                        CodigoUnico = codigo,
                        // TODO: ¡IMPORTANTE! reemplazar esto con rutas de recursos reales una vez obtenidos los recursos.
                        RutaRecurso = $"avatar/{kvp.Key.ToString().ToLower()}/{codigo}.svg"
                    });
                }
            }

            modelBuilder.Entity<AtributoAvatar>().HasData(atributos);
        }
    }
}

