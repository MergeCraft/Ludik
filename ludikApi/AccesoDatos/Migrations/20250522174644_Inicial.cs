using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
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
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario_Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Contrasenia_Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    NombreCompleto_Apellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NombreCompleto_Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    correo_Correo = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreguntasRespuestasSeguridad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstudianteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasRespuestasSeguridad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreguntasRespuestasSeguridad_Usuarios_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TablasEquivalencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablasEquivalencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablasEquivalencia_Usuarios_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BarrasProgreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valorMin = table.Column<int>(type: "int", nullable: false),
                    valorMax = table.Column<int>(type: "int", nullable: false),
                    tablaEquivalenciaId = table.Column<int>(type: "int", nullable: false)
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
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Institucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Materia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TablaEquivalenciaId = table.Column<int>(type: "int", nullable: false),
                    TiendaId = table.Column<int>(type: "int", nullable: false),
                    EnlaceUnionId = table.Column<int>(type: "int", nullable: true),
                    ProfesorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupos_EnlacesUnion_EnlaceUnionId",
                        column: x => x.EnlaceUnionId,
                        principalTable: "EnlacesUnion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grupos_TablasEquivalencia_TablaEquivalenciaId",
                        column: x => x.TablaEquivalenciaId,
                        principalTable: "TablasEquivalencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grupos_Tiendas_TiendaId",
                        column: x => x.TiendaId,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grupos_Usuarios_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesUnion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesUnion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesUnion_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudesUnion_Usuarios_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hitos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantMedallasRequeridas = table.Column<int>(type: "int", nullable: false),
                    recompensaId = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hitos_Usuarios_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medallas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    icono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    monedasOtorgadas = table.Column<int>(type: "int", nullable: false),
                    asignacionMutua = table.Column<bool>(type: "bit", nullable: false),
                    EquivalenciaId = table.Column<int>(type: "int", nullable: true),
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: true),
                    ProfesorId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Medallas_Usuarios_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
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
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    monedas = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    barraProgresoId = table.Column<int>(type: "int", nullable: false),
                    TablaClasificacionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilesEstudiantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilesEstudiantes_BarrasProgreso_barraProgresoId",
                        column: x => x.barraProgresoId,
                        principalTable: "BarrasProgreso",
                        principalColumn: "Id",
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
                    table.ForeignKey(
                        name: "FK_PerfilesEstudiantes_Usuarios_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Usuarios",
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
                    rangofecha_fechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rangofecha_fechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "IX_Grupos_EnlaceUnionId",
                table: "Grupos",
                column: "EnlaceUnionId",
                unique: true,
                filter: "[EnlaceUnionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_Nombre",
                table: "Grupos",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_TablaEquivalenciaId",
                table: "Grupos",
                column: "TablaEquivalenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_TiendaId",
                table: "Grupos",
                column: "TiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hitos_EstudianteId",
                table: "Hitos",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Hitos_recompensaId",
                table: "Hitos",
                column: "recompensaId");

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
                name: "IX_PerfilesEstudiantes_barraProgresoId",
                table: "PerfilesEstudiantes",
                column: "barraProgresoId");

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
                name: "IX_SolicitudesUnion_EstudianteId",
                table: "SolicitudesUnion",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesUnion_GrupoId",
                table: "SolicitudesUnion",
                column: "GrupoId");

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
                name: "IX_Usuarios_correo_Correo",
                table: "Usuarios",
                column: "correo_Correo",
                unique: true,
                filter: "[correo_Correo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario_Nombre",
                table: "Usuarios",
                column: "NombreUsuario_Nombre",
                unique: true);

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
                name: "FK_BarrasProgreso_TablasEquivalencia_tablaEquivalenciaId",
                table: "BarrasProgreso");

            migrationBuilder.DropForeignKey(
                name: "FK_Equivalencias_TablasEquivalencia_TablaEquivalenciaId",
                table: "Equivalencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_TablasEquivalencia_TablaEquivalenciaId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_EnlacesUnion_EnlaceUnionId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Tiendas_TiendaId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Usuarios_ProfesorId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medallas_Usuarios_ProfesorId",
                table: "Medallas");

            migrationBuilder.DropForeignKey(
                name: "FK_PerfilesEstudiantes_Usuarios_EstudianteId",
                table: "PerfilesEstudiantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Medallas_Equivalencias_EquivalenciaId",
                table: "Medallas");

            migrationBuilder.DropForeignKey(
                name: "FK_Medallas_PerfilesEstudiantes_PerfilEstudianteId",
                table: "Medallas");

            migrationBuilder.DropForeignKey(
                name: "FK_RendimientosPeriodos_PerfilesEstudiantes_perfilEstudianteId",
                table: "RendimientosPeriodos");

            migrationBuilder.DropTable(
                name: "Hitos");

            migrationBuilder.DropTable(
                name: "Pines");

            migrationBuilder.DropTable(
                name: "PreguntasRespuestasSeguridad");

            migrationBuilder.DropTable(
                name: "SolicitudesUnion");

            migrationBuilder.DropTable(
                name: "Recompensas");

            migrationBuilder.DropTable(
                name: "TablasEquivalencia");

            migrationBuilder.DropTable(
                name: "EnlacesUnion");

            migrationBuilder.DropTable(
                name: "Tiendas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Equivalencias");

            migrationBuilder.DropTable(
                name: "PerfilesEstudiantes");

            migrationBuilder.DropTable(
                name: "BarrasProgreso");

            migrationBuilder.DropTable(
                name: "TablasClasificacion");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Medallas");

            migrationBuilder.DropTable(
                name: "RendimientosPeriodos");
        }
    }
}
