using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class SegundaConDataAnnotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "grupoId",
                table: "PerfilesEstudiantes",
                newName: "GrupoId");

            migrationBuilder.RenameColumn(
                name: "estudianteId",
                table: "PerfilesEstudiantes",
                newName: "EstudianteId");

            migrationBuilder.RenameColumn(
                name: "profesorId",
                table: "Grupos",
                newName: "ProfesorId");

            migrationBuilder.AlterColumn<string>(
                name: "nombreUsuario_Nombre",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NombreCompleto_Nombre",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NombreCompleto_Apellido",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "correo_Correro",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GrupoId",
                table: "Tiendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "TablasEquivalencia",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "TablasClasificacion",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Recompensas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Medallas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "Grupos",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_correo_Correro",
                table: "Usuarios",
                column: "correo_Correro",
                unique: true,
                filter: "[correo_Correro] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_nombreUsuario_Nombre",
                table: "Usuarios",
                column: "nombreUsuario_Nombre",
                unique: true,
                filter: "[nombreUsuario_Nombre] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TablasEquivalencia_Nombre",
                table: "TablasEquivalencia",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_TablasClasificacion_Nombre",
                table: "TablasClasificacion",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Recompensas_Nombre",
                table: "Recompensas",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "UX_PerfilEstudiante_GrupoId_EstudianteId",
                table: "PerfilesEstudiantes",
                columns: new[] { "GrupoId", "EstudianteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medallas_Nombre",
                table: "Medallas",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_ProfesorId",
                table: "Grupos",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_nombre",
                table: "Grupos",
                column: "nombre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_correo_Correro",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_nombreUsuario_Nombre",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_TablasEquivalencia_Nombre",
                table: "TablasEquivalencia");

            migrationBuilder.DropIndex(
                name: "IX_TablasClasificacion_Nombre",
                table: "TablasClasificacion");

            migrationBuilder.DropIndex(
                name: "IX_Recompensas_Nombre",
                table: "Recompensas");

            migrationBuilder.DropIndex(
                name: "UX_PerfilEstudiante_GrupoId_EstudianteId",
                table: "PerfilesEstudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Medallas_Nombre",
                table: "Medallas");

            migrationBuilder.DropIndex(
                name: "IX_Grupo_ProfesorId",
                table: "Grupos");

            migrationBuilder.DropIndex(
                name: "IX_Grupos_nombre",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "correo_Correro",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "GrupoId",
                table: "Tiendas");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "TablasEquivalencia");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "TablasClasificacion");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Recompensas");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Medallas");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "Grupos");

            migrationBuilder.RenameColumn(
                name: "GrupoId",
                table: "PerfilesEstudiantes",
                newName: "grupoId");

            migrationBuilder.RenameColumn(
                name: "EstudianteId",
                table: "PerfilesEstudiantes",
                newName: "estudianteId");

            migrationBuilder.RenameColumn(
                name: "ProfesorId",
                table: "Grupos",
                newName: "profesorId");

            migrationBuilder.AlterColumn<string>(
                name: "nombreUsuario_Nombre",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NombreCompleto_Nombre",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "NombreCompleto_Apellido",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
