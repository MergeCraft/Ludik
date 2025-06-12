using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ultimosCambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarrasProgreso_PerfilesEstudiantes_perfilEstudianteId",
                table: "BarrasProgreso");

            migrationBuilder.DropForeignKey(
                name: "FK_BarrasProgreso_TablasEquivalencia_tablaEquivalenciaId",
                table: "BarrasProgreso");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_EnlacesUnion_enlaceUnionId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_TablasEquivalencia_tablaEquivalenciaId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Hitos_Recompensas_recompensaId",
                table: "Hitos");

            migrationBuilder.DropForeignKey(
                name: "FK_RendimientosPeriodos_PerfilesEstudiantes_perfilEstudianteId",
                table: "RendimientosPeriodos");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesUnion_Estudiantes_estudianteId",
                table: "SolicitudesUnion");

            migrationBuilder.DropForeignKey(
                name: "FK_TablasClasificacion_Medallas_medallaAsociadaId",
                table: "TablasClasificacion");

            migrationBuilder.RenameColumn(
                name: "medallaAsociadaId",
                table: "TablasClasificacion",
                newName: "MedallaAsociadaId");

            migrationBuilder.RenameIndex(
                name: "IX_TablasClasificacion_medallaAsociadaId",
                table: "TablasClasificacion",
                newName: "IX_TablasClasificacion_MedallaAsociadaId");

            migrationBuilder.RenameColumn(
                name: "fecha",
                table: "SolicitudesUnion",
                newName: "Fecha");

            migrationBuilder.RenameColumn(
                name: "estudianteId",
                table: "SolicitudesUnion",
                newName: "EstudianteId");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitudesUnion_estudianteId",
                table: "SolicitudesUnion",
                newName: "IX_SolicitudesUnion_EstudianteId");

            migrationBuilder.RenameColumn(
                name: "perfilEstudianteId",
                table: "RendimientosPeriodos",
                newName: "PerfilEstudianteId");

            migrationBuilder.RenameColumn(
                name: "notaObtenida",
                table: "RendimientosPeriodos",
                newName: "NotaObtenida");

            migrationBuilder.RenameIndex(
                name: "IX_RendimientosPeriodos_perfilEstudianteId",
                table: "RendimientosPeriodos",
                newName: "IX_RendimientosPeriodos_PerfilEstudianteId");

            migrationBuilder.RenameColumn(
                name: "precio",
                table: "Recompensas",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "periodo",
                table: "Recompensas",
                newName: "Periodo");

            migrationBuilder.RenameColumn(
                name: "multiplicador",
                table: "Recompensas",
                newName: "Multiplicador");

            migrationBuilder.RenameColumn(
                name: "imagen",
                table: "Recompensas",
                newName: "Imagen");

            migrationBuilder.RenameColumn(
                name: "respuesta",
                table: "PreguntasRespuestasSeguridad",
                newName: "Respuesta");

            migrationBuilder.RenameColumn(
                name: "pregunta",
                table: "PreguntasRespuestasSeguridad",
                newName: "Pregunta");

            migrationBuilder.RenameColumn(
                name: "tiempoDeVida",
                table: "Pines",
                newName: "TiempoDeVida");

            migrationBuilder.RenameColumn(
                name: "idUsuario",
                table: "Pines",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "fueUtilizado",
                table: "Pines",
                newName: "FueUtilizado");

            migrationBuilder.RenameColumn(
                name: "fExpiracion",
                table: "Pines",
                newName: "FExpiracion");

            migrationBuilder.RenameColumn(
                name: "fCreacion",
                table: "Pines",
                newName: "FCreacion");

            migrationBuilder.RenameColumn(
                name: "pin",
                table: "Pines",
                newName: "Codigo");

            migrationBuilder.RenameColumn(
                name: "monedas",
                table: "PerfilesEstudiantes",
                newName: "Monedas");

            migrationBuilder.RenameColumn(
                name: "metaCalificacion",
                table: "PerfilesEstudiantes",
                newName: "MetaCalificacion");

            migrationBuilder.RenameColumn(
                name: "enlaceAvatar",
                table: "PerfilesEstudiantes",
                newName: "EnlaceAvatar");

            migrationBuilder.RenameColumn(
                name: "avatarGrupoId",
                table: "PerfilesEstudiantes",
                newName: "AvatarGrupoId");

            migrationBuilder.RenameColumn(
                name: "tieneAsignacionMutua",
                table: "Medallas",
                newName: "TieneAsignacionMutua");

            migrationBuilder.RenameColumn(
                name: "monedasOtorgadas",
                table: "Medallas",
                newName: "MonedasOtorgadas");

            migrationBuilder.RenameColumn(
                name: "icono",
                table: "Medallas",
                newName: "Icono");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "Medallas",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "recompensaId",
                table: "Hitos",
                newName: "RecompensaId");

            migrationBuilder.RenameColumn(
                name: "cantMedallasRequeridas",
                table: "Hitos",
                newName: "CantMedallasRequeridas");

            migrationBuilder.RenameIndex(
                name: "IX_Hitos_recompensaId",
                table: "Hitos",
                newName: "IX_Hitos_RecompensaId");

            migrationBuilder.RenameColumn(
                name: "tablaEquivalenciaId",
                table: "Grupos",
                newName: "TablaEquivalenciaId");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Grupos",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "materia",
                table: "Grupos",
                newName: "Materia");

            migrationBuilder.RenameColumn(
                name: "institucion",
                table: "Grupos",
                newName: "Institucion");

            migrationBuilder.RenameColumn(
                name: "fCreacion",
                table: "Grupos",
                newName: "FCreacion");

            migrationBuilder.RenameColumn(
                name: "enlaceUnionId",
                table: "Grupos",
                newName: "EnlaceUnionId");

            migrationBuilder.RenameIndex(
                name: "IX_Grupos_tablaEquivalenciaId",
                table: "Grupos",
                newName: "IX_Grupos_TablaEquivalenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_Grupos_nombre",
                table: "Grupos",
                newName: "IX_Grupos_Nombre");

            migrationBuilder.RenameIndex(
                name: "IX_Grupos_enlaceUnionId",
                table: "Grupos",
                newName: "IX_Grupos_EnlaceUnionId");

            migrationBuilder.RenameColumn(
                name: "nota",
                table: "Equivalencias",
                newName: "Nota");

            migrationBuilder.RenameColumn(
                name: "urlCompleta",
                table: "EnlacesUnion",
                newName: "UrlCompleta");

            migrationBuilder.RenameColumn(
                name: "expiracion",
                table: "EnlacesUnion",
                newName: "Expiracion");

            migrationBuilder.RenameColumn(
                name: "codigoBase",
                table: "EnlacesUnion",
                newName: "CodigoUnico");

            migrationBuilder.RenameColumn(
                name: "valorMin",
                table: "BarrasProgreso",
                newName: "ValorMin");

            migrationBuilder.RenameColumn(
                name: "valorMax",
                table: "BarrasProgreso",
                newName: "ValorMax");

            migrationBuilder.RenameColumn(
                name: "tablaEquivalenciaId",
                table: "BarrasProgreso",
                newName: "TablaEquivalenciaId");

            migrationBuilder.RenameColumn(
                name: "perfilEstudianteId",
                table: "BarrasProgreso",
                newName: "PerfilEstudianteId");

            migrationBuilder.RenameIndex(
                name: "IX_BarrasProgreso_tablaEquivalenciaId",
                table: "BarrasProgreso",
                newName: "IX_BarrasProgreso_TablaEquivalenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_BarrasProgreso_perfilEstudianteId",
                table: "BarrasProgreso",
                newName: "IX_BarrasProgreso_PerfilEstudianteId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarrasProgreso_PerfilesEstudiantes_PerfilEstudianteId",
                table: "BarrasProgreso",
                column: "PerfilEstudianteId",
                principalTable: "PerfilesEstudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarrasProgreso_TablasEquivalencia_TablaEquivalenciaId",
                table: "BarrasProgreso",
                column: "TablaEquivalenciaId",
                principalTable: "TablasEquivalencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_EnlacesUnion_EnlaceUnionId",
                table: "Grupos",
                column: "EnlaceUnionId",
                principalTable: "EnlacesUnion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_TablasEquivalencia_TablaEquivalenciaId",
                table: "Grupos",
                column: "TablaEquivalenciaId",
                principalTable: "TablasEquivalencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hitos_Recompensas_RecompensaId",
                table: "Hitos",
                column: "RecompensaId",
                principalTable: "Recompensas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RendimientosPeriodos_PerfilesEstudiantes_PerfilEstudianteId",
                table: "RendimientosPeriodos",
                column: "PerfilEstudianteId",
                principalTable: "PerfilesEstudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesUnion_Estudiantes_EstudianteId",
                table: "SolicitudesUnion",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TablasClasificacion_Medallas_MedallaAsociadaId",
                table: "TablasClasificacion",
                column: "MedallaAsociadaId",
                principalTable: "Medallas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarrasProgreso_PerfilesEstudiantes_PerfilEstudianteId",
                table: "BarrasProgreso");

            migrationBuilder.DropForeignKey(
                name: "FK_BarrasProgreso_TablasEquivalencia_TablaEquivalenciaId",
                table: "BarrasProgreso");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_EnlacesUnion_EnlaceUnionId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_TablasEquivalencia_TablaEquivalenciaId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Hitos_Recompensas_RecompensaId",
                table: "Hitos");

            migrationBuilder.DropForeignKey(
                name: "FK_RendimientosPeriodos_PerfilesEstudiantes_PerfilEstudianteId",
                table: "RendimientosPeriodos");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesUnion_Estudiantes_EstudianteId",
                table: "SolicitudesUnion");

            migrationBuilder.DropForeignKey(
                name: "FK_TablasClasificacion_Medallas_MedallaAsociadaId",
                table: "TablasClasificacion");

            migrationBuilder.RenameColumn(
                name: "MedallaAsociadaId",
                table: "TablasClasificacion",
                newName: "medallaAsociadaId");

            migrationBuilder.RenameIndex(
                name: "IX_TablasClasificacion_MedallaAsociadaId",
                table: "TablasClasificacion",
                newName: "IX_TablasClasificacion_medallaAsociadaId");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "SolicitudesUnion",
                newName: "fecha");

            migrationBuilder.RenameColumn(
                name: "EstudianteId",
                table: "SolicitudesUnion",
                newName: "estudianteId");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitudesUnion_EstudianteId",
                table: "SolicitudesUnion",
                newName: "IX_SolicitudesUnion_estudianteId");

            migrationBuilder.RenameColumn(
                name: "PerfilEstudianteId",
                table: "RendimientosPeriodos",
                newName: "perfilEstudianteId");

            migrationBuilder.RenameColumn(
                name: "NotaObtenida",
                table: "RendimientosPeriodos",
                newName: "notaObtenida");

            migrationBuilder.RenameIndex(
                name: "IX_RendimientosPeriodos_PerfilEstudianteId",
                table: "RendimientosPeriodos",
                newName: "IX_RendimientosPeriodos_perfilEstudianteId");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Recompensas",
                newName: "precio");

            migrationBuilder.RenameColumn(
                name: "Periodo",
                table: "Recompensas",
                newName: "periodo");

            migrationBuilder.RenameColumn(
                name: "Multiplicador",
                table: "Recompensas",
                newName: "multiplicador");

            migrationBuilder.RenameColumn(
                name: "Imagen",
                table: "Recompensas",
                newName: "imagen");

            migrationBuilder.RenameColumn(
                name: "Respuesta",
                table: "PreguntasRespuestasSeguridad",
                newName: "respuesta");

            migrationBuilder.RenameColumn(
                name: "Pregunta",
                table: "PreguntasRespuestasSeguridad",
                newName: "pregunta");

            migrationBuilder.RenameColumn(
                name: "TiempoDeVida",
                table: "Pines",
                newName: "tiempoDeVida");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Pines",
                newName: "idUsuario");

            migrationBuilder.RenameColumn(
                name: "FueUtilizado",
                table: "Pines",
                newName: "fueUtilizado");

            migrationBuilder.RenameColumn(
                name: "FExpiracion",
                table: "Pines",
                newName: "fExpiracion");

            migrationBuilder.RenameColumn(
                name: "FCreacion",
                table: "Pines",
                newName: "fCreacion");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "Pines",
                newName: "pin");

            migrationBuilder.RenameColumn(
                name: "Monedas",
                table: "PerfilesEstudiantes",
                newName: "monedas");

            migrationBuilder.RenameColumn(
                name: "MetaCalificacion",
                table: "PerfilesEstudiantes",
                newName: "metaCalificacion");

            migrationBuilder.RenameColumn(
                name: "EnlaceAvatar",
                table: "PerfilesEstudiantes",
                newName: "enlaceAvatar");

            migrationBuilder.RenameColumn(
                name: "AvatarGrupoId",
                table: "PerfilesEstudiantes",
                newName: "avatarGrupoId");

            migrationBuilder.RenameColumn(
                name: "TieneAsignacionMutua",
                table: "Medallas",
                newName: "tieneAsignacionMutua");

            migrationBuilder.RenameColumn(
                name: "MonedasOtorgadas",
                table: "Medallas",
                newName: "monedasOtorgadas");

            migrationBuilder.RenameColumn(
                name: "Icono",
                table: "Medallas",
                newName: "icono");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Medallas",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "RecompensaId",
                table: "Hitos",
                newName: "recompensaId");

            migrationBuilder.RenameColumn(
                name: "CantMedallasRequeridas",
                table: "Hitos",
                newName: "cantMedallasRequeridas");

            migrationBuilder.RenameIndex(
                name: "IX_Hitos_RecompensaId",
                table: "Hitos",
                newName: "IX_Hitos_recompensaId");

            migrationBuilder.RenameColumn(
                name: "TablaEquivalenciaId",
                table: "Grupos",
                newName: "tablaEquivalenciaId");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Grupos",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Materia",
                table: "Grupos",
                newName: "materia");

            migrationBuilder.RenameColumn(
                name: "Institucion",
                table: "Grupos",
                newName: "institucion");

            migrationBuilder.RenameColumn(
                name: "FCreacion",
                table: "Grupos",
                newName: "fCreacion");

            migrationBuilder.RenameColumn(
                name: "EnlaceUnionId",
                table: "Grupos",
                newName: "enlaceUnionId");

            migrationBuilder.RenameIndex(
                name: "IX_Grupos_TablaEquivalenciaId",
                table: "Grupos",
                newName: "IX_Grupos_tablaEquivalenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_Grupos_Nombre",
                table: "Grupos",
                newName: "IX_Grupos_nombre");

            migrationBuilder.RenameIndex(
                name: "IX_Grupos_EnlaceUnionId",
                table: "Grupos",
                newName: "IX_Grupos_enlaceUnionId");

            migrationBuilder.RenameColumn(
                name: "Nota",
                table: "Equivalencias",
                newName: "nota");

            migrationBuilder.RenameColumn(
                name: "UrlCompleta",
                table: "EnlacesUnion",
                newName: "urlCompleta");

            migrationBuilder.RenameColumn(
                name: "Expiracion",
                table: "EnlacesUnion",
                newName: "expiracion");

            migrationBuilder.RenameColumn(
                name: "CodigoUnico",
                table: "EnlacesUnion",
                newName: "codigoBase");

            migrationBuilder.RenameColumn(
                name: "ValorMin",
                table: "BarrasProgreso",
                newName: "valorMin");

            migrationBuilder.RenameColumn(
                name: "ValorMax",
                table: "BarrasProgreso",
                newName: "valorMax");

            migrationBuilder.RenameColumn(
                name: "TablaEquivalenciaId",
                table: "BarrasProgreso",
                newName: "tablaEquivalenciaId");

            migrationBuilder.RenameColumn(
                name: "PerfilEstudianteId",
                table: "BarrasProgreso",
                newName: "perfilEstudianteId");

            migrationBuilder.RenameIndex(
                name: "IX_BarrasProgreso_TablaEquivalenciaId",
                table: "BarrasProgreso",
                newName: "IX_BarrasProgreso_tablaEquivalenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_BarrasProgreso_PerfilEstudianteId",
                table: "BarrasProgreso",
                newName: "IX_BarrasProgreso_perfilEstudianteId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarrasProgreso_PerfilesEstudiantes_perfilEstudianteId",
                table: "BarrasProgreso",
                column: "perfilEstudianteId",
                principalTable: "PerfilesEstudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarrasProgreso_TablasEquivalencia_tablaEquivalenciaId",
                table: "BarrasProgreso",
                column: "tablaEquivalenciaId",
                principalTable: "TablasEquivalencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_EnlacesUnion_enlaceUnionId",
                table: "Grupos",
                column: "enlaceUnionId",
                principalTable: "EnlacesUnion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_TablasEquivalencia_tablaEquivalenciaId",
                table: "Grupos",
                column: "tablaEquivalenciaId",
                principalTable: "TablasEquivalencia",
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
                name: "FK_RendimientosPeriodos_PerfilesEstudiantes_perfilEstudianteId",
                table: "RendimientosPeriodos",
                column: "perfilEstudianteId",
                principalTable: "PerfilesEstudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesUnion_Estudiantes_estudianteId",
                table: "SolicitudesUnion",
                column: "estudianteId",
                principalTable: "Estudiantes",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TablasClasificacion_Medallas_medallaAsociadaId",
                table: "TablasClasificacion",
                column: "medallaAsociadaId",
                principalTable: "Medallas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
