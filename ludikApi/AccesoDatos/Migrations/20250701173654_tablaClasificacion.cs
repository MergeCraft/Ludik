using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class tablaClasificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TablaClasificacionParticipantes_PerfilesEstudiantes_PerfilEstudianteId",
                table: "TablaClasificacionParticipantes");

            migrationBuilder.DropForeignKey(
                name: "FK_TablaClasificacionParticipantes_TablasClasificacion_TablaClasificacionId",
                table: "TablaClasificacionParticipantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TablaClasificacionParticipantes",
                table: "TablaClasificacionParticipantes");

            migrationBuilder.DropIndex(
                name: "IX_TablaClasificacionParticipantes_TablaClasificacionId",
                table: "TablaClasificacionParticipantes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TablaClasificacionParticipantes",
                table: "TablaClasificacionParticipantes",
                columns: new[] { "TablaClasificacionId", "PerfilEstudianteId" });

            migrationBuilder.CreateIndex(
                name: "IX_TablaClasificacionParticipantes_PerfilEstudianteId",
                table: "TablaClasificacionParticipantes",
                column: "PerfilEstudianteId");

            migrationBuilder.AddForeignKey(
                name: "FK_TablaClasificacionParticipantes_PerfilesEstudiantes_PerfilEstudianteId",
                table: "TablaClasificacionParticipantes",
                column: "PerfilEstudianteId",
                principalTable: "PerfilesEstudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TablaClasificacionParticipantes_TablasClasificacion_TablaClasificacionId",
                table: "TablaClasificacionParticipantes",
                column: "TablaClasificacionId",
                principalTable: "TablasClasificacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TablaClasificacionParticipantes_PerfilesEstudiantes_PerfilEstudianteId",
                table: "TablaClasificacionParticipantes");

            migrationBuilder.DropForeignKey(
                name: "FK_TablaClasificacionParticipantes_TablasClasificacion_TablaClasificacionId",
                table: "TablaClasificacionParticipantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TablaClasificacionParticipantes",
                table: "TablaClasificacionParticipantes");

            migrationBuilder.DropIndex(
                name: "IX_TablaClasificacionParticipantes_PerfilEstudianteId",
                table: "TablaClasificacionParticipantes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TablaClasificacionParticipantes",
                table: "TablaClasificacionParticipantes",
                columns: new[] { "PerfilEstudianteId", "TablaClasificacionId" });

            migrationBuilder.CreateIndex(
                name: "IX_TablaClasificacionParticipantes_TablaClasificacionId",
                table: "TablaClasificacionParticipantes",
                column: "TablaClasificacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TablaClasificacionParticipantes_PerfilesEstudiantes_PerfilEstudianteId",
                table: "TablaClasificacionParticipantes",
                column: "PerfilEstudianteId",
                principalTable: "PerfilesEstudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TablaClasificacionParticipantes_TablasClasificacion_TablaClasificacionId",
                table: "TablaClasificacionParticipantes",
                column: "TablaClasificacionId",
                principalTable: "TablasClasificacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
