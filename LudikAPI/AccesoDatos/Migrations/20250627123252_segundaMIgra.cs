using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class segundaMIgra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barba",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "Boca",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "Cejas",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "ColorBarba",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "ColorGafas",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "ColorPelo",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "ColorPiel",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "ColorRopa",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "ColorSombrero",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "Gafas",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "Gorro",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "LogoRopa",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "Ojos",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "Pelo",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "ProbabilidadBarba",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "ProbabilidadGafas",
                table: "Avatares");

            migrationBuilder.DropColumn(
                name: "Ropa",
                table: "Avatares");

            migrationBuilder.CreateTable(
                name: "AtributosAvatar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    RutaRecurso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoUnico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtributosAvatar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtributosAvatar_Avatares_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatares",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtributosAvatar_AvatarId",
                table: "AtributosAvatar",
                column: "AvatarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtributosAvatar");

            migrationBuilder.AddColumn<string>(
                name: "Barba",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Boca",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cejas",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorBarba",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorGafas",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorPelo",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorPiel",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorRopa",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorSombrero",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gafas",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gorro",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogoRopa",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ojos",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pelo",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProbabilidadBarba",
                table: "Avatares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProbabilidadGafas",
                table: "Avatares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Ropa",
                table: "Avatares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
