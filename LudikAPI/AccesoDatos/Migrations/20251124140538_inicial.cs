using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TablaClasificacionParticipantes");

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 3, 13 });

            migrationBuilder.AddColumn<int>(
                name: "PerfilEstudianteId",
                table: "TablasClasificacion",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "EquivalenciaMedallas",
                columns: new[] { "EquivalenciaId", "MedallaId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 3, 4 },
                    { 3, 5 },
                    { 4, 5 },
                    { 5, 5 },
                    { 6, 5 },
                    { 7, 5 },
                    { 8, 5 },
                    { 9, 5 },
                    { 10, 7 },
                    { 11, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TablasClasificacion_PerfilEstudianteId",
                table: "TablasClasificacion",
                column: "PerfilEstudianteId");

            migrationBuilder.AddForeignKey(
                name: "FK_TablasClasificacion_PerfilesEstudiantes_PerfilEstudianteId",
                table: "TablasClasificacion",
                column: "PerfilEstudianteId",
                principalTable: "PerfilesEstudiantes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TablasClasificacion_PerfilesEstudiantes_PerfilEstudianteId",
                table: "TablasClasificacion");

            migrationBuilder.DropIndex(
                name: "IX_TablasClasificacion_PerfilEstudianteId",
                table: "TablasClasificacion");

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 10, 7 });

            migrationBuilder.DeleteData(
                table: "EquivalenciaMedallas",
                keyColumns: new[] { "EquivalenciaId", "MedallaId" },
                keyValues: new object[] { 11, 9 });

            migrationBuilder.DropColumn(
                name: "PerfilEstudianteId",
                table: "TablasClasificacion");

            migrationBuilder.CreateTable(
                name: "TablaClasificacionParticipantes",
                columns: table => new
                {
                    TablaClasificacionId = table.Column<int>(type: "int", nullable: false),
                    PerfilEstudianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaClasificacionParticipantes", x => new { x.TablaClasificacionId, x.PerfilEstudianteId });
                    table.ForeignKey(
                        name: "FK_TablaClasificacionParticipantes_PerfilesEstudiantes_PerfilEstudianteId",
                        column: x => x.PerfilEstudianteId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TablaClasificacionParticipantes_TablasClasificacion_TablaClasificacionId",
                        column: x => x.TablaClasificacionId,
                        principalTable: "TablasClasificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EquivalenciaMedallas",
                columns: new[] { "EquivalenciaId", "MedallaId" },
                values: new object[] { 3, 13 });

            migrationBuilder.CreateIndex(
                name: "IX_TablaClasificacionParticipantes_PerfilEstudianteId",
                table: "TablaClasificacionParticipantes",
                column: "PerfilEstudianteId");
        }
    }
}
