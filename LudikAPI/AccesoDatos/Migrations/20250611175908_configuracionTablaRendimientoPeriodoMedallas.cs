using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class configuracionTablaRendimientoPeriodoMedallas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medallas_RendimientosPeriodos_RendimientoPeriodoId",
                table: "Medallas");

            migrationBuilder.DropIndex(
                name: "IX_Medallas_RendimientoPeriodoId",
                table: "Medallas");

            migrationBuilder.DropColumn(
                name: "RendimientoPeriodoId",
                table: "Medallas");

            migrationBuilder.CreateTable(
                name: "RendimientoPeriodoMedallas",
                columns: table => new
                {
                    MedallaId = table.Column<int>(type: "int", nullable: false),
                    RendimientoPeriodoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendimientoPeriodoMedallas", x => new { x.MedallaId, x.RendimientoPeriodoId });
                    table.ForeignKey(
                        name: "FK_RendimientoPeriodoMedallas_Medallas_MedallaId",
                        column: x => x.MedallaId,
                        principalTable: "Medallas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RendimientoPeriodoMedallas_RendimientosPeriodos_RendimientoPeriodoId",
                        column: x => x.RendimientoPeriodoId,
                        principalTable: "RendimientosPeriodos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RendimientoPeriodoMedallas_RendimientoPeriodoId",
                table: "RendimientoPeriodoMedallas",
                column: "RendimientoPeriodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RendimientoPeriodoMedallas");

            migrationBuilder.AddColumn<int>(
                name: "RendimientoPeriodoId",
                table: "Medallas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medallas_RendimientoPeriodoId",
                table: "Medallas",
                column: "RendimientoPeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medallas_RendimientosPeriodos_RendimientoPeriodoId",
                table: "Medallas",
                column: "RendimientoPeriodoId",
                principalTable: "RendimientosPeriodos",
                principalColumn: "Id");
        }
    }
}
