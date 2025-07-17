using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class segundaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UmbralesParaMedallasPorKudos_Medallas_MedallaId",
                table: "UmbralesParaMedallasPorKudos");

            migrationBuilder.DropForeignKey(
                name: "FK_UmbralesParaMedallasPorKudos_TiposKudo_TipoKudoId",
                table: "UmbralesParaMedallasPorKudos");

            migrationBuilder.AddForeignKey(
                name: "FK_UmbralesParaMedallasPorKudos_Medallas_MedallaId",
                table: "UmbralesParaMedallasPorKudos",
                column: "MedallaId",
                principalTable: "Medallas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UmbralesParaMedallasPorKudos_TiposKudo_TipoKudoId",
                table: "UmbralesParaMedallasPorKudos",
                column: "TipoKudoId",
                principalTable: "TiposKudo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UmbralesParaMedallasPorKudos_Medallas_MedallaId",
                table: "UmbralesParaMedallasPorKudos");

            migrationBuilder.DropForeignKey(
                name: "FK_UmbralesParaMedallasPorKudos_TiposKudo_TipoKudoId",
                table: "UmbralesParaMedallasPorKudos");

            migrationBuilder.AddForeignKey(
                name: "FK_UmbralesParaMedallasPorKudos_Medallas_MedallaId",
                table: "UmbralesParaMedallasPorKudos",
                column: "MedallaId",
                principalTable: "Medallas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UmbralesParaMedallasPorKudos_TiposKudo_TipoKudoId",
                table: "UmbralesParaMedallasPorKudos",
                column: "TipoKudoId",
                principalTable: "TiposKudo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
