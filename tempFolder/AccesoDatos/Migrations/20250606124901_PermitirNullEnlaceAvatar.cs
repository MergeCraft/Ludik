using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class PermitirNullEnlaceAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "enlaceAvatar",
                table: "PerfilesEstudiantes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "enlaceAvatar",
                table: "PerfilesEstudiantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
