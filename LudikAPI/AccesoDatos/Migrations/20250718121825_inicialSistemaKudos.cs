using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class inicialSistemaKudos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtributosAvatar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    RutaRecurso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoUnico = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtributosAvatar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnlacesUnion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoUnico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlCompleta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnlacesUnion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TiempoDeVida = table.Column<int>(type: "int", nullable: false),
                    FueUtilizado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreguntasDeSeguridad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasDeSeguridad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreRol = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NombreRolNormalizado = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EstampaConcurrencia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "TiposKudo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposKudo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImagenPerfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NombreUsuarioNormalizado = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CorreoNormalizado = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CorreoConfirmado = table.Column<bool>(type: "bit", nullable: false),
                    ContraseniaHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstampaSeguridad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstampaConcurrencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonoConfirmado = table.Column<bool>(type: "bit", nullable: false),
                    AutenticacionDosFactores = table.Column<bool>(type: "bit", nullable: false),
                    FinBloqueo = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    BloqueoHabilitado = table.Column<bool>(type: "bit", nullable: false),
                    IntentosFallidos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "ReclamacionesRoles",
                columns: table => new
                {
                    ReclamacionRolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoReclamacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorReclamacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReclamacionesRoles", x => x.ReclamacionRolId);
                    table.ForeignKey(
                        name: "FK_ReclamacionesRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IniciosSesionUsuario",
                columns: table => new
                {
                    Proveedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaveProveedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreProveedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IniciosSesionUsuario", x => new { x.Proveedor, x.ClaveProveedor });
                    table.ForeignKey(
                        name: "FK_IniciosSesionUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Profesores_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReclamacionesUsuario",
                columns: table => new
                {
                    ReclamacionUsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoReclamacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorReclamacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReclamacionesUsuario", x => x.ReclamacionUsuarioId);
                    table.ForeignKey(
                        name: "FK_ReclamacionesUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokensUsuario",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Proveedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValorToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokensUsuario", x => new { x.UsuarioId, x.Proveedor, x.NombreToken });
                    table.ForeignKey(
                        name: "FK_TokensUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RolId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => new { x.UsuarioId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreguntasRespuestasSeguridad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreguntaDeSeguridadId = table.Column<int>(type: "int", nullable: false),
                    Respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasRespuestasSeguridad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreguntasRespuestasSeguridad_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreguntasRespuestasSeguridad_PreguntasDeSeguridad_PreguntaDeSeguridadId",
                        column: x => x.PreguntaDeSeguridadId,
                        principalTable: "PreguntasDeSeguridad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medallas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UrlImagenMiniatura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonedasOtorgadas = table.Column<int>(type: "int", nullable: false),
                    TieneAsignacionMutua = table.Column<bool>(type: "bit", nullable: false),
                    ProfesorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medallas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medallas_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TablasEquivalencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfesorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablasEquivalencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablasEquivalencia_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equivalencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nota = table.Column<int>(type: "int", nullable: false),
                    TablaEquivalenciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equivalencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equivalencias_TablasEquivalencia_TablaEquivalenciaId",
                        column: x => x.TablaEquivalenciaId,
                        principalTable: "TablasEquivalencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Institucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Materia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TablaEquivalenciaId = table.Column<int>(type: "int", nullable: false),
                    EnlaceUnionId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaUltimoReinicio = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupos_EnlacesUnion_EnlaceUnionId",
                        column: x => x.EnlaceUnionId,
                        principalTable: "EnlacesUnion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grupos_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grupos_TablasEquivalencia_TablaEquivalenciaId",
                        column: x => x.TablaEquivalenciaId,
                        principalTable: "TablasEquivalencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquivalenciaMedallas",
                columns: table => new
                {
                    EquivalenciaId = table.Column<int>(type: "int", nullable: false),
                    MedallaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciaMedallas", x => new { x.EquivalenciaId, x.MedallaId });
                    table.ForeignKey(
                        name: "FK_EquivalenciaMedallas_Equivalencias_EquivalenciaId",
                        column: x => x.EquivalenciaId,
                        principalTable: "Equivalencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquivalenciaMedallas_Medallas_MedallaId",
                        column: x => x.MedallaId,
                        principalTable: "Medallas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesUnion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesUnion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesUnion_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudesUnion_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TablasClasificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MedallaAsociadaId = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablasClasificacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablasClasificacion_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TablasClasificacion_Medallas_MedallaAsociadaId",
                        column: x => x.MedallaAsociadaId,
                        principalTable: "Medallas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tiendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tiendas_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UmbralesParaMedallasPorKudos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadKudos = table.Column<int>(type: "int", nullable: false),
                    MedallaId = table.Column<int>(type: "int", nullable: false),
                    TipoKudoId = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UmbralesParaMedallasPorKudos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UmbralesParaMedallasPorKudos_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UmbralesParaMedallasPorKudos_Medallas_MedallaId",
                        column: x => x.MedallaId,
                        principalTable: "Medallas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UmbralesParaMedallasPorKudos_TiposKudo_TipoKudoId",
                        column: x => x.TipoKudoId,
                        principalTable: "TiposKudo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recompensas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RutaImagenCompleta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RutaImagenMiniatura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiereImagen = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    TiendaId = table.Column<int>(type: "int", nullable: false),
                    RecompensaTipo = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    AtributoAvatarId = table.Column<int>(type: "int", nullable: true),
                    FechaActivacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duracion = table.Column<TimeSpan>(type: "time", nullable: true),
                    Multiplicador = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recompensas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recompensas_AtributosAvatar_AtributoAvatarId",
                        column: x => x.AtributoAvatarId,
                        principalTable: "AtributosAvatar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recompensas_Tiendas_TiendaId",
                        column: x => x.TiendaId,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hitos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantMedallasRequeridas = table.Column<int>(type: "int", nullable: false),
                    RecompensaId = table.Column<int>(type: "int", nullable: false),
                    Otorgado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hitos_Recompensas_RecompensaId",
                        column: x => x.RecompensaId,
                        principalTable: "Recompensas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilesEstudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetaCalificacion = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Monedas = table.Column<int>(type: "int", nullable: false),
                    RutaImagenCompleta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RutaImagenMiniatura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadKudosDisponibles = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    PotenciadorActivoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilesEstudiantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilesEstudiantes_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilesEstudiantes_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerfilesEstudiantes_Recompensas_PotenciadorActivoId",
                        column: x => x.PotenciadorActivoId,
                        principalTable: "Recompensas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstudianteHitos",
                columns: table => new
                {
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HitoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudianteHitos", x => new { x.EstudianteId, x.HitoId });
                    table.ForeignKey(
                        name: "FK_EstudianteHitos_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstudianteHitos_Hitos_HitoId",
                        column: x => x.HitoId,
                        principalTable: "Hitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Avatares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorFondo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voltear = table.Column<bool>(type: "bit", nullable: false),
                    Rotacion = table.Column<int>(type: "int", nullable: false),
                    Zoom = table.Column<int>(type: "int", nullable: false),
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avatares_PerfilesEstudiantes_PerfilEstudianteId",
                        column: x => x.PerfilEstudianteId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarrasProgreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorMin = table.Column<int>(type: "int", nullable: false),
                    ValorMax = table.Column<int>(type: "int", nullable: false),
                    TablaEquivalenciaId = table.Column<int>(type: "int", nullable: false),
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarrasProgreso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarrasProgreso_PerfilesEstudiantes_PerfilEstudianteId",
                        column: x => x.PerfilEstudianteId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarrasProgreso_TablasEquivalencia_TablaEquivalenciaId",
                        column: x => x.TablaEquivalenciaId,
                        principalTable: "TablasEquivalencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KudosOtorgados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilEstudianteEmisorId = table.Column<int>(type: "int", nullable: false),
                    PerfilEstudianteReceptorId = table.Column<int>(type: "int", nullable: false),
                    TipoKudoId = table.Column<int>(type: "int", nullable: false),
                    FechaOtorgamiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KudosOtorgados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KudosOtorgados_PerfilesEstudiantes_PerfilEstudianteEmisorId",
                        column: x => x.PerfilEstudianteEmisorId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KudosOtorgados_PerfilesEstudiantes_PerfilEstudianteReceptorId",
                        column: x => x.PerfilEstudianteReceptorId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KudosOtorgados_TiposKudo_TipoKudoId",
                        column: x => x.TipoKudoId,
                        principalTable: "TiposKudo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilEstudianteMedallas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: false),
                    MedallaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilEstudianteMedallas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilEstudianteMedallas_Medallas_MedallaId",
                        column: x => x.MedallaId,
                        principalTable: "Medallas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerfilEstudianteMedallas_PerfilesEstudiantes_PerfilEstudianteId",
                        column: x => x.PerfilEstudianteId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilEstudianteRecompensas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: false),
                    RecompensaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilEstudianteRecompensas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilEstudianteRecompensas_PerfilesEstudiantes_PerfilEstudianteId",
                        column: x => x.PerfilEstudianteId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilEstudianteRecompensas_Recompensas_RecompensaId",
                        column: x => x.RecompensaId,
                        principalTable: "Recompensas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RendimientosPeriodos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NotaObtenida = table.Column<int>(type: "int", nullable: false),
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendimientosPeriodos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RendimientosPeriodos_PerfilesEstudiantes_PerfilEstudianteId",
                        column: x => x.PerfilEstudianteId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TablaClasificacionParticipantes",
                columns: table => new
                {
                    TablaClasificacionId = table.Column<int>(type: "int", nullable: false),
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaClasificacionParticipantes", x => new { x.TablaClasificacionId, x.PerfilEstudianteId });
                    table.ForeignKey(
                        name: "FK_TablaClasificacionParticipantes_PerfilesEstudiantes_PerfilEstudianteId",
                        column: x => x.PerfilEstudianteId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TablaClasificacionParticipantes_TablasClasificacion_TablaClasificacionId",
                        column: x => x.TablaClasificacionId,
                        principalTable: "TablasClasificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvatarAtributos",
                columns: table => new
                {
                    AvatarId = table.Column<int>(type: "int", nullable: false),
                    AtributoSeleccionadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvatarAtributos", x => new { x.AvatarId, x.AtributoSeleccionadoId });
                    table.ForeignKey(
                        name: "FK_AvatarAtributos_AtributosAvatar_AtributoSeleccionadoId",
                        column: x => x.AtributoSeleccionadoId,
                        principalTable: "AtributosAvatar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvatarAtributos_Avatares_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RendimientoPeriodoMedallas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RendimientoPeriodoId = table.Column<int>(type: "int", nullable: false),
                    MedallaId = table.Column<int>(type: "int", nullable: false),
                    FechaOtorgada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendimientoPeriodoMedallas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RendimientoPeriodoMedallas_Medallas_MedallaId",
                        column: x => x.MedallaId,
                        principalTable: "Medallas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RendimientoPeriodoMedallas_RendimientosPeriodos_RendimientoPeriodoId",
                        column: x => x.RendimientoPeriodoId,
                        principalTable: "RendimientosPeriodos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AtributosAvatar",
                columns: new[] { "Id", "CodigoUnico", "Nombre", "RutaRecurso", "Tipo" },
                values: new object[,]
                {
                    { 1, "bigHair", "BigHair", "top-bigHair.png", 0 },
                    { 2, "bob", "Bob", "top-bob.png", 0 },
                    { 3, "bun", "Bun", "top-bun.png", 0 },
                    { 4, "curly", "Curly", "top-curly.png", 0 },
                    { 5, "curvy", "Curvy", "top-curvy.png", 0 },
                    { 6, "dreads", "Dreads", "top-dreads.png", 0 },
                    { 7, "dreads01", "Dreads01", "top-dreads01.png", 0 },
                    { 8, "dreads02", "Dreads02", "top-dreads02.png", 0 },
                    { 9, "frida", "Frida", "top-frida.png", 0 },
                    { 10, "frizzle", "Frizzle", "top-frizzle.png", 0 },
                    { 11, "fro", "Fro", "top-fro.png", 0 },
                    { 12, "froBand", "FroBand", "top-froBand.png", 0 },
                    { 13, "hat", "Hat", "top-hat.png", 0 },
                    { 14, "hijab", "Hijab", "top-hijab.png", 0 },
                    { 15, "longButNotTooLong", "LongButNotTooLong", "top-longButNotTooLong.png", 0 },
                    { 16, "miaWallace", "MiaWallace", "top-miaWallace.png", 0 },
                    { 17, "shaggy", "Shaggy", "top-shaggy.png", 0 },
                    { 18, "shaggyMullet", "ShaggyMullet", "top-shaggyMullet.png", 0 },
                    { 19, "shavedSides", "ShavedSides", "top-shavedSides.png", 0 },
                    { 20, "shortCurly", "ShortCurly", "top-shortCurly.png", 0 },
                    { 21, "shortFlat", "ShortFlat", "top-shortFlat.png", 0 },
                    { 22, "shortRound", "ShortRound", "top-shortRound.png", 0 },
                    { 23, "shortWaved", "ShortWaved", "top-shortWaved.png", 0 },
                    { 24, "sides", "Sides", "top-sides.png", 0 },
                    { 25, "straight01", "Straight01", "top-straight01.png", 0 },
                    { 26, "straight02", "Straight02", "top-straight02.png", 0 },
                    { 27, "straightAndStrand", "StraightAndStrand", "top-straightAndStrand.png", 0 },
                    { 28, "theCaesar", "TheCaesar", "top-theCaesar.png", 0 },
                    { 29, "theCaesarAndSidePart", "TheCaesarAndSidePart", "top-theCaesarAndSidePart.png", 0 },
                    { 30, "turban", "Turban", "top-turban.png", 0 },
                    { 31, "winterHat1", "WinterHat1", "top-winterHat1.png", 0 },
                    { 32, "winterHat02", "WinterHat02", "top-winterHat02.png", 0 },
                    { 33, "winterHat03", "WinterHat03", "top-winterHat03.png", 0 },
                    { 34, "winterHat04", "WinterHat04", "top-winterHat04.png", 0 },
                    { 35, "angry", "Angry", "eyebrows-angry.png", 1 },
                    { 36, "angryNatural", "AngryNatural", "eyebrows-angryNatural.png", 1 },
                    { 37, "default", "Default", "eyebrows-default.png", 1 },
                    { 38, "defaultNatural", "DefaultNatural", "eyebrows-defaultNatural.png", 1 },
                    { 39, "flatNatural", "FlatNatural", "eyebrows-flatNatural.png", 1 },
                    { 40, "frownNatural", "FrownNatural", "eyebrows-frownNatural.png", 1 },
                    { 41, "raisedExcited", "RaisedExcited", "eyebrows-raisedExcited.png", 1 },
                    { 42, "raisedExcitedNatural", "RaisedExcitedNatural", "eyebrows-raisedExcitedNatural.png", 1 },
                    { 43, "sadConcerned", "SadConcerned", "eyebrows-sadConcerned.png", 1 },
                    { 44, "sadConcernedNatural", "SadConcernedNatural", "eyebrows-sadConcernedNatural.png", 1 },
                    { 45, "unibrowNatural", "UnibrowNatural", "eyebrows-unibrowNatural.png", 1 },
                    { 46, "upDown", "UpDown", "eyebrows-upDown.png", 1 },
                    { 47, "upDownNatural", "UpDownNatural", "eyebrows-upDownNatural.png", 1 },
                    { 48, "closed", "Closed", "eyes-closed.png", 2 },
                    { 49, "cry", "Cry", "eyes-cry.png", 2 },
                    { 50, "default", "Default", "eyes-default.png", 2 },
                    { 51, "eyeRoll", "EyeRoll", "eyes-eyeRoll.png", 2 },
                    { 52, "happy", "Happy", "eyes-happy.png", 2 },
                    { 53, "hearts", "Hearts", "eyes-hearts.png", 2 },
                    { 54, "side", "Side", "eyes-side.png", 2 },
                    { 55, "squint", "Squint", "eyes-squint.png", 2 },
                    { 56, "surprised", "Surprised", "eyes-surprised.png", 2 },
                    { 57, "wink", "Wink", "eyes-wink.png", 2 },
                    { 58, "winkWacky", "WinkWacky", "eyes-winkWacky.png", 2 },
                    { 59, "xDizzy", "XDizzy", "eyes-xDizzy.png", 2 },
                    { 60, "concerned", "Concerned", "mouth-concerned.png", 3 },
                    { 61, "default", "Default", "mouth-default.png", 3 },
                    { 62, "disbelief", "Disbelief", "mouth-disbelief.png", 3 },
                    { 63, "eating", "Eating", "mouth-eating.png", 3 },
                    { 64, "grimace", "Grimace", "mouth-grimace.png", 3 },
                    { 65, "sad", "Sad", "mouth-sad.png", 3 },
                    { 66, "screamOpen", "ScreamOpen", "mouth-screamOpen.png", 3 },
                    { 67, "serious", "Serious", "mouth-serious.png", 3 },
                    { 68, "smile", "Smile", "mouth-smile.png", 3 },
                    { 69, "tongue", "Tongue", "mouth-tongue.png", 3 },
                    { 70, "twinkle", "Twinkle", "mouth-twinkle.png", 3 },
                    { 71, "beardLight", "BeardLight", "beard-beardLight.png", 4 },
                    { 72, "beardMajestic", "BeardMajestic", "beard-beardMajestic.png", 4 },
                    { 73, "beardMedium", "BeardMedium", "beard-beardMedium.png", 4 },
                    { 74, "moustacheFancy", "MoustacheFancy", "beard-moustacheFancy.png", 4 },
                    { 75, "moustacheMagnum", "MoustacheMagnum", "beard-moustacheMagnum.png", 4 },
                    { 76, "eyepatch", "Eyepatch", "accessories-eyepatch.png", 5 },
                    { 77, "kurt", "Kurt", "accessories-kurt.png", 5 },
                    { 78, "prescription01", "Prescription01", "accessories-prescription01.png", 5 },
                    { 79, "prescription02", "Prescription02", "accessories-prescription02.png", 5 },
                    { 80, "round", "Round", "accessories-round.png", 5 },
                    { 81, "sunglasses", "Sunglasses", "accessories-sunglasses.png", 5 },
                    { 82, "wayfarers", "Wayfarers", "accessories-wayfarers.png", 5 },
                    { 83, "blazerAndShirt", "BlazerAndShirt", "clothing-blazerAndShirt.png", 6 },
                    { 84, "blazerAndSweater", "BlazerAndSweater", "clothing-blazerAndSweater.png", 6 },
                    { 85, "collarAndSweater", "CollarAndSweater", "clothing-collarAndSweater.png", 6 },
                    { 86, "graphicShirt", "GraphicShirt", "clothing-graphicShirt.png", 6 },
                    { 87, "hoodie", "Hoodie", "clothing-hoodie.png", 6 },
                    { 88, "overall", "Overall", "clothing-overall.png", 6 },
                    { 89, "shirtCrewNeck", "ShirtCrewNeck", "clothing-shirtCrewNeck.png", 6 },
                    { 90, "shirtScoopNeck", "ShirtScoopNeck", "clothing-shirtScoopNeck.png", 6 },
                    { 91, "shirtVNeck", "ShirtVNeck", "clothing-shirtVNeck.png", 6 },
                    { 92, "614335", "614335", "skinColor-614335.png", 7 },
                    { 93, "ae5d29", "ae5d29", "skinColor-ae5d29.png", 7 },
                    { 94, "d08b5b", "d08b5b", "skinColor-d08b5b.png", 7 },
                    { 95, "edb98a", "edb98a", "skinColor-edb98a.png", 7 },
                    { 96, "f8d25c", "f8d25c", "skinColor-f8d25c.png", 7 },
                    { 97, "fd9841", "fd9841", "skinColor-fd9841.png", 7 },
                    { 98, "ffdbb4", "ffdbb4", "skinColor-ffdbb4.png", 7 },
                    { 99, "2c1b18", "2c1b18", "hairColor-2c1b18.png", 8 },
                    { 100, "4a312c", "4a312c", "hairColor-4a312c.png", 8 },
                    { 101, "724133", "724133", "hairColor-724133.png", 8 },
                    { 102, "a55728", "a55728", "hairColor-a55728.png", 8 },
                    { 103, "b58143", "b58143", "hairColor-b58143.png", 8 },
                    { 104, "c93305", "c93305", "hairColor-c93305.png", 8 },
                    { 105, "d6b370", "d6b370", "hairColor-d6b370.png", 8 },
                    { 106, "e8e1e1", "e8e1e1", "hairColor-e8e1e1.png", 8 },
                    { 107, "ecdcbf", "ecdcbf", "hairColor-ecdcbf.png", 8 },
                    { 108, "f59797", "f59797", "hairColor-f59797.png", 8 },
                    { 109, "2c1b18", "2c1b18", "beardColor-2c1b18.png", 9 },
                    { 110, "4a312c", "4a312c", "beardColor-4a312c.png", 9 },
                    { 111, "724133", "724133", "beardColor-724133.png", 9 },
                    { 112, "a55728", "a55728", "beardColor-a55728.png", 9 },
                    { 113, "b58143", "b58143", "beardColor-b58143.png", 9 },
                    { 114, "c93305", "c93305", "beardColor-c93305.png", 9 },
                    { 115, "d6b370", "d6b370", "beardColor-d6b370.png", 9 },
                    { 116, "e8e1e1", "e8e1e1", "beardColor-e8e1e1.png", 9 },
                    { 117, "ecdcbf", "ecdcbf", "beardColor-ecdcbf.png", 9 },
                    { 118, "f59797", "f59797", "beardColor-f59797.png", 9 },
                    { 119, "3c4f5c", "3c4f5c", "clothesColor-3c4f5c.png", 10 },
                    { 120, "65c9ff", "65c9ff", "clothesColor-65c9ff.png", 10 },
                    { 121, "262e33", "262e33", "clothesColor-262e33.png", 10 },
                    { 122, "5199e4", "5199e4", "clothesColor-5199e4.png", 10 },
                    { 123, "25557c", "25557c", "clothesColor-25557c.png", 10 },
                    { 124, "929598", "929598", "clothesColor-929598.png", 10 },
                    { 125, "a7ffc4", "a7ffc4", "clothesColor-a7ffc4.png", 10 },
                    { 126, "b1e2ff", "b1e2ff", "clothesColor-b1e2ff.png", 10 },
                    { 127, "e6e6e6", "e6e6e6", "clothesColor-e6e6e6.png", 10 },
                    { 128, "ff5c5c", "ff5c5c", "clothesColor-ff5c5c.png", 10 },
                    { 129, "ff488e", "ff488e", "clothesColor-ff488e.png", 10 },
                    { 130, "ffafb9", "ffafb9", "clothesColor-ffafb9.png", 10 },
                    { 131, "ffffb1", "ffffb1", "clothesColor-ffffb1.png", 10 },
                    { 132, "ffffff", "ffffff", "clothesColor-ffffff.png", 10 },
                    { 133, "3c4f5c", "3c4f5c", "accessoriesColor-3c4f5c.png", 11 },
                    { 134, "65c9ff", "65c9ff", "accessoriesColor-65c9ff.png", 11 },
                    { 135, "262e33", "262e33", "accessoriesColor-262e33.png", 11 },
                    { 136, "5199e4", "5199e4", "accessoriesColor-5199e4.png", 11 },
                    { 137, "25557c", "25557c", "accessoriesColor-25557c.png", 11 },
                    { 138, "929598", "929598", "accessoriesColor-929598.png", 11 },
                    { 139, "a7ffc4", "a7ffc4", "accessoriesColor-a7ffc4.png", 11 },
                    { 140, "b1e2ff", "b1e2ff", "accessoriesColor-b1e2ff.png", 11 },
                    { 141, "e6e6e6", "e6e6e6", "accessoriesColor-e6e6e6.png", 11 },
                    { 142, "ff5c5c", "ff5c5c", "accessoriesColor-ff5c5c.png", 11 },
                    { 143, "ff488e", "ff488e", "accessoriesColor-ff488e.png", 11 },
                    { 144, "ffafb9", "ffafb9", "accessoriesColor-ffafb9.png", 11 },
                    { 145, "ffdeb5", "ffdeb5", "accessoriesColor-ffdeb5.png", 11 },
                    { 146, "ffffb1", "ffffb1", "accessoriesColor-ffffb1.png", 11 },
                    { 147, "ffffff", "ffffff", "accessoriesColor-ffffff.png", 11 }
                });

            migrationBuilder.InsertData(
                table: "EnlacesUnion",
                columns: new[] { "Id", "CodigoUnico", "Expiracion", "UrlCompleta" },
                values: new object[,]
                {
                    { 1, "MAT1A25", new DateTime(2026, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), "https://www.ludik.app/unirse/MAT1A25" },
                    { 2, "HISTU25", new DateTime(2026, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), "https://www.ludik.app/unirse/HISTU25" }
                });

            migrationBuilder.InsertData(
                table: "PreguntasDeSeguridad",
                columns: new[] { "Id", "Texto" },
                values: new object[,]
                {
                    { 1, "¿Cuál era el nombre de tu escuela primaria?" },
                    { 2, "¿Cuál es el primer nombre de tu abuela materna?" },
                    { 3, "¿Cuál era el nombre de tu primera mascota?" },
                    { 4, "¿Cuál era el apodo que te decía tu familia en la infancia?" },
                    { 5, "¿Cuál es el nombre de tu personaje de ficción favorito (de un libro, serie o videojuego)?" },
                    { 6, "¿Cuál fue el primer videojuego que lograste completar?" },
                    { 7, "Si pudieras tener un superpoder, ¿cuál sería?" },
                    { 8, "¿Cuál es el apellido del primer amigo o amiga que hiciste al empezar el liceo?" },
                    { 9, "¿Cuál es el nombre del hospital donde naciste?" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RolId", "EstampaConcurrencia", "NombreRol", "NombreRolNormalizado" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", null, "Profesor", "PROFESOR" },
                    { "3d5e174e-3b0e-446f-86af-483d56fd7211", null, "Estudiante", "ESTUDIANTE" }
                });

            migrationBuilder.InsertData(
                table: "TiposKudo",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Considera dar este kudo cuando un compañero te dedica tiempo para explicarte algo que no entendías o te ayuda a completar una tarea.", "Gracias por la Ayuda" },
                    { 2, "Considera dar este kudo cuando la pregunta de un compañero aclara una duda para todo el grupo o genera un debate que enriquece la clase.", "Esa Pregunta Suma" },
                    { 3, "Considera dar este kudo cuando el esfuerzo, la perseverancia o la actitud positiva de un compañero te motiven a superarte.", "Inspirador" },
                    { 4, "Considera dar este kudo cuando un compañero toma tu idea o la de alguien más y la mejora, aportando un punto de vista que hace el trabajo más fuerte.", "Conectando Ideas" },
                    { 5, "Considera dar este kudo cuando un compañero organiza el trabajo en equipo, se asegura de que todos participen o guía al grupo para cumplir el objetivo.", "Líder de Equipo" },
                    { 6, "Considera dar este kudo cuando un compañero comparte un enlace, video, apunte o cualquier material que te resultó muy útil para estudiar o hacer una tarea.", "Bibliotecario" },
                    { 7, "Considera dar este kudo cuando notes que un compañero se esfuerza por integrar a otros, asegurándose de que nadie se quede atrás y todos se sientan parte del equipo.", "Codo a Codo" },
                    { 8, "Considera dar este kudo cuando un compañero te da una sugerencia para mejorar tu trabajo de forma respetuosa y con la intención real de ayudar.", "Crítica que Construye" },
                    { 9, "Considera dar este kudo cuando un compañero propone una solución original a un problema o una idea innovadora para un proyecto que sorprende al grupo.", "Chispa Creativa" },
                    { 10, "Considera dar este kudo cuando la explicación de un compañero sobre un tema muy difícil hace que, finalmente, lo entiendas con total claridad.", "Einstein" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "IntentosFallidos", "EstampaConcurrencia", "Correo", "CorreoConfirmado", "ImagenPerfil", "BloqueoHabilitado", "FinBloqueo", "CorreoNormalizado", "NombreUsuarioNormalizado", "ContraseniaHash", "Telefono", "TelefonoConfirmado", "EstampaSeguridad", "AutenticacionDosFactores", "NombreUsuario", "Apellido", "Nombre" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "b0c8b6a8-8e6b-4e6a-9e1e-2e0b166a9c76", "cecilia@gmail.com", true, null, false, null, "CECILIA@GMAIL.COM", "CECILIA", "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==", null, false, "STATIC_SECURITY_STAMP_1", false, "cecilia", "Rodríguez", "Carlos" },
                    { "9e445865-a24d-4543-a6c6-9443d048cdb0", 0, "a1d3b5e7-9f2d-4b8c-8a1e-3f0e2d5b4a6b", "laura.fernandez@ludik.edu.uy", true, null, false, null, "LAURA.FERNANDEZ@LUDIK.EDU.UY", "LAURA.FERNANDEZ", "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==", null, false, "STATIC_SECURITY_STAMP_2", false, "laura", "Fernández", "Laura" },
                    { "a1445865-a24d-4543-a6c6-9443d048cdb1", 0, "c4b6e8a0-1d3f-4e9a-9c8e-5d2a4f6b8c0d", null, false, null, false, null, null, "SANTIAGO", "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==", null, false, "STATIC_SECURITY_STAMP_3", false, "santiago", "Pérez", "Santiago" },
                    { "b2445865-a24d-4543-a6c6-9443d048cdb2", 0, "d5c7f9b1-2e4g-5f0b-a0d9-6e3b5g7c9d1e", null, false, null, false, null, null, "VALENTINA", "AQAAAAIAAYagAAAAENuS3fE5d1k/aN2zV8mY9wR8cI7qU5kY4tL6wP9eO3bF0dG1sS5nC2vX3jJ4oP7eWw==", null, false, "STATIC_SECURITY_STAMP_4", false, "valentina", "Gómez", "Valentina" },
                    { "c3445865-a24d-4543-a6c6-9443d048cdb3", 0, "e6d80ac2-3f5h-6g1c-b1e0-7f4c6h8d0e2f", null, false, null, false, null, null, "MATIAS", "AQAAAAIAAYagAAAAENuS3fE5d1k/aN2zV8mY9wR8cI7qU5kY4tL6wP9eO3bF0dG1sS5nC2vX3jJ4oP7eWw==", null, false, "STATIC_SECURITY_STAMP_5", false, "matias", "González", "Matías" },
                    { "d4445865-a24d-4543-a6c6-9443d048cdb4", 0, "f7e91bd3-4g6i-7h2d-c2f1-8g5d7i9e1f3g", null, false, null, false, null, null, "CAMILA", "AQAAAAIAAYagAAAAENuS3fE5d1k/aN2zV8mY9wR8cI7qU5kY4tL6wP9eO3bF0dG1sS5nC2vX3jJ4oP7eWw==", null, false, "STATIC_SECURITY_STAMP_6", false, "camila", "Martínez", "Camila" },
                    { "e5445865-a24d-4543-a6c6-9443d048cdb5", 0, "g8f02ce4-5h7j-8i3e-d3g2-9h6e8j0f2g4h", null, false, null, false, null, null, "LUCAS", "AQAAAAIAAYagAAAAENuS3fE5d1k/aN2zV8mY9wR8cI7qU5kY4tL6wP9eO3bF0dG1sS5nC2vX3jJ4oP7eWw==", null, false, "STATIC_SECURITY_STAMP_7", false, "lucas", "Silva", "Lucas" }
                });

            migrationBuilder.InsertData(
                table: "Estudiantes",
                column: "UsuarioId",
                values: new object[]
                {
                    "a1445865-a24d-4543-a6c6-9443d048cdb1",
                    "b2445865-a24d-4543-a6c6-9443d048cdb2",
                    "c3445865-a24d-4543-a6c6-9443d048cdb3",
                    "d4445865-a24d-4543-a6c6-9443d048cdb4",
                    "e5445865-a24d-4543-a6c6-9443d048cdb5"
                });

            migrationBuilder.InsertData(
                table: "Profesores",
                column: "UsuarioId",
                values: new object[]
                {
                    "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    "9e445865-a24d-4543-a6c6-9443d048cdb0"
                });

            migrationBuilder.InsertData(
                table: "UsuariosRoles",
                columns: new[] { "RolId", "UsuarioId" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", "9e445865-a24d-4543-a6c6-9443d048cdb0" },
                    { "3d5e174e-3b0e-446f-86af-483d56fd7211", "a1445865-a24d-4543-a6c6-9443d048cdb1" },
                    { "3d5e174e-3b0e-446f-86af-483d56fd7211", "b2445865-a24d-4543-a6c6-9443d048cdb2" },
                    { "3d5e174e-3b0e-446f-86af-483d56fd7211", "c3445865-a24d-4543-a6c6-9443d048cdb3" },
                    { "3d5e174e-3b0e-446f-86af-483d56fd7211", "d4445865-a24d-4543-a6c6-9443d048cdb4" },
                    { "3d5e174e-3b0e-446f-86af-483d56fd7211", "e5445865-a24d-4543-a6c6-9443d048cdb5" }
                });

            migrationBuilder.InsertData(
                table: "Medallas",
                columns: new[] { "Id", "Descripcion", "MonedasOtorgadas", "Nombre", "ProfesorId", "TieneAsignacionMutua", "UrlImagenMiniatura" },
                values: new object[,]
                {
                    { 1, "Asistencia y participación en todas las clases del mes.", 30, "Participación Perfecta", "8e445865-a24d-4543-a6c6-9443d048cdb9", false, "medalla_participacion_perfecta.png" },
                    { 2, "Ayuda destacada a compañeros en proyectos grupales.", 25, "Maestro de la Colaboración", "9e445865-a24d-4543-a6c6-9443d048cdb0", false, "medalla_maestro_colaboracion.png" },
                    { 3, "Realización de preguntas perspicaces que enriquecen la clase.", 15, "Mente Curiosa", "9e445865-a24d-4543-a6c6-9443d048cdb0", false, "medalla_mente_curiosa.png" },
                    { 4, "Se otorga por ser un pilar de apoyo para tus compañeros. Demuestra que estás siempre dispuesto a ofrecer tu ayuda cuando alguien la necesita.", 20, "Compañerismo", "8e445865-a24d-4543-a6c6-9443d048cdb9", false, "medalla_companerismo.png" },
                    { 5, "Premia a las mentes que nunca dejan de preguntar. Se consigue al realizar preguntas que desafían al grupo y enriquecen el aprendizaje de todos.", 15, "Curiosidad Insaciable", "9e445865-a24d-4543-a6c6-9443d048cdb0", false, "medalla_curiosidad_insaciable.png" },
                    { 6, "Reconoce a quienes inspiran con su ejemplo. Se obtiene al demostrar una actitud y un esfuerzo que motivan a todo el grupo a superarse.", 25, "Faro del Grupo", "8e445865-a24d-4543-a6c6-9443d048cdb9", false, "medalla_faro_del_grupo.png" },
                    { 7, "Para aquellos que no solo tienen buenas ideas, sino que construyen sobre las de los demás para crear algo aún mejor.", 20, "Arquitecto de Ideas", "9e445865-a24d-4543-a6c6-9443d048cdb0", false, "medalla_arquitecto_ideas.png" },
                    { 8, "Se otorga por demostrar liderazgo natural, guiando y organizando al equipo para alcanzar metas comunes de forma efectiva.", 25, "Capitán de Equipo", "8e445865-a24d-4543-a6c6-9443d048cdb9", false, "medalla_capitan_equipo.png" },
                    { 9, "Premia la iniciativa de buscar y compartir recursos valiosos (videos, artículos, herramientas) que benefician a toda la clase.", 15, "Cazador de Tesoros", "9e445865-a24d-4543-a6c6-9443d048cdb0", false, "medalla_cazador_tesoros.png" },
                    { 10, "Se consigue al fomentar activamente un ambiente de respeto e inclusión, asegurando que cada miembro del grupo se sienta valorado.", 20, "Espíritu de Equipo", "8e445865-a24d-4543-a6c6-9443d048cdb9", false, "medalla_espiritu_equipo.png" },
                    { 11, "Reconoce la habilidad de dar críticas constructivas que ayudan a los compañeros a mejorar su trabajo de forma positiva y amable.", 15, "Pulidor de Diamantes", "9e445865-a24d-4543-a6c6-9443d048cdb0", false, "medalla_pulidor_diamantes.png" },
                    { 12, "Se otorga por aportar ideas creativas y soluciones originales que sacan al grupo de la rutina y abren nuevas posibilidades.", 20, "Mente Innovadora", "8e445865-a24d-4543-a6c6-9443d048cdb9", false, "medalla_mente_innovadora.png" },
                    { 13, "Premia la increíble habilidad de tomar un tema complejo y explicarlo de una manera tan clara y sencilla que todos puedan entenderlo.", 25, "El Explicador", "9e445865-a24d-4543-a6c6-9443d048cdb0", false, "medalla_el_explicador.png" }
                });

            migrationBuilder.InsertData(
                table: "PreguntasRespuestasSeguridad",
                columns: new[] { "Id", "EstudianteId", "PreguntaDeSeguridadId", "Respuesta" },
                values: new object[,]
                {
                    { 1, "a1445865-a24d-4543-a6c6-9443d048cdb1", 1, "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==" },
                    { 2, "a1445865-a24d-4543-a6c6-9443d048cdb1", 3, "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==" },
                    { 3, "b2445865-a24d-4543-a6c6-9443d048cdb2", 2, "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==" },
                    { 4, "b2445865-a24d-4543-a6c6-9443d048cdb2", 5, "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==" }
                });

            migrationBuilder.InsertData(
                table: "TablasEquivalencia",
                columns: new[] { "Id", "Nombre", "ProfesorId" },
                values: new object[,]
                {
                    { 1, "Calificaciones Estándar (C. Rodríguez)", "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { 2, "Evaluación Continua (L. Fernández)", "9e445865-a24d-4543-a6c6-9443d048cdb0" }
                });

            migrationBuilder.InsertData(
                table: "Equivalencias",
                columns: new[] { "Id", "Nota", "TablaEquivalenciaId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Grupos",
                columns: new[] { "Id", "EnlaceUnionId", "FCreacion", "FechaUltimoReinicio", "Institucion", "Materia", "Nombre", "ProfesorId", "TablaEquivalenciaId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 19, 10, 30, 0, 0, DateTimeKind.Utc), null, "Liceo N°5", "Matemática", "Matemática 1A - 2025", "8e445865-a24d-4543-a6c6-9443d048cdb9", 1 },
                    { 2, 2, new DateTime(2025, 6, 19, 10, 30, 0, 0, DateTimeKind.Utc), null, "Liceo N°5", "Historia", "Historia Universal - 2025", "9e445865-a24d-4543-a6c6-9443d048cdb0", 2 }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciaMedallas",
                columns: new[] { "EquivalenciaId", "MedallaId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "PerfilesEstudiantes",
                columns: new[] { "Id", "CantidadKudosDisponibles", "EstudianteId", "GrupoId", "MetaCalificacion", "Monedas", "PotenciadorActivoId", "RutaImagenCompleta", "RutaImagenMiniatura" },
                values: new object[,]
                {
                    { 1, 0, "a1445865-a24d-4543-a6c6-9443d048cdb1", 1, 8, 120, null, "default/avatar_full.jpg", "default/avatar_thumb.jpg" },
                    { 2, 0, "b2445865-a24d-4543-a6c6-9443d048cdb2", 1, 9, 150, null, "default/avatar_full.jpg", "default/avatar_thumb.jpg" },
                    { 3, 0, "c3445865-a24d-4543-a6c6-9443d048cdb3", 1, 7, 95, null, "default/avatar_full.jpg", "default/avatar_thumb.jpg" },
                    { 4, 0, "d4445865-a24d-4543-a6c6-9443d048cdb4", 2, 10, 200, null, "default/avatar_full.jpg", "default/avatar_thumb.jpg" },
                    { 5, 0, "e5445865-a24d-4543-a6c6-9443d048cdb5", 2, 8, 180, null, "default/avatar_full.jpg", "default/avatar_thumb.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Tiendas",
                columns: new[] { "Id", "GrupoId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Avatares",
                columns: new[] { "Id", "ColorFondo", "PerfilEstudianteId", "Rotacion", "Voltear", "Zoom" },
                values: new object[,]
                {
                    { 1, "b1e2ff", 1, 0, false, 100 },
                    { 2, "a7ffc4", 2, 0, false, 100 },
                    { 3, "ffafb9", 3, 0, false, 100 },
                    { 4, "ffffb1", 4, 0, false, 100 },
                    { 5, "e6e6e6", 5, 0, false, 100 }
                });

            migrationBuilder.InsertData(
                table: "Recompensas",
                columns: new[] { "Id", "Nombre", "Precio", "RecompensaTipo", "RequiereImagen", "RutaImagenCompleta", "RutaImagenMiniatura", "TiendaId" },
                values: new object[,]
                {
                    { 1, "Estrella Mágica", 50, "Simple", false, "star", "star", 1 },
                    { 2, "Regalo Sorpresa", 30, "Simple", false, "gift", "gift", 1 },
                    { 3, "Corazón Brillante", 20, "Simple", false, "heart", "heart", 1 },
                    { 4, "Medalla de Oro", 80, "Simple", false, "medal", "medal", 1 },
                    { 5, "Montón de Monedas", 100, "Simple", false, "coins", "coins", 1 },
                    { 6, "Trofeo Brillante", 70, "Simple", false, "trophy", "trophy", 2 },
                    { 7, "Llama de Fuego", 40, "Simple", false, "fire", "fire", 2 },
                    { 8, "Corona Real", 90, "Simple", false, "crown", "crown", 2 },
                    { 9, "Cohete Espacial", 60, "Simple", false, "rocket", "rocket", 2 },
                    { 10, "Robot Amistoso", 55, "Simple", false, "robot", "robot", 2 }
                });

            migrationBuilder.InsertData(
                table: "Recompensas",
                columns: new[] { "Id", "AtributoAvatarId", "Nombre", "Precio", "RecompensaTipo", "RequiereImagen", "RutaImagenCompleta", "RutaImagenMiniatura", "TiendaId" },
                values: new object[,]
                {
                    { 11, 21, "Item: ShortFlat", 0, "PersonalizacionAvatar", true, "top-shortFlat.png", "top-shortFlat.png", 1 },
                    { 12, 37, "Item: Default", 0, "PersonalizacionAvatar", true, "eyebrows-default.png", "eyebrows-default.png", 1 },
                    { 13, 38, "Item: DefaultNatural", 0, "PersonalizacionAvatar", true, "eyebrows-defaultNatural.png", "eyebrows-defaultNatural.png", 1 },
                    { 14, 68, "Item: Smile", 0, "PersonalizacionAvatar", true, "mouth-smile.png", "mouth-smile.png", 1 },
                    { 15, 91, "Item: ShirtVNeck", 0, "PersonalizacionAvatar", true, "clothing-shirtVNeck.png", "clothing-shirtVNeck.png", 1 },
                    { 16, 81, "Item: Sunglasses", 0, "PersonalizacionAvatar", true, "accessories-sunglasses.png", "accessories-sunglasses.png", 1 },
                    { 17, 71, "Item: BeardLight", 0, "PersonalizacionAvatar", true, "beard-beardLight.png", "beard-beardLight.png", 1 },
                    { 18, 95, "Item: edb98a", 0, "PersonalizacionAvatar", true, "skinColor-edb98a.png", "skinColor-edb98a.png", 1 },
                    { 19, 102, "Item: a55728", 0, "PersonalizacionAvatar", true, "hairColor-a55728.png", "hairColor-a55728.png", 1 },
                    { 20, 119, "Item: 3c4f5c", 0, "PersonalizacionAvatar", true, "clothesColor-3c4f5c.png", "clothesColor-3c4f5c.png", 1 },
                    { 21, 135, "Item: 262e33", 0, "PersonalizacionAvatar", true, "accessoriesColor-262e33.png", "accessoriesColor-262e33.png", 1 },
                    { 22, 112, "Item: a55728", 0, "PersonalizacionAvatar", true, "beardColor-a55728.png", "beardColor-a55728.png", 1 }
                });

            migrationBuilder.InsertData(
                table: "AvatarAtributos",
                columns: new[] { "AtributoSeleccionadoId", "AvatarId" },
                values: new object[,]
                {
                    { 21, 1 },
                    { 37, 1 },
                    { 38, 1 },
                    { 68, 1 },
                    { 71, 1 },
                    { 81, 1 },
                    { 91, 1 },
                    { 95, 1 },
                    { 102, 1 },
                    { 112, 1 },
                    { 119, 1 },
                    { 135, 1 }
                });

            migrationBuilder.InsertData(
                table: "PerfilEstudianteRecompensas",
                columns: new[] { "Id", "PerfilEstudianteId", "RecompensaId" },
                values: new object[,]
                {
                    { 1, 1, 11 },
                    { 2, 1, 12 },
                    { 3, 1, 13 },
                    { 4, 1, 14 },
                    { 5, 1, 15 },
                    { 6, 1, 16 },
                    { 7, 1, 17 },
                    { 8, 1, 18 },
                    { 9, 1, 19 },
                    { 10, 1, 20 },
                    { 11, 1, 21 },
                    { 12, 1, 22 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvatarAtributos_AtributoSeleccionadoId",
                table: "AvatarAtributos",
                column: "AtributoSeleccionadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avatares_PerfilEstudianteId",
                table: "Avatares",
                column: "PerfilEstudianteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BarrasProgreso_PerfilEstudianteId",
                table: "BarrasProgreso",
                column: "PerfilEstudianteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BarrasProgreso_TablaEquivalenciaId",
                table: "BarrasProgreso",
                column: "TablaEquivalenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_EquivalenciaMedallas_MedallaId",
                table: "EquivalenciaMedallas",
                column: "MedallaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equivalencias_TablaEquivalenciaId",
                table: "Equivalencias",
                column: "TablaEquivalenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudianteHitos_HitoId",
                table: "EstudianteHitos",
                column: "HitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_ProfesorId",
                table: "Grupos",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_EnlaceUnionId",
                table: "Grupos",
                column: "EnlaceUnionId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_Nombre",
                table: "Grupos",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_TablaEquivalenciaId",
                table: "Grupos",
                column: "TablaEquivalenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hitos_RecompensaId",
                table: "Hitos",
                column: "RecompensaId");

            migrationBuilder.CreateIndex(
                name: "IX_IniciosSesionUsuario_UsuarioId",
                table: "IniciosSesionUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_KudosOtorgados_PerfilEstudianteEmisorId",
                table: "KudosOtorgados",
                column: "PerfilEstudianteEmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_KudosOtorgados_PerfilEstudianteReceptorId",
                table: "KudosOtorgados",
                column: "PerfilEstudianteReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_KudosOtorgados_TipoKudoId",
                table: "KudosOtorgados",
                column: "TipoKudoId");

            migrationBuilder.CreateIndex(
                name: "IX_Medallas_Nombre",
                table: "Medallas",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Medallas_ProfesorId",
                table: "Medallas",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilesEstudiantes_EstudianteId",
                table: "PerfilesEstudiantes",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilesEstudiantes_PotenciadorActivoId",
                table: "PerfilesEstudiantes",
                column: "PotenciadorActivoId");

            migrationBuilder.CreateIndex(
                name: "UX_PerfilEstudiante_GrupoId_EstudianteId",
                table: "PerfilesEstudiantes",
                columns: new[] { "GrupoId", "EstudianteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilEstudianteMedallas_MedallaId",
                table: "PerfilEstudianteMedallas",
                column: "MedallaId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilEstudianteMedallas_PerfilEstudianteId",
                table: "PerfilEstudianteMedallas",
                column: "PerfilEstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilEstudianteRecompensas_PerfilEstudianteId_RecompensaId",
                table: "PerfilEstudianteRecompensas",
                columns: new[] { "PerfilEstudianteId", "RecompensaId" });

            migrationBuilder.CreateIndex(
                name: "IX_PerfilEstudianteRecompensas_RecompensaId",
                table: "PerfilEstudianteRecompensas",
                column: "RecompensaId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasRespuestasSeguridad_EstudianteId",
                table: "PreguntasRespuestasSeguridad",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasRespuestasSeguridad_PreguntaDeSeguridadId",
                table: "PreguntasRespuestasSeguridad",
                column: "PreguntaDeSeguridadId");

            migrationBuilder.CreateIndex(
                name: "IX_ReclamacionesRoles_RolId",
                table: "ReclamacionesRoles",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_ReclamacionesUsuario_UsuarioId",
                table: "ReclamacionesUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Recompensas_AtributoAvatarId",
                table: "Recompensas",
                column: "AtributoAvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Recompensas_Nombre",
                table: "Recompensas",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Recompensas_TiendaId",
                table: "Recompensas",
                column: "TiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_RendimientoPeriodoMedallas_MedallaId",
                table: "RendimientoPeriodoMedallas",
                column: "MedallaId");

            migrationBuilder.CreateIndex(
                name: "IX_RendimientoPeriodoMedallas_RendimientoPeriodoId_MedallaId",
                table: "RendimientoPeriodoMedallas",
                columns: new[] { "RendimientoPeriodoId", "MedallaId" });

            migrationBuilder.CreateIndex(
                name: "IX_RendimientosPeriodos_PerfilEstudianteId",
                table: "RendimientosPeriodos",
                column: "PerfilEstudianteId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NombreRolNormalizado",
                unique: true,
                filter: "[NombreRolNormalizado] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesUnion_EstudianteId",
                table: "SolicitudesUnion",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesUnion_GrupoId",
                table: "SolicitudesUnion",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_TablaClasificacionParticipantes_PerfilEstudianteId",
                table: "TablaClasificacionParticipantes",
                column: "PerfilEstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_TablasClasificacion_GrupoId",
                table: "TablasClasificacion",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_TablasClasificacion_MedallaAsociadaId",
                table: "TablasClasificacion",
                column: "MedallaAsociadaId");

            migrationBuilder.CreateIndex(
                name: "IX_TablasClasificacion_Nombre",
                table: "TablasClasificacion",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_TablasEquivalencia_Nombre",
                table: "TablasEquivalencia",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_TablasEquivalencia_ProfesorId",
                table: "TablasEquivalencia",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tiendas_GrupoId",
                table: "Tiendas",
                column: "GrupoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UmbralesParaMedallasPorKudos_GrupoId",
                table: "UmbralesParaMedallasPorKudos",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_UmbralesParaMedallasPorKudos_MedallaId",
                table: "UmbralesParaMedallasPorKudos",
                column: "MedallaId");

            migrationBuilder.CreateIndex(
                name: "IX_UmbralesParaMedallasPorKudos_TipoKudoId",
                table: "UmbralesParaMedallasPorKudos",
                column: "TipoKudoId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Usuarios",
                column: "CorreoNormalizado");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Usuarios",
                column: "NombreUsuarioNormalizado",
                unique: true,
                filter: "[NombreUsuarioNormalizado] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_RolId",
                table: "UsuariosRoles",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvatarAtributos");

            migrationBuilder.DropTable(
                name: "BarrasProgreso");

            migrationBuilder.DropTable(
                name: "EquivalenciaMedallas");

            migrationBuilder.DropTable(
                name: "EstudianteHitos");

            migrationBuilder.DropTable(
                name: "IniciosSesionUsuario");

            migrationBuilder.DropTable(
                name: "KudosOtorgados");

            migrationBuilder.DropTable(
                name: "PerfilEstudianteMedallas");

            migrationBuilder.DropTable(
                name: "PerfilEstudianteRecompensas");

            migrationBuilder.DropTable(
                name: "Pines");

            migrationBuilder.DropTable(
                name: "PreguntasRespuestasSeguridad");

            migrationBuilder.DropTable(
                name: "ReclamacionesRoles");

            migrationBuilder.DropTable(
                name: "ReclamacionesUsuario");

            migrationBuilder.DropTable(
                name: "RendimientoPeriodoMedallas");

            migrationBuilder.DropTable(
                name: "SolicitudesUnion");

            migrationBuilder.DropTable(
                name: "TablaClasificacionParticipantes");

            migrationBuilder.DropTable(
                name: "TokensUsuario");

            migrationBuilder.DropTable(
                name: "UmbralesParaMedallasPorKudos");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "Avatares");

            migrationBuilder.DropTable(
                name: "Equivalencias");

            migrationBuilder.DropTable(
                name: "Hitos");

            migrationBuilder.DropTable(
                name: "PreguntasDeSeguridad");

            migrationBuilder.DropTable(
                name: "RendimientosPeriodos");

            migrationBuilder.DropTable(
                name: "TablasClasificacion");

            migrationBuilder.DropTable(
                name: "TiposKudo");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "PerfilesEstudiantes");

            migrationBuilder.DropTable(
                name: "Medallas");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Recompensas");

            migrationBuilder.DropTable(
                name: "AtributosAvatar");

            migrationBuilder.DropTable(
                name: "Tiendas");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "EnlacesUnion");

            migrationBuilder.DropTable(
                name: "TablasEquivalencia");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
