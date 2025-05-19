using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class getterysetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_correo_Correro",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "correo_Correro",
                table: "Usuarios",
                newName: "correo_Correo");

            migrationBuilder.AddColumn<int>(
                name: "ProfesorId",
                table: "TablasEquivalencia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfesorId",
                table: "Medallas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_correo_Correo",
                table: "Usuarios",
                column: "correo_Correo",
                unique: true,
                filter: "[correo_Correo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TablasEquivalencia_ProfesorId",
                table: "TablasEquivalencia",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Medallas_ProfesorId",
                table: "Medallas",
                column: "ProfesorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_Usuarios_ProfesorId",
                table: "Grupos",
                column: "ProfesorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medallas_Usuarios_ProfesorId",
                table: "Medallas",
                column: "ProfesorId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TablasEquivalencia_Usuarios_ProfesorId",
                table: "TablasEquivalencia",
                column: "ProfesorId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Usuarios_ProfesorId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medallas_Usuarios_ProfesorId",
                table: "Medallas");

            migrationBuilder.DropForeignKey(
                name: "FK_TablasEquivalencia_Usuarios_ProfesorId",
                table: "TablasEquivalencia");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_correo_Correo",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_TablasEquivalencia_ProfesorId",
                table: "TablasEquivalencia");

            migrationBuilder.DropIndex(
                name: "IX_Medallas_ProfesorId",
                table: "Medallas");

            migrationBuilder.DropColumn(
                name: "ProfesorId",
                table: "TablasEquivalencia");

            migrationBuilder.DropColumn(
                name: "ProfesorId",
                table: "Medallas");

            migrationBuilder.RenameColumn(
                name: "correo_Correo",
                table: "Usuarios",
                newName: "correo_Correro");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_correo_Correro",
                table: "Usuarios",
                column: "correo_Correro",
                unique: true,
                filter: "[correo_Correro] IS NOT NULL");
        }
    }
}
