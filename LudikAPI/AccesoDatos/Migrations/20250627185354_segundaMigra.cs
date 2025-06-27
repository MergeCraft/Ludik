using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class segundaMigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecompensaTipo",
                table: "Recompensas",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<int>(
                name: "AtributoAvatarId",
                table: "Recompensas",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Recompensas",
                columns: new[] { "Id", "AtributoAvatarId", "Nombre", "Precio", "RecompensaTipo", "RutaImagenCompleta", "RutaImagenMiniatura", "TiendaId" },
                values: new object[,]
                {
                    { 11, 21, "Item: ShortFlat", 0, "PersonalizacionAvatar", "avatar/pelo/shortFlat.svg", "avatar/pelo/shortFlat.svg", 1 },
                    { 12, 37, "Item: Default", 0, "PersonalizacionAvatar", "avatar/cejas/default.svg", "avatar/cejas/default.svg", 1 },
                    { 13, 38, "Item: DefaultNatural", 0, "PersonalizacionAvatar", "avatar/cejas/defaultNatural.svg", "avatar/cejas/defaultNatural.svg", 1 },
                    { 14, 68, "Item: Smile", 0, "PersonalizacionAvatar", "avatar/boca/smile.svg", "avatar/boca/smile.svg", 1 },
                    { 15, 91, "Item: ShirtVNeck", 0, "PersonalizacionAvatar", "avatar/ropa/shirtVNeck.svg", "avatar/ropa/shirtVNeck.svg", 1 },
                    { 16, 81, "Item: Sunglasses", 0, "PersonalizacionAvatar", "avatar/gafas/sunglasses.svg", "avatar/gafas/sunglasses.svg", 1 },
                    { 17, 71, "Item: BeardLight", 0, "PersonalizacionAvatar", "avatar/barba/beardLight.svg", "avatar/barba/beardLight.svg", 1 },
                    { 18, 95, "Item: edb98a", 0, "PersonalizacionAvatar", "avatar/colorpiel/edb98a.svg", "avatar/colorpiel/edb98a.svg", 1 },
                    { 19, 102, "Item: a55728", 0, "PersonalizacionAvatar", "avatar/colorpelo/a55728.svg", "avatar/colorpelo/a55728.svg", 1 },
                    { 20, 119, "Item: 3c4f5c", 0, "PersonalizacionAvatar", "avatar/colorropa/3c4f5c.svg", "avatar/colorropa/3c4f5c.svg", 1 },
                    { 21, 135, "Item: 262e33", 0, "PersonalizacionAvatar", "avatar/colorgafas/262e33.svg", "avatar/colorgafas/262e33.svg", 1 },
                    { 22, 112, "Item: a55728", 0, "PersonalizacionAvatar", "avatar/colorbarba/a55728.svg", "avatar/colorbarba/a55728.svg", 1 }
                });

            migrationBuilder.InsertData(
                table: "PerfilEstudianteRecompensas",
                columns: new[] { "Id", "PerfilEstudianteId", "RecompensaId" },
                values: new object[,]
                {
                    { 1, 1, 11 },
                    { 2, 1, 12 },
                    { 3, 1, 13 },
                    { 4, 1, 14 },
                    { 5, 1, 15 },
                    { 6, 1, 16 },
                    { 7, 1, 17 },
                    { 8, 1, 18 },
                    { 9, 1, 19 },
                    { 10, 1, 20 },
                    { 11, 1, 21 },
                    { 12, 1, 22 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recompensas_AtributoAvatarId",
                table: "Recompensas",
                column: "AtributoAvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recompensas_AtributosAvatar_AtributoAvatarId",
                table: "Recompensas",
                column: "AtributoAvatarId",
                principalTable: "AtributosAvatar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recompensas_AtributosAvatar_AtributoAvatarId",
                table: "Recompensas");

            migrationBuilder.DropIndex(
                name: "IX_Recompensas_AtributoAvatarId",
                table: "Recompensas");

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PerfilEstudianteRecompensas",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DropColumn(
                name: "AtributoAvatarId",
                table: "Recompensas");

            migrationBuilder.AlterColumn<string>(
                name: "RecompensaTipo",
                table: "Recompensas",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);
        }
    }
}
