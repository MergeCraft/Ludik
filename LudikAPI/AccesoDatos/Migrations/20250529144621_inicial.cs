using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnlacesUnion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoBase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expiracion = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    pin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tiempoDeVida = table.Column<int>(type: "int", nullable: false),
                    fueUtilizado = table.Column<bool>(type: "bit", nullable: false)
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
                    ImagenPerfil = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "TablasEquivalencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfesorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablasEquivalencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablasEquivalencia_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Equivalencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nota = table.Column<int>(type: "int", nullable: false),
                    TablaEquivalenciaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equivalencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equivalencias_TablasEquivalencia_TablaEquivalenciaId",
                        column: x => x.TablaEquivalenciaId,
                        principalTable: "TablasEquivalencia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    institucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    materia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tablaEquivalenciaId = table.Column<int>(type: "int", nullable: false),
                    enlaceUnionId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupos_EnlacesUnion_enlaceUnionId",
                        column: x => x.enlaceUnionId,
                        principalTable: "EnlacesUnion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grupos_Profesores_ProfesorId1",
                        column: x => x.ProfesorId1,
                        principalTable: "Profesores",
                        principalColumn: "UsuarioId");
                    table.ForeignKey(
                        name: "FK_Grupos_TablasEquivalencia_tablaEquivalenciaId",
                        column: x => x.tablaEquivalenciaId,
                        principalTable: "TablasEquivalencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesUnion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estudianteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    grupoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesUnion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesUnion_Estudiantes_estudianteId",
                        column: x => x.estudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "UsuarioId");
                    table.ForeignKey(
                        name: "FK_SolicitudesUnion_Grupos_grupoId",
                        column: x => x.grupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "BarrasProgreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valorMin = table.Column<int>(type: "int", nullable: false),
                    valorMax = table.Column<int>(type: "int", nullable: false),
                    tablaEquivalenciaId = table.Column<int>(type: "int", nullable: false),
                    perfilEstudianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarrasProgreso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarrasProgreso_TablasEquivalencia_tablaEquivalenciaId",
                        column: x => x.tablaEquivalenciaId,
                        principalTable: "TablasEquivalencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hitos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantMedallasRequeridas = table.Column<int>(type: "int", nullable: false),
                    recompensaId = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hitos_Estudiantes_EstudianteId",
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
                    descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    icono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    monedasOtorgadas = table.Column<int>(type: "int", nullable: false),
                    tieneAsignacionMutua = table.Column<bool>(type: "bit", nullable: false),
                    EquivalenciaId = table.Column<int>(type: "int", nullable: true),
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: true),
                    ProfesorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RendimientoPeriodoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medallas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medallas_Equivalencias_EquivalenciaId",
                        column: x => x.EquivalenciaId,
                        principalTable: "Equivalencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Medallas_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "TablasClasificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    medallaAsociadaId = table.Column<int>(type: "int", nullable: false),
                    grupoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablasClasificacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablasClasificacion_Grupos_grupoId",
                        column: x => x.grupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TablasClasificacion_Medallas_medallaAsociadaId",
                        column: x => x.medallaAsociadaId,
                        principalTable: "Medallas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilesEstudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    avatarGrupoId = table.Column<int>(type: "int", nullable: false),
                    enlaceAvatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    metaCalificacion = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    monedas = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    TablaClasificacionId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_PerfilesEstudiantes_TablasClasificacion_TablaClasificacionId",
                        column: x => x.TablaClasificacionId,
                        principalTable: "TablasClasificacion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recompensas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: true),
                    TiendaId = table.Column<int>(type: "int", nullable: true),
                    periodo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    multiplicador = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recompensas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recompensas_PerfilesEstudiantes_PerfilEstudianteId",
                        column: x => x.PerfilEstudianteId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recompensas_Tiendas_TiendaId",
                        column: x => x.TiendaId,
                        principalTable: "Tiendas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RendimientosPeriodos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notaObtenida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    perfilEstudianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendimientosPeriodos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RendimientosPeriodos_PerfilesEstudiantes_perfilEstudianteId",
                        column: x => x.perfilEstudianteId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarrasProgreso_perfilEstudianteId",
                table: "BarrasProgreso",
                column: "perfilEstudianteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BarrasProgreso_tablaEquivalenciaId",
                table: "BarrasProgreso",
                column: "tablaEquivalenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equivalencias_TablaEquivalenciaId",
                table: "Equivalencias",
                column: "TablaEquivalenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_ProfesorId",
                table: "Grupos",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_enlaceUnionId",
                table: "Grupos",
                column: "enlaceUnionId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_nombre",
                table: "Grupos",
                column: "nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_ProfesorId1",
                table: "Grupos",
                column: "ProfesorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_tablaEquivalenciaId",
                table: "Grupos",
                column: "tablaEquivalenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hitos_EstudianteId",
                table: "Hitos",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Hitos_recompensaId",
                table: "Hitos",
                column: "recompensaId");

            migrationBuilder.CreateIndex(
                name: "IX_IniciosSesionUsuario_UsuarioId",
                table: "IniciosSesionUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Medallas_EquivalenciaId",
                table: "Medallas",
                column: "EquivalenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Medallas_Nombre",
                table: "Medallas",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Medallas_PerfilEstudianteId",
                table: "Medallas",
                column: "PerfilEstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Medallas_ProfesorId",
                table: "Medallas",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Medallas_RendimientoPeriodoId",
                table: "Medallas",
                column: "RendimientoPeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilesEstudiantes_EstudianteId",
                table: "PerfilesEstudiantes",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilesEstudiantes_TablaClasificacionId",
                table: "PerfilesEstudiantes",
                column: "TablaClasificacionId");

            migrationBuilder.CreateIndex(
                name: "UX_PerfilEstudiante_GrupoId_EstudianteId",
                table: "PerfilesEstudiantes",
                columns: new[] { "GrupoId", "EstudianteId" },
                unique: true);

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
                name: "IX_Recompensas_PerfilEstudianteId",
                table: "Recompensas",
                column: "PerfilEstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Recompensas_TiendaId",
                table: "Recompensas",
                column: "TiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_RendimientosPeriodos_perfilEstudianteId",
                table: "RendimientosPeriodos",
                column: "perfilEstudianteId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NombreRolNormalizado",
                unique: true,
                filter: "[NombreRolNormalizado] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesUnion_estudianteId",
                table: "SolicitudesUnion",
                column: "estudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesUnion_grupoId",
                table: "SolicitudesUnion",
                column: "grupoId");

            migrationBuilder.CreateIndex(
                name: "IX_TablasClasificacion_grupoId",
                table: "TablasClasificacion",
                column: "grupoId");

            migrationBuilder.CreateIndex(
                name: "IX_TablasClasificacion_medallaAsociadaId",
                table: "TablasClasificacion",
                column: "medallaAsociadaId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BarrasProgreso_PerfilesEstudiantes_perfilEstudianteId",
                table: "BarrasProgreso",
                column: "perfilEstudianteId",
                principalTable: "PerfilesEstudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hitos_Recompensas_recompensaId",
                table: "Hitos",
                column: "recompensaId",
                principalTable: "Recompensas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medallas_PerfilesEstudiantes_PerfilEstudianteId",
                table: "Medallas",
                column: "PerfilEstudianteId",
                principalTable: "PerfilesEstudiantes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medallas_RendimientosPeriodos_RendimientoPeriodoId",
                table: "Medallas",
                column: "RendimientoPeriodoId",
                principalTable: "RendimientosPeriodos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medallas_PerfilesEstudiantes_PerfilEstudianteId",
                table: "Medallas");

            migrationBuilder.DropForeignKey(
                name: "FK_RendimientosPeriodos_PerfilesEstudiantes_perfilEstudianteId",
                table: "RendimientosPeriodos");

            migrationBuilder.DropTable(
                name: "BarrasProgreso");

            migrationBuilder.DropTable(
                name: "Hitos");

            migrationBuilder.DropTable(
                name: "IniciosSesionUsuario");

            migrationBuilder.DropTable(
                name: "Pines");

            migrationBuilder.DropTable(
                name: "PreguntasRespuestasSeguridad");

            migrationBuilder.DropTable(
                name: "ReclamacionesRoles");

            migrationBuilder.DropTable(
                name: "ReclamacionesUsuario");

            migrationBuilder.DropTable(
                name: "SolicitudesUnion");

            migrationBuilder.DropTable(
                name: "TokensUsuario");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "Recompensas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Tiendas");

            migrationBuilder.DropTable(
                name: "PerfilesEstudiantes");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "TablasClasificacion");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Medallas");

            migrationBuilder.DropTable(
                name: "EnlacesUnion");

            migrationBuilder.DropTable(
                name: "Equivalencias");

            migrationBuilder.DropTable(
                name: "RendimientosPeriodos");

            migrationBuilder.DropTable(
                name: "TablasEquivalencia");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
