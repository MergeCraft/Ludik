using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class configuracionTablaEstudianteHitos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hitos_Estudiantes_EstudianteId",
                table: "Hitos");

            migrationBuilder.DropIndex(
                name: "IX_Hitos_EstudianteId",
                table: "Hitos");

            migrationBuilder.DropColumn(
                name: "EstudianteId",
                table: "Hitos");

            migrationBuilder.CreateTable(
                name: "EstudianteHitos",
                columns: table => new
                {
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HitoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudianteHitos", x => new { x.EstudianteId, x.HitoId });
                    table.ForeignKey(
                        name: "FK_EstudianteHitos_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstudianteHitos_Hitos_HitoId",
                        column: x => x.HitoId,
                        principalTable: "Hitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstudianteHitos_HitoId",
                table: "EstudianteHitos",
                column: "HitoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstudianteHitos");

            migrationBuilder.AddColumn<string>(
                name: "EstudianteId",
                table: "Hitos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hitos_EstudianteId",
                table: "Hitos",
                column: "EstudianteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hitos_Estudiantes_EstudianteId",
                table: "Hitos",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "UsuarioId");
        }
    }
}
