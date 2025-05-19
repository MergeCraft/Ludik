using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ComprobarParaLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_nombreUsuario_Nombre",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "nombreUsuario_Nombre",
                table: "Usuarios",
                newName: "NombreUsuario_Nombre");

            migrationBuilder.AlterColumn<string>(
                name: "NombreUsuario_Nombre",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario_Nombre",
                table: "Usuarios",
                column: "NombreUsuario_Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_NombreUsuario_Nombre",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "NombreUsuario_Nombre",
                table: "Usuarios",
                newName: "nombreUsuario_Nombre");

            migrationBuilder.AlterColumn<string>(
                name: "nombreUsuario_Nombre",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_nombreUsuario_Nombre",
                table: "Usuarios",
                column: "nombreUsuario_Nombre",
                unique: true,
                filter: "[nombreUsuario_Nombre] IS NOT NULL");
        }
    }
}
