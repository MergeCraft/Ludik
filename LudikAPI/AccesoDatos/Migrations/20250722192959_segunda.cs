using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class segunda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CantidadKudosDisponibles",
                table: "PerfilesEstudiantes",
                newName: "KudosDisponiblesParaOtorgar");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaObtencion",
                table: "PerfilEstudianteMedallas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaObtencion",
                table: "PerfilEstudianteMedallas");

            migrationBuilder.RenameColumn(
                name: "KudosDisponiblesParaOtorgar",
                table: "PerfilesEstudiantes",
                newName: "CantidadKudosDisponibles");
        }
    }
}
