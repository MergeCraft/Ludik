using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class terceraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtributosAvatar_Avatares_AvatarId",
                table: "AtributosAvatar");

            migrationBuilder.DropIndex(
                name: "IX_AtributosAvatar_AvatarId",
                table: "AtributosAvatar");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "AtributosAvatar");

            migrationBuilder.CreateTable(
                name: "AvatarAtributos",
                columns: table => new
                {
                    AtributosSeleccionadosId = table.Column<int>(type: "int", nullable: false),
                    AvatarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvatarAtributos", x => new { x.AtributosSeleccionadosId, x.AvatarId });
                    table.ForeignKey(
                        name: "FK_AvatarAtributos_AtributosAvatar_AtributosSeleccionadosId",
                        column: x => x.AtributosSeleccionadosId,
                        principalTable: "AtributosAvatar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvatarAtributos_Avatares_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvatarAtributos_AvatarId",
                table: "AvatarAtributos",
                column: "AvatarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvatarAtributos");

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "AtributosAvatar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AtributosAvatar_AvatarId",
                table: "AtributosAvatar",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AtributosAvatar_Avatares_AvatarId",
                table: "AtributosAvatar",
                column: "AvatarId",
                principalTable: "Avatares",
                principalColumn: "Id");
        }
    }
}
