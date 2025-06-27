using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class iniConDatosPrecargados : Migration
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
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasRespuestasSeguridad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreguntasRespuestasSeguridad_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Medallas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    ProfesorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    GrupoId = table.Column<int>(type: "int", nullable: false)
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
                name: "RendimientosPeriodos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NotaObtenida = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: false),
                    TablaClasificacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaClasificacionParticipantes", x => new { x.PerfilEstudianteId, x.TablaClasificacionId });
                    table.ForeignKey(
                        name: "FK_TablaClasificacionParticipantes_PerfilesEstudiantes_PerfilEstudianteId",
                        column: x => x.PerfilEstudianteId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TablaClasificacionParticipantes_TablasClasificacion_TablaClasificacionId",
                        column: x => x.TablaClasificacionId,
                        principalTable: "TablasClasificacion",
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
                    Precio = table.Column<int>(type: "int", nullable: false),
                    TiendaId = table.Column<int>(type: "int", nullable: false),
                    RecompensaTipo = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Periodo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Multiplicador = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recompensas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recompensas_Tiendas_TiendaId",
                        column: x => x.TiendaId,
                        principalTable: "Tiendas",
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
                    MedallaId = table.Column<int>(type: "int", nullable: false),
                    RendimientoPeriodoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendimientoPeriodoMedallas", x => new { x.MedallaId, x.RendimientoPeriodoId });
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

            migrationBuilder.CreateTable(
                name: "Hitos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantMedallasRequeridas = table.Column<int>(type: "int", nullable: false),
                    RecompensaId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.InsertData(
                table: "AtributosAvatar",
                columns: new[] { "Id", "CodigoUnico", "Nombre", "RutaRecurso", "Tipo" },
                values: new object[,]
                {
                    { 1, "bigHair", "BigHair", "avatar/pelo/bigHair.svg", 0 },
                    { 2, "bob", "Bob", "avatar/pelo/bob.svg", 0 },
                    { 3, "bun", "Bun", "avatar/pelo/bun.svg", 0 },
                    { 4, "curly", "Curly", "avatar/pelo/curly.svg", 0 },
                    { 5, "curvy", "Curvy", "avatar/pelo/curvy.svg", 0 },
                    { 6, "dreads", "Dreads", "avatar/pelo/dreads.svg", 0 },
                    { 7, "dreads01", "Dreads01", "avatar/pelo/dreads01.svg", 0 },
                    { 8, "dreads02", "Dreads02", "avatar/pelo/dreads02.svg", 0 },
                    { 9, "frida", "Frida", "avatar/pelo/frida.svg", 0 },
                    { 10, "frizzle", "Frizzle", "avatar/pelo/frizzle.svg", 0 },
                    { 11, "fro", "Fro", "avatar/pelo/fro.svg", 0 },
                    { 12, "froBand", "FroBand", "avatar/pelo/froBand.svg", 0 },
                    { 13, "hat", "Hat", "avatar/pelo/hat.svg", 0 },
                    { 14, "hijab", "Hijab", "avatar/pelo/hijab.svg", 0 },
                    { 15, "longButNotTooLong", "LongButNotTooLong", "avatar/pelo/longButNotTooLong.svg", 0 },
                    { 16, "miaWallace", "MiaWallace", "avatar/pelo/miaWallace.svg", 0 },
                    { 17, "shaggy", "Shaggy", "avatar/pelo/shaggy.svg", 0 },
                    { 18, "shaggyMullet", "ShaggyMullet", "avatar/pelo/shaggyMullet.svg", 0 },
                    { 19, "shavedSides", "ShavedSides", "avatar/pelo/shavedSides.svg", 0 },
                    { 20, "shortCurly", "ShortCurly", "avatar/pelo/shortCurly.svg", 0 },
                    { 21, "shortFlat", "ShortFlat", "avatar/pelo/shortFlat.svg", 0 },
                    { 22, "shortRound", "ShortRound", "avatar/pelo/shortRound.svg", 0 },
                    { 23, "shortWaved", "ShortWaved", "avatar/pelo/shortWaved.svg", 0 },
                    { 24, "sides", "Sides", "avatar/pelo/sides.svg", 0 },
                    { 25, "straight01", "Straight01", "avatar/pelo/straight01.svg", 0 },
                    { 26, "straight02", "Straight02", "avatar/pelo/straight02.svg", 0 },
                    { 27, "straightAndStrand", "StraightAndStrand", "avatar/pelo/straightAndStrand.svg", 0 },
                    { 28, "theCaesar", "TheCaesar", "avatar/pelo/theCaesar.svg", 0 },
                    { 29, "theCaesarAndSidePart", "TheCaesarAndSidePart", "avatar/pelo/theCaesarAndSidePart.svg", 0 },
                    { 30, "turban", "Turban", "avatar/pelo/turban.svg", 0 },
                    { 31, "winterHat1", "WinterHat1", "avatar/pelo/winterHat1.svg", 0 },
                    { 32, "winterHat02", "WinterHat02", "avatar/pelo/winterHat02.svg", 0 },
                    { 33, "winterHat03", "WinterHat03", "avatar/pelo/winterHat03.svg", 0 },
                    { 34, "winterHat04", "WinterHat04", "avatar/pelo/winterHat04.svg", 0 },
                    { 35, "angry", "Angry", "avatar/cejas/angry.svg", 1 },
                    { 36, "angryNatural", "AngryNatural", "avatar/cejas/angryNatural.svg", 1 },
                    { 37, "default", "Default", "avatar/cejas/default.svg", 1 },
                    { 38, "defaultNatural", "DefaultNatural", "avatar/cejas/defaultNatural.svg", 1 },
                    { 39, "flatNatural", "FlatNatural", "avatar/cejas/flatNatural.svg", 1 },
                    { 40, "frownNatural", "FrownNatural", "avatar/cejas/frownNatural.svg", 1 },
                    { 41, "raisedExcited", "RaisedExcited", "avatar/cejas/raisedExcited.svg", 1 },
                    { 42, "raisedExcitedNatural", "RaisedExcitedNatural", "avatar/cejas/raisedExcitedNatural.svg", 1 },
                    { 43, "sadConcerned", "SadConcerned", "avatar/cejas/sadConcerned.svg", 1 },
                    { 44, "sadConcernedNatural", "SadConcernedNatural", "avatar/cejas/sadConcernedNatural.svg", 1 },
                    { 45, "unibrowNatural", "UnibrowNatural", "avatar/cejas/unibrowNatural.svg", 1 },
                    { 46, "upDown", "UpDown", "avatar/cejas/upDown.svg", 1 },
                    { 47, "upDownNatural", "UpDownNatural", "avatar/cejas/upDownNatural.svg", 1 },
                    { 48, "closed", "Closed", "avatar/ojos/closed.svg", 2 },
                    { 49, "cry", "Cry", "avatar/ojos/cry.svg", 2 },
                    { 50, "default", "Default", "avatar/ojos/default.svg", 2 },
                    { 51, "eyeRoll", "EyeRoll", "avatar/ojos/eyeRoll.svg", 2 },
                    { 52, "happy", "Happy", "avatar/ojos/happy.svg", 2 },
                    { 53, "hearts", "Hearts", "avatar/ojos/hearts.svg", 2 },
                    { 54, "side", "Side", "avatar/ojos/side.svg", 2 },
                    { 55, "squint", "Squint", "avatar/ojos/squint.svg", 2 },
                    { 56, "surprised", "Surprised", "avatar/ojos/surprised.svg", 2 },
                    { 57, "wink", "Wink", "avatar/ojos/wink.svg", 2 },
                    { 58, "winkWacky", "WinkWacky", "avatar/ojos/winkWacky.svg", 2 },
                    { 59, "xDizzy", "XDizzy", "avatar/ojos/xDizzy.svg", 2 },
                    { 60, "concerned", "Concerned", "avatar/boca/concerned.svg", 3 },
                    { 61, "default", "Default", "avatar/boca/default.svg", 3 },
                    { 62, "disbelief", "Disbelief", "avatar/boca/disbelief.svg", 3 },
                    { 63, "eating", "Eating", "avatar/boca/eating.svg", 3 },
                    { 64, "grimace", "Grimace", "avatar/boca/grimace.svg", 3 },
                    { 65, "sad", "Sad", "avatar/boca/sad.svg", 3 },
                    { 66, "screamOpen", "ScreamOpen", "avatar/boca/screamOpen.svg", 3 },
                    { 67, "serious", "Serious", "avatar/boca/serious.svg", 3 },
                    { 68, "smile", "Smile", "avatar/boca/smile.svg", 3 },
                    { 69, "tongue", "Tongue", "avatar/boca/tongue.svg", 3 },
                    { 70, "twinkle", "Twinkle", "avatar/boca/twinkle.svg", 3 },
                    { 71, "beardLight", "BeardLight", "avatar/barba/beardLight.svg", 4 },
                    { 72, "beardMajestic", "BeardMajestic", "avatar/barba/beardMajestic.svg", 4 },
                    { 73, "beardMedium", "BeardMedium", "avatar/barba/beardMedium.svg", 4 },
                    { 74, "moustacheFancy", "MoustacheFancy", "avatar/barba/moustacheFancy.svg", 4 },
                    { 75, "moustacheMagnum", "MoustacheMagnum", "avatar/barba/moustacheMagnum.svg", 4 },
                    { 76, "eyepatch", "Eyepatch", "avatar/gafas/eyepatch.svg", 5 },
                    { 77, "kurt", "Kurt", "avatar/gafas/kurt.svg", 5 },
                    { 78, "prescription01", "Prescription01", "avatar/gafas/prescription01.svg", 5 },
                    { 79, "prescription02", "Prescription02", "avatar/gafas/prescription02.svg", 5 },
                    { 80, "round", "Round", "avatar/gafas/round.svg", 5 },
                    { 81, "sunglasses", "Sunglasses", "avatar/gafas/sunglasses.svg", 5 },
                    { 82, "wayfarers", "Wayfarers", "avatar/gafas/wayfarers.svg", 5 },
                    { 83, "blazerAndShirt", "BlazerAndShirt", "avatar/ropa/blazerAndShirt.svg", 6 },
                    { 84, "blazerAndSweater", "BlazerAndSweater", "avatar/ropa/blazerAndSweater.svg", 6 },
                    { 85, "collarAndSweater", "CollarAndSweater", "avatar/ropa/collarAndSweater.svg", 6 },
                    { 86, "graphicShirt", "GraphicShirt", "avatar/ropa/graphicShirt.svg", 6 },
                    { 87, "hoodie", "Hoodie", "avatar/ropa/hoodie.svg", 6 },
                    { 88, "overall", "Overall", "avatar/ropa/overall.svg", 6 },
                    { 89, "shirtCrewNeck", "ShirtCrewNeck", "avatar/ropa/shirtCrewNeck.svg", 6 },
                    { 90, "shirtScoopNeck", "ShirtScoopNeck", "avatar/ropa/shirtScoopNeck.svg", 6 },
                    { 91, "shirtVNeck", "ShirtVNeck", "avatar/ropa/shirtVNeck.svg", 6 },
                    { 92, "614335", "614335", "avatar/colorpiel/614335.svg", 7 },
                    { 93, "ae5d29", "ae5d29", "avatar/colorpiel/ae5d29.svg", 7 },
                    { 94, "d08b5b", "d08b5b", "avatar/colorpiel/d08b5b.svg", 7 },
                    { 95, "edb98a", "edb98a", "avatar/colorpiel/edb98a.svg", 7 },
                    { 96, "f8d25c", "f8d25c", "avatar/colorpiel/f8d25c.svg", 7 },
                    { 97, "fd9841", "fd9841", "avatar/colorpiel/fd9841.svg", 7 },
                    { 98, "ffdbb4", "ffdbb4", "avatar/colorpiel/ffdbb4.svg", 7 },
                    { 99, "2c1b18", "2c1b18", "avatar/colorpelo/2c1b18.svg", 8 },
                    { 100, "4a312c", "4a312c", "avatar/colorpelo/4a312c.svg", 8 },
                    { 101, "724133", "724133", "avatar/colorpelo/724133.svg", 8 },
                    { 102, "a55728", "a55728", "avatar/colorpelo/a55728.svg", 8 },
                    { 103, "b58143", "b58143", "avatar/colorpelo/b58143.svg", 8 },
                    { 104, "c93305", "c93305", "avatar/colorpelo/c93305.svg", 8 },
                    { 105, "d6b370", "d6b370", "avatar/colorpelo/d6b370.svg", 8 },
                    { 106, "e8e1e1", "e8e1e1", "avatar/colorpelo/e8e1e1.svg", 8 },
                    { 107, "ecdcbf", "ecdcbf", "avatar/colorpelo/ecdcbf.svg", 8 },
                    { 108, "f59797", "f59797", "avatar/colorpelo/f59797.svg", 8 },
                    { 109, "2c1b18", "2c1b18", "avatar/colorbarba/2c1b18.svg", 9 },
                    { 110, "4a312c", "4a312c", "avatar/colorbarba/4a312c.svg", 9 },
                    { 111, "724133", "724133", "avatar/colorbarba/724133.svg", 9 },
                    { 112, "a55728", "a55728", "avatar/colorbarba/a55728.svg", 9 },
                    { 113, "b58143", "b58143", "avatar/colorbarba/b58143.svg", 9 },
                    { 114, "c93305", "c93305", "avatar/colorbarba/c93305.svg", 9 },
                    { 115, "d6b370", "d6b370", "avatar/colorbarba/d6b370.svg", 9 },
                    { 116, "e8e1e1", "e8e1e1", "avatar/colorbarba/e8e1e1.svg", 9 },
                    { 117, "ecdcbf", "ecdcbf", "avatar/colorbarba/ecdcbf.svg", 9 },
                    { 118, "f59797", "f59797", "avatar/colorbarba/f59797.svg", 9 },
                    { 119, "3c4f5c", "3c4f5c", "avatar/colorropa/3c4f5c.svg", 10 },
                    { 120, "65c9ff", "65c9ff", "avatar/colorropa/65c9ff.svg", 10 },
                    { 121, "262e33", "262e33", "avatar/colorropa/262e33.svg", 10 },
                    { 122, "5199e4", "5199e4", "avatar/colorropa/5199e4.svg", 10 },
                    { 123, "25557c", "25557c", "avatar/colorropa/25557c.svg", 10 },
                    { 124, "929598", "929598", "avatar/colorropa/929598.svg", 10 },
                    { 125, "a7ffc4", "a7ffc4", "avatar/colorropa/a7ffc4.svg", 10 },
                    { 126, "b1e2ff", "b1e2ff", "avatar/colorropa/b1e2ff.svg", 10 },
                    { 127, "e6e6e6", "e6e6e6", "avatar/colorropa/e6e6e6.svg", 10 },
                    { 128, "ff5c5c", "ff5c5c", "avatar/colorropa/ff5c5c.svg", 10 },
                    { 129, "ff488e", "ff488e", "avatar/colorropa/ff488e.svg", 10 },
                    { 130, "ffafb9", "ffafb9", "avatar/colorropa/ffafb9.svg", 10 },
                    { 131, "ffffb1", "ffffb1", "avatar/colorropa/ffffb1.svg", 10 },
                    { 132, "ffffff", "ffffff", "avatar/colorropa/ffffff.svg", 10 },
                    { 133, "3c4f5c", "3c4f5c", "avatar/colorgafas/3c4f5c.svg", 11 },
                    { 134, "65c9ff", "65c9ff", "avatar/colorgafas/65c9ff.svg", 11 },
                    { 135, "262e33", "262e33", "avatar/colorgafas/262e33.svg", 11 },
                    { 136, "5199e4", "5199e4", "avatar/colorgafas/5199e4.svg", 11 },
                    { 137, "25557c", "25557c", "avatar/colorgafas/25557c.svg", 11 },
                    { 138, "929598", "929598", "avatar/colorgafas/929598.svg", 11 },
                    { 139, "a7ffc4", "a7ffc4", "avatar/colorgafas/a7ffc4.svg", 11 },
                    { 140, "b1e2ff", "b1e2ff", "avatar/colorgafas/b1e2ff.svg", 11 },
                    { 141, "e6e6e6", "e6e6e6", "avatar/colorgafas/e6e6e6.svg", 11 },
                    { 142, "ff5c5c", "ff5c5c", "avatar/colorgafas/ff5c5c.svg", 11 },
                    { 143, "ff488e", "ff488e", "avatar/colorgafas/ff488e.svg", 11 },
                    { 144, "ffafb9", "ffafb9", "avatar/colorgafas/ffafb9.svg", 11 },
                    { 145, "ffdeb5", "ffdeb5", "avatar/colorgafas/ffdeb5.svg", 11 },
                    { 146, "ffffb1", "ffffb1", "avatar/colorgafas/ffffb1.svg", 11 },
                    { 147, "ffffff", "ffffff", "avatar/colorgafas/ffffff.svg", 11 }
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
                table: "Roles",
                columns: new[] { "RolId", "EstampaConcurrencia", "NombreRol", "NombreRolNormalizado" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", null, "Profesor", "PROFESOR" },
                    { "3d5e174e-3b0e-446f-86af-483d56fd7211", null, "Estudiante", "ESTUDIANTE" }
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
                    { 1, "Asistencia y participación en todas las clases del mes.", 30, "Participación Perfecta", "8e445865-a24d-4543-a6c6-9443d048cdb9", false, "icono_asistencia.png" },
                    { 2, "Ayuda destacada a compañeros en proyectos grupales.", 25, "Maestro de la Colaboración", "8e445865-a24d-4543-a6c6-9443d048cdb9", false, "icono_colaboracion.png" },
                    { 3, "Realización de preguntas perspicaces que enriquecen la clase.", 15, "Mente Curiosa", "9e445865-a24d-4543-a6c6-9443d048cdb0", false, "icono_pregunta.png" }
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
                columns: new[] { "Id", "EnlaceUnionId", "FCreacion", "Institucion", "Materia", "Nombre", "ProfesorId", "TablaEquivalenciaId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 19, 10, 30, 0, 0, DateTimeKind.Utc), "Liceo N°5", "Matemática", "Matemática 1A - 2025", "8e445865-a24d-4543-a6c6-9443d048cdb9", 1 },
                    { 2, 2, new DateTime(2025, 6, 19, 10, 30, 0, 0, DateTimeKind.Utc), "Liceo N°5", "Historia", "Historia Universal - 2025", "9e445865-a24d-4543-a6c6-9443d048cdb0", 2 }
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
                columns: new[] { "Id", "EstudianteId", "GrupoId", "MetaCalificacion", "Monedas", "RutaImagenCompleta", "RutaImagenMiniatura" },
                values: new object[,]
                {
                    { 1, "a1445865-a24d-4543-a6c6-9443d048cdb1", 1, 8, 120, "default/avatar_full.jpg", "default/avatar_thumb.jpg" },
                    { 2, "b2445865-a24d-4543-a6c6-9443d048cdb2", 1, 9, 150, "default/avatar_full.jpg", "default/avatar_thumb.jpg" },
                    { 3, "c3445865-a24d-4543-a6c6-9443d048cdb3", 1, 7, 95, "default/avatar_full.jpg", "default/avatar_thumb.jpg" },
                    { 4, "d4445865-a24d-4543-a6c6-9443d048cdb4", 2, 10, 200, "default/avatar_full.jpg", "default/avatar_thumb.jpg" },
                    { 5, "e5445865-a24d-4543-a6c6-9443d048cdb5", 2, 8, 180, "default/avatar_full.jpg", "default/avatar_thumb.jpg" }
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
                columns: new[] { "Id", "Nombre", "Precio", "RecompensaTipo", "RutaImagenCompleta", "RutaImagenMiniatura", "TiendaId" },
                values: new object[,]
                {
                    { 1, "Estrella Mágica", 50, "Simple", "star", "star", 1 },
                    { 2, "Regalo Sorpresa", 30, "Simple", "gift", "gift", 1 },
                    { 3, "Corazón Brillante", 20, "Simple", "heart", "heart", 1 },
                    { 4, "Medalla de Oro", 80, "Simple", "medal", "medal", 1 },
                    { 5, "Montón de Monedas", 100, "Simple", "coins", "coins", 1 },
                    { 6, "Trofeo Brillante", 70, "Simple", "trophy", "trophy", 2 },
                    { 7, "Llama de Fuego", 40, "Simple", "fire", "fire", 2 },
                    { 8, "Corona Real", 90, "Simple", "crown", "crown", 2 },
                    { 9, "Cohete Espacial", 60, "Simple", "rocket", "rocket", 2 },
                    { 10, "Robot Amistoso", 55, "Simple", "robot", "robot", 2 }
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
                name: "IX_PerfilEstudianteRecompensas_PerfilEstudianteId",
                table: "PerfilEstudianteRecompensas",
                column: "PerfilEstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilEstudianteRecompensas_RecompensaId",
                table: "PerfilEstudianteRecompensas",
                column: "RecompensaId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasRespuestasSeguridad_EstudianteId",
                table: "PreguntasRespuestasSeguridad",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReclamacionesRoles_RolId",
                table: "ReclamacionesRoles",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_ReclamacionesUsuario_UsuarioId",
                table: "ReclamacionesUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Recompensas_Nombre",
                table: "Recompensas",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Recompensas_TiendaId",
                table: "Recompensas",
                column: "TiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_RendimientoPeriodoMedallas_RendimientoPeriodoId",
                table: "RendimientoPeriodoMedallas",
                column: "RendimientoPeriodoId");

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
                name: "IX_TablaClasificacionParticipantes_TablaClasificacionId",
                table: "TablaClasificacionParticipantes",
                column: "TablaClasificacionId");

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
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "AtributosAvatar");

            migrationBuilder.DropTable(
                name: "Avatares");

            migrationBuilder.DropTable(
                name: "Equivalencias");

            migrationBuilder.DropTable(
                name: "Hitos");

            migrationBuilder.DropTable(
                name: "RendimientosPeriodos");

            migrationBuilder.DropTable(
                name: "TablasClasificacion");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Recompensas");

            migrationBuilder.DropTable(
                name: "PerfilesEstudiantes");

            migrationBuilder.DropTable(
                name: "Medallas");

            migrationBuilder.DropTable(
                name: "Tiendas");

            migrationBuilder.DropTable(
                name: "Estudiantes");

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
