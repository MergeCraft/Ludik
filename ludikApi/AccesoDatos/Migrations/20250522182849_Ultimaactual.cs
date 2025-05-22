using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Ultimaactual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grupos_EnlaceUnionId",
                table: "Grupos");

            migrationBuilder.AlterColumn<int>(
                name: "EnlaceUnionId",
                table: "Grupos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_EnlaceUnionId",
                table: "Grupos",
                column: "EnlaceUnionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grupos_EnlaceUnionId",
                table: "Grupos");

            migrationBuilder.AlterColumn<int>(
                name: "EnlaceUnionId",
                table: "Grupos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_EnlaceUnionId",
                table: "Grupos",
                column: "EnlaceUnionId",
                unique: true,
                filter: "[EnlaceUnionId] IS NOT NULL");
        }
    }
}
