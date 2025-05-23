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
            migrationBuilder.CreateSequence(
                name: "UsuarioSequence");

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
                name: "Estudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [UsuarioSequence]"),
                    ImagenPerfil = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Id);
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
                name: "Profesores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [UsuarioSequence]"),
                    ImagenPerfil = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.Id);
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
                        name: "FK_PreguntasRespuestasSeguridad_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
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
                        name: "FK_TablasEquivalencia_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id");
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
                    ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    materia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tablaEquivalenciaId = table.Column<int>(type: "int", nullable: false),
                    enlaceUnionId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Grupos_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    estudianteId = table.Column<int>(type: "int", nullable: false),
                    grupoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesUnion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesUnion_Estudiantes_estudianteId",
                        column: x => x.estudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    EstudianteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hitos_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id");
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
                        name: "FK_Medallas_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
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
                    TablaClasificacionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilesEstudiantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilesEstudiantes_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
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
                name: "IX_Estudiantes_NombreUsuario",
                table: "Estudiantes",
                column: "NombreUsuario",
                unique: true,
                filter: "[NombreUsuario] IS NOT NULL");

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
                name: "IX_Profesores_Email",
                table: "Profesores",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_NombreUsuario",
                table: "Profesores",
                column: "NombreUsuario",
                unique: true,
                filter: "[NombreUsuario] IS NOT NULL");

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
                name: "Pines");

            migrationBuilder.DropTable(
                name: "PreguntasRespuestasSeguridad");

            migrationBuilder.DropTable(
                name: "SolicitudesUnion");

            migrationBuilder.DropTable(
                name: "Recompensas");

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

            migrationBuilder.DropSequence(
                name: "UsuarioSequence");
        }
    }
}
