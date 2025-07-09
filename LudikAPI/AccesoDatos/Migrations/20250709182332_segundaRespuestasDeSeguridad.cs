using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class segundaRespuestasDeSeguridad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPreguntaDeSeguridadDelSistema",
                table: "PreguntasRespuestasSeguridad");

            migrationBuilder.InsertData(
                table: "PreguntasRespuestasSeguridad",
                columns: new[] { "Id", "EstudianteId", "PreguntaDeSeguridadId", "Respuesta" },
                values: new object[,]
                {
                    { 1, "a1445865-a24d-4543-a6c6-9443d048cdb1", 1, "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==" },
                    { 2, "a1445865-a24d-4543-a6c6-9443d048cdb1", 3, "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==" },
                    { 3, "b2445865-a24d-4543-a6c6-9443d048cdb2", 2, "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==" },
                    { 4, "b2445865-a24d-4543-a6c6-9443d048cdb2", 5, "AQAAAAIAAYagAAAAEICeFSdiFCtz68TPDuBQMzkt7RT8ecvoXx3nTwJev5fDDu098ITlRrx8fymingA8Mg==" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PreguntasRespuestasSeguridad",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PreguntasRespuestasSeguridad",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PreguntasRespuestasSeguridad",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PreguntasRespuestasSeguridad",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "IdPreguntaDeSeguridadDelSistema",
                table: "PreguntasRespuestasSeguridad",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
