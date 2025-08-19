using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class updateConfigTienda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recompensas_Tiendas_TiendaId",
                table: "Recompensas");

            migrationBuilder.DropIndex(
                name: "IX_Recompensas_TiendaId",
                table: "Recompensas");

            migrationBuilder.DropColumn(
                name: "TiendaId",
                table: "Recompensas");

            migrationBuilder.CreateTable(
                name: "RecompensaTienda",
                columns: table => new
                {
                    RecompesasId = table.Column<int>(type: "int", nullable: false),
                    TiendaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecompensaTienda", x => new { x.RecompesasId, x.TiendaId });
                    table.ForeignKey(
                        name: "FK_RecompensaTienda_Recompensas_RecompesasId",
                        column: x => x.RecompesasId,
                        principalTable: "Recompensas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecompensaTienda_Tiendas_TiendaId",
                        column: x => x.TiendaId,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecompensaTienda_TiendaId",
                table: "RecompensaTienda",
                column: "TiendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecompensaTienda");

            migrationBuilder.AddColumn<int>(
                name: "TiendaId",
                table: "Recompensas",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 1,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 2,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 3,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 4,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 5,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 6,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 7,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 8,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 9,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 10,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 101,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 102,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 103,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 104,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 105,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 106,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 107,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 108,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 109,
                column: "TiendaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 110,
                column: "TiendaId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Recompensas_TiendaId",
                table: "Recompensas",
                column: "TiendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recompensas_Tiendas_TiendaId",
                table: "Recompensas",
                column: "TiendaId",
                principalTable: "Tiendas",
                principalColumn: "Id");
        }
    }
}
