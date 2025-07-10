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
                new Estudiante { Id = estudiante2Id, UserName = "valentina", NormalizedUserName = "VALENTINA", Email = null, NormalizedEmail = null, EmailConfirmed = false, PasswordHash = "AQAAAAIAAYagAAAAENuS3fE5d1k/aN2zV8mY9wR8cI7qU5kY4tL6wP9eO3bF0dG1sS5nC2vX3jJ4oP7eWw==", SecurityStamp = "STATIC_SECURITY_STAMP_4", ConcurrencyStamp = "d5c7f9b1-2e4g-5f0b-a0d9-6e3b5g7c9d1e" },
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
            // 11. CREACIÓN DE TIENDAS (una por cada grupo)
            var tienda1Id = 1;
            var tienda2Id = 2;
            modelBuilder.Entity<Tienda>().HasData(
                new { Id = tienda1Id, GrupoId = 1 },
                new { Id = tienda2Id, GrupoId = 2 }
            );
            modelBuilder.Entity<RecompensaSimple>().HasData(
                // Para Tienda 1
                new RecompensaSimple { Id = 1, Nombre = "Estrella Mágica", Precio = 50, RutaImagenCompleta = "star", RutaImagenMiniatura = "star", TiendaId = tienda1Id },
                new RecompensaSimple { Id = 2, Nombre = "Regalo Sorpresa", Precio = 30, RutaImagenCompleta = "gift", RutaImagenMiniatura = "gift", TiendaId = tienda1Id },
                new RecompensaSimple { Id = 3, Nombre = "Corazón Brillante", Precio = 20, RutaImagenCompleta = "heart", RutaImagenMiniatura = "heart", TiendaId = tienda1Id },
                new RecompensaSimple { Id = 4, Nombre = "Medalla de Oro", Precio = 80, RutaImagenCompleta = "medal", RutaImagenMiniatura = "medal", TiendaId = tienda1Id },
                new RecompensaSimple { Id = 5, Nombre = "Montón de Monedas", Precio = 100, RutaImagenCompleta = "coins", RutaImagenMiniatura = "coins", TiendaId = tienda1Id },
                // Para Tienda 2
                new RecompensaSimple { Id = 6, Nombre = "Trofeo Brillante", Precio = 70, RutaImagenCompleta = "trophy", RutaImagenMiniatura = "trophy", TiendaId = tienda2Id },
                new RecompensaSimple { Id = 7, Nombre = "Llama de Fuego", Precio = 40, RutaImagenCompleta = "fire", RutaImagenMiniatura = "fire", TiendaId = tienda2Id },
                new RecompensaSimple { Id = 8, Nombre = "Corona Real", Precio = 90, RutaImagenCompleta = "crown", RutaImagenMiniatura = "crown", TiendaId = tienda2Id },
                new RecompensaSimple { Id = 9, Nombre = "Cohete Espacial", Precio = 60, RutaImagenCompleta = "rocket", RutaImagenMiniatura = "rocket", TiendaId = tienda2Id },
                new RecompensaSimple { Id = 10, Nombre = "Robot Amistoso", Precio = 55, RutaImagenCompleta = "robot", RutaImagenMiniatura = "robot", TiendaId = tienda2Id }
            );



            // --- 12. CREACIÓN DE AVATARES INICIALES ---
            // Se crea un avatar para cada perfil de estudiante precargado.
            modelBuilder.Entity<Avatar>().HasData(
                new Avatar { Id = 1, PerfilEstudianteId = 1, ColorFondo = "b1e2ff", Voltear = false, Rotacion = 0, Zoom = 100 },
                new Avatar { Id = 2, PerfilEstudianteId = 2, ColorFondo = "a7ffc4", Voltear = false, Rotacion = 0, Zoom = 100 },
                new Avatar { Id = 3, PerfilEstudianteId = 3, ColorFondo = "ffafb9", Voltear = false, Rotacion = 0, Zoom = 100 },
                new Avatar { Id = 4, PerfilEstudianteId = 4, ColorFondo = "ffffb1", Voltear = false, Rotacion = 0, Zoom = 100 },
                new Avatar { Id = 5, PerfilEstudianteId = 5, ColorFondo = "e6e6e6", Voltear = false, Rotacion = 0, Zoom = 100 }
            );

            // ====================================================
            // --- INICIO DE LA PRECARGA DE ATRIBUTOS DE AVATAR ---
            // ====================================================
            var atributos = PrecargarAtributosAvatar(modelBuilder);
            var atributosPorDefecto = AsignarAvatarPorDefecto(modelBuilder, atributos);
            PrecargarInventarioInicial(modelBuilder, atributosPorDefecto);

            // ===========================================
            // --- PRECARGA DE PREGUNTAS DE SEGURIDAD ---
            // ===========================================
            PrecargarPreguntasDeSeguridad(modelBuilder);

            // ===========================================
            // --- PRECARGA DE RESPUESTAS DE SEGURIDAD ---
            // ===========================================
            PrecargarRespuestasDeSeguridad(modelBuilder);

        }
        private static void PrecargarRespuestasDeSeguridad(ModelBuilder modelBuilder)
        {
            // --- IDs de los estudiantes a los que asignaremos respuestas ---
            var estudiante1Id = "a1445865-a24d-4543-a6c6-9443d048cdb1"; // santiago
            var estudiante2Id = "b2445865-a24d-4543-a6c6-9443d048cdb2"; // valentina

            // --- Hashes Pre-generados para las respuestas ---
            // Respuestas para Santiago: "Cecilia1." y "Cecilia1."
            var hashPrimaria = "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==";
            var hashMascota = "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==";

            // Respuestas para Valentina: "Cecilia1." y "Cecilia1."
            var hashAbuela = "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==";
            var hashPersonaje = "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==";

            modelBuilder.Entity<PreguntaRespuestaSeguridad>().HasData(
                // --- Respuestas para Santiago ---
                new PreguntaRespuestaSeguridad
                {
                    Id = 1, // PK de esta tabla
                    PreguntaDeSeguridadId = 1, // FK a "¿Cuál era el nombre de tu escuela primaria?"
                    Respuesta = hashPrimaria,
                    EstudianteId = estudiante1Id
                },
                new PreguntaRespuestaSeguridad
                {
                    Id = 2,
                    PreguntaDeSeguridadId = 3, // FK a "¿Cuál era el nombre de tu primera mascota?"
                    Respuesta = hashMascota,
                    EstudianteId = estudiante1Id
                },

                // --- Respuestas para Valentina ---
                new PreguntaRespuestaSeguridad
                {
                    Id = 3,
                    PreguntaDeSeguridadId = 2, // FK a "¿Cuál es el primer nombre de tu abuela materna?"
                    Respuesta = hashAbuela,
                    EstudianteId = estudiante2Id
                },
                new PreguntaRespuestaSeguridad
                {
                    Id = 4,
                    PreguntaDeSeguridadId = 5, // FK a "¿Cuál es el nombre de tu personaje de ficción favorito...?"
                    Respuesta = hashPersonaje,
                    EstudianteId = estudiante2Id
                }
            );
        }

        private static void PrecargarPreguntasDeSeguridad(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PreguntaDeSeguridad>().HasData(
                new PreguntaDeSeguridad { Id = 1, Texto = "¿Cuál era el nombre de tu escuela primaria?" },
                new PreguntaDeSeguridad { Id = 2, Texto = "¿Cuál es el primer nombre de tu abuela materna?" },
                new PreguntaDeSeguridad { Id = 3, Texto = "¿Cuál era el nombre de tu primera mascota?" },
                new PreguntaDeSeguridad { Id = 4, Texto = "¿Cuál era el apodo que te decía tu familia en la infancia?" },
                new PreguntaDeSeguridad { Id = 5, Texto = "¿Cuál es el nombre de tu personaje de ficción favorito (de un libro, serie o videojuego)?" },
                new PreguntaDeSeguridad { Id = 6, Texto = "¿Cuál fue el primer videojuego que lograste completar?" },
                new PreguntaDeSeguridad { Id = 7, Texto = "Si pudieras tener un superpoder, ¿cuál sería?" },
                new PreguntaDeSeguridad { Id = 8, Texto = "¿Cuál es el apellido del primer amigo o amiga que hiciste al empezar el liceo?" },
                new PreguntaDeSeguridad { Id = 9, Texto = "¿Cuál es el nombre del hospital donde naciste?" }



            );
        }

        private static List<AtributoAvatar> PrecargarAtributosAvatar(ModelBuilder modelBuilder)
        {
            int idCounter = 1;
            var atributos = new List<AtributoAvatar>();
            Func<string, string> capitalizar = s => char.ToUpper(s[0]) + s.Substring(1);
            var mapeoPrefijos = new Dictionary<TipoAtributo, string>
            {
                { TipoAtributo.Pelo, "top-" },
                { TipoAtributo.Cejas, "eyebrows-" },
                { TipoAtributo.Ojos, "eyes-" },
                { TipoAtributo.Boca, "mouth-" },
                { TipoAtributo.Barba, "beard-" }, 
                { TipoAtributo.Gafas, "accessories-" },
                { TipoAtributo.Ropa, "clothing-" },
                { TipoAtributo.ColorPiel, "skinColor-" },
                { TipoAtributo.ColorPelo, "hairColor-" },
                { TipoAtributo.ColorBarba, "beardColor-" },
                { TipoAtributo.ColorRopa, "clothesColor-" },
                { TipoAtributo.ColorGafas, "accessoriesColor-" }
            };

            var datos = new Dictionary<TipoAtributo, string[]> { { TipoAtributo.Pelo, new[] { "bigHair", "bob", "bun", "curly", "curvy", "dreads", "dreads01", "dreads02", "frida", "frizzle", "fro", "froBand", "hat", "hijab", "longButNotTooLong", "miaWallace", "shaggy", "shaggyMullet", "shavedSides", "shortCurly", "shortFlat", "shortRound", "shortWaved", "sides", "straight01", "straight02", "straightAndStrand", "theCaesar", "theCaesarAndSidePart", "turban", "winterHat1", "winterHat02", "winterHat03", "winterHat04" } },
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

                var prefijo = mapeoPrefijos[kvp.Key];
                var tipoAtributo = kvp.Key;

                foreach (var codigo in kvp.Value)
                {
                    atributos.Add(new AtributoAvatar
                    {
                        Id = idCounter++,
                        Tipo = tipoAtributo,
                        Nombre = tipoAtributo.ToString().Contains("Color") ? codigo : capitalizar(codigo),
                        CodigoUnico = codigo,
                        RutaRecurso = $"{prefijo}{codigo}.png"
                    });
                }
            }

            modelBuilder.Entity<AtributoAvatar>().HasData(atributos);
            return atributos;
        }

        // --- MÉTODO PARA ASIGNAR ATRIBUTOS POR DEFECTO ---
        private static List<AtributoAvatar> AsignarAvatarPorDefecto(ModelBuilder modelBuilder, List<AtributoAvatar> atributos)
        {
            // Objeto auxiliar con los códigos únicos de los atributos por defecto
            var avatarPorDefecto = new
            {
                Pelo = "shortFlat",
                Ojos = "default",
                Cejas = "defaultNatural",
                Boca = "smile",
                Ropa = "shirtVNeck",
                Gafas = "sunglasses",
                Barba = "beardLight",
                ColorPiel = "edb98a",
                ColorPelo = "a55728",
                ColorRopa = "3c4f5c",
                ColorGafas = "262e33",
                ColorBarba = "a55728"
            };

            // 1. Encontrar y recolectar los objetos AtributoAvatar por defecto en una lista fuertemente tipada.
            var atributosAsignados = new List<AtributoAvatar>
            {
                atributos.First(a => a.CodigoUnico == avatarPorDefecto.Pelo),
                atributos.First(a => a.CodigoUnico == avatarPorDefecto.Ojos),
                atributos.First(a => a.CodigoUnico == avatarPorDefecto.Cejas),
                atributos.First(a => a.CodigoUnico == avatarPorDefecto.Boca),
                atributos.First(a => a.CodigoUnico == avatarPorDefecto.Ropa),
                atributos.First(a => a.CodigoUnico == avatarPorDefecto.Gafas),
                atributos.First(a => a.CodigoUnico == avatarPorDefecto.Barba),
                atributos.First(a => a.Tipo == TipoAtributo.ColorPiel && a.CodigoUnico == avatarPorDefecto.ColorPiel),
                atributos.First(a => a.Tipo == TipoAtributo.ColorPelo && a.CodigoUnico == avatarPorDefecto.ColorPelo),
                atributos.First(a => a.Tipo == TipoAtributo.ColorRopa && a.CodigoUnico == avatarPorDefecto.ColorRopa),
                atributos.First(a => a.Tipo == TipoAtributo.ColorGafas && a.CodigoUnico == avatarPorDefecto.ColorGafas),
                atributos.First(a => a.Tipo == TipoAtributo.ColorBarba && a.CodigoUnico == avatarPorDefecto.ColorBarba)
            };

            // 2. Usar la lista anterior para crear los datos de la tabla de unión (objetos anónimos).
            var datosParaTablaDeUnion = atributosAsignados.Select(attr => new
            {
                AvatarId = 1,
                AtributoSeleccionadoId = attr.Id 
            }).ToArray();

            // 3. Poblar la tabla de unión con los datos correctos.
            modelBuilder.Entity("AvatarAtributos").HasData(datosParaTablaDeUnion);

            // 4. Devolver la lista de entidades AtributoAvatar, como se requiere para el siguiente paso.
            return atributosAsignados;
        }

        private static void PrecargarInventarioInicial(ModelBuilder modelBuilder, List<AtributoAvatar> atributosPorDefecto)
        {

            int proximoIdRecompensa = 11;
            var recompensasAvatar = new List<PersonalizacionAvatar>();

            foreach (var atributo in atributosPorDefecto)
            {
                recompensasAvatar.Add(new PersonalizacionAvatar
                {
                    Id = proximoIdRecompensa++,
                    Nombre = $"Item: {atributo.Nombre}",
                    Precio = 0, // Precio 0 porque ya los posee
                    RutaImagenCompleta = atributo.RutaRecurso,
                    RutaImagenMiniatura = atributo.RutaRecurso,
                    TiendaId = 1,
                    AtributoAvatarId = atributo.Id
                });
            }

            modelBuilder.Entity<PersonalizacionAvatar>().HasData(recompensasAvatar);

            int nextSeedId = 1;              
            var inventarioInicial = recompensasAvatar
                .Select(r => new PerfilEstudianteRecompensa
                {
                    Id = nextSeedId++,  
                    PerfilEstudianteId = 1,
                    RecompensaId = r.Id
                })
                .ToArray();

            modelBuilder.Entity<PerfilEstudianteRecompensa>()
                        .HasData(inventarioInicial);
        }
    }
}

