using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class cuartaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AtributosAvatar",
                columns: new[] { "Id", "CodigoUnico", "Nombre", "RutaRecurso", "Tipo" },
                values: new object[,]
                {
                    { 1, "bigHair", "BigHair", "avatar/pelo/bigHair.svg", 0 },
                    { 2, "bob", "Bob", "avatar/pelo/bob.svg", 0 },
                    { 3, "bun", "Bun", "avatar/pelo/bun.svg", 0 },
                    { 4, "curly", "Curly", "avatar/pelo/curly.svg", 0 },
                    { 5, "curvy", "Curvy", "avatar/pelo/curvy.svg", 0 },
                    { 6, "dreads", "Dreads", "avatar/pelo/dreads.svg", 0 },
                    { 7, "dreads01", "Dreads01", "avatar/pelo/dreads01.svg", 0 },
                    { 8, "dreads02", "Dreads02", "avatar/pelo/dreads02.svg", 0 },
                    { 9, "frida", "Frida", "avatar/pelo/frida.svg", 0 },
                    { 10, "frizzle", "Frizzle", "avatar/pelo/frizzle.svg", 0 },
                    { 11, "fro", "Fro", "avatar/pelo/fro.svg", 0 },
                    { 12, "froBand", "FroBand", "avatar/pelo/froBand.svg", 0 },
                    { 13, "hat", "Hat", "avatar/pelo/hat.svg", 0 },
                    { 14, "hijab", "Hijab", "avatar/pelo/hijab.svg", 0 },
                    { 15, "longButNotTooLong", "LongButNotTooLong", "avatar/pelo/longButNotTooLong.svg", 0 },
                    { 16, "miaWallace", "MiaWallace", "avatar/pelo/miaWallace.svg", 0 },
                    { 17, "shaggy", "Shaggy", "avatar/pelo/shaggy.svg", 0 },
                    { 18, "shaggyMullet", "ShaggyMullet", "avatar/pelo/shaggyMullet.svg", 0 },
                    { 19, "shavedSides", "ShavedSides", "avatar/pelo/shavedSides.svg", 0 },
                    { 20, "shortCurly", "ShortCurly", "avatar/pelo/shortCurly.svg", 0 },
                    { 21, "shortFlat", "ShortFlat", "avatar/pelo/shortFlat.svg", 0 },
                    { 22, "shortRound", "ShortRound", "avatar/pelo/shortRound.svg", 0 },
                    { 23, "shortWaved", "ShortWaved", "avatar/pelo/shortWaved.svg", 0 },
                    { 24, "sides", "Sides", "avatar/pelo/sides.svg", 0 },
                    { 25, "straight01", "Straight01", "avatar/pelo/straight01.svg", 0 },
                    { 26, "straight02", "Straight02", "avatar/pelo/straight02.svg", 0 },
                    { 27, "straightAndStrand", "StraightAndStrand", "avatar/pelo/straightAndStrand.svg", 0 },
                    { 28, "theCaesar", "TheCaesar", "avatar/pelo/theCaesar.svg", 0 },
                    { 29, "theCaesarAndSidePart", "TheCaesarAndSidePart", "avatar/pelo/theCaesarAndSidePart.svg", 0 },
                    { 30, "turban", "Turban", "avatar/pelo/turban.svg", 0 },
                    { 31, "winterHat1", "WinterHat1", "avatar/pelo/winterHat1.svg", 0 },
                    { 32, "winterHat02", "WinterHat02", "avatar/pelo/winterHat02.svg", 0 },
                    { 33, "winterHat03", "WinterHat03", "avatar/pelo/winterHat03.svg", 0 },
                    { 34, "winterHat04", "WinterHat04", "avatar/pelo/winterHat04.svg", 0 },
                    { 35, "angry", "Angry", "avatar/cejas/angry.svg", 1 },
                    { 36, "angryNatural", "AngryNatural", "avatar/cejas/angryNatural.svg", 1 },
                    { 37, "default", "Default", "avatar/cejas/default.svg", 1 },
                    { 38, "defaultNatural", "DefaultNatural", "avatar/cejas/defaultNatural.svg", 1 },
                    { 39, "flatNatural", "FlatNatural", "avatar/cejas/flatNatural.svg", 1 },
                    { 40, "frownNatural", "FrownNatural", "avatar/cejas/frownNatural.svg", 1 },
                    { 41, "raisedExcited", "RaisedExcited", "avatar/cejas/raisedExcited.svg", 1 },
                    { 42, "raisedExcitedNatural", "RaisedExcitedNatural", "avatar/cejas/raisedExcitedNatural.svg", 1 },
                    { 43, "sadConcerned", "SadConcerned", "avatar/cejas/sadConcerned.svg", 1 },
                    { 44, "sadConcernedNatural", "SadConcernedNatural", "avatar/cejas/sadConcernedNatural.svg", 1 },
                    { 45, "unibrowNatural", "UnibrowNatural", "avatar/cejas/unibrowNatural.svg", 1 },
                    { 46, "upDown", "UpDown", "avatar/cejas/upDown.svg", 1 },
                    { 47, "upDownNatural", "UpDownNatural", "avatar/cejas/upDownNatural.svg", 1 },
                    { 48, "closed", "Closed", "avatar/ojos/closed.svg", 2 },
                    { 49, "cry", "Cry", "avatar/ojos/cry.svg", 2 },
                    { 50, "default", "Default", "avatar/ojos/default.svg", 2 },
                    { 51, "eyeRoll", "EyeRoll", "avatar/ojos/eyeRoll.svg", 2 },
                    { 52, "happy", "Happy", "avatar/ojos/happy.svg", 2 },
                    { 53, "hearts", "Hearts", "avatar/ojos/hearts.svg", 2 },
                    { 54, "side", "Side", "avatar/ojos/side.svg", 2 },
                    { 55, "squint", "Squint", "avatar/ojos/squint.svg", 2 },
                    { 56, "surprised", "Surprised", "avatar/ojos/surprised.svg", 2 },
                    { 57, "wink", "Wink", "avatar/ojos/wink.svg", 2 },
                    { 58, "winkWacky", "WinkWacky", "avatar/ojos/winkWacky.svg", 2 },
                    { 59, "xDizzy", "XDizzy", "avatar/ojos/xDizzy.svg", 2 },
                    { 60, "concerned", "Concerned", "avatar/boca/concerned.svg", 3 },
                    { 61, "default", "Default", "avatar/boca/default.svg", 3 },
                    { 62, "disbelief", "Disbelief", "avatar/boca/disbelief.svg", 3 },
                    { 63, "eating", "Eating", "avatar/boca/eating.svg", 3 },
                    { 64, "grimace", "Grimace", "avatar/boca/grimace.svg", 3 },
                    { 65, "sad", "Sad", "avatar/boca/sad.svg", 3 },
                    { 66, "screamOpen", "ScreamOpen", "avatar/boca/screamOpen.svg", 3 },
                    { 67, "serious", "Serious", "avatar/boca/serious.svg", 3 },
                    { 68, "smile", "Smile", "avatar/boca/smile.svg", 3 },
                    { 69, "tongue", "Tongue", "avatar/boca/tongue.svg", 3 },
                    { 70, "twinkle", "Twinkle", "avatar/boca/twinkle.svg", 3 },
                    { 71, "beardLight", "BeardLight", "avatar/barba/beardLight.svg", 4 },
                    { 72, "beardMajestic", "BeardMajestic", "avatar/barba/beardMajestic.svg", 4 },
                    { 73, "beardMedium", "BeardMedium", "avatar/barba/beardMedium.svg", 4 },
                    { 74, "moustacheFancy", "MoustacheFancy", "avatar/barba/moustacheFancy.svg", 4 },
                    { 75, "moustacheMagnum", "MoustacheMagnum", "avatar/barba/moustacheMagnum.svg", 4 },
                    { 76, "eyepatch", "Eyepatch", "avatar/gafas/eyepatch.svg", 5 },
                    { 77, "kurt", "Kurt", "avatar/gafas/kurt.svg", 5 },
                    { 78, "prescription01", "Prescription01", "avatar/gafas/prescription01.svg", 5 },
                    { 79, "prescription02", "Prescription02", "avatar/gafas/prescription02.svg", 5 },
                    { 80, "round", "Round", "avatar/gafas/round.svg", 5 },
                    { 81, "sunglasses", "Sunglasses", "avatar/gafas/sunglasses.svg", 5 },
                    { 82, "wayfarers", "Wayfarers", "avatar/gafas/wayfarers.svg", 5 },
                    { 83, "blazerAndShirt", "BlazerAndShirt", "avatar/ropa/blazerAndShirt.svg", 6 },
                    { 84, "blazerAndSweater", "BlazerAndSweater", "avatar/ropa/blazerAndSweater.svg", 6 },
                    { 85, "collarAndSweater", "CollarAndSweater", "avatar/ropa/collarAndSweater.svg", 6 },
                    { 86, "graphicShirt", "GraphicShirt", "avatar/ropa/graphicShirt.svg", 6 },
                    { 87, "hoodie", "Hoodie", "avatar/ropa/hoodie.svg", 6 },
                    { 88, "overall", "Overall", "avatar/ropa/overall.svg", 6 },
                    { 89, "shirtCrewNeck", "ShirtCrewNeck", "avatar/ropa/shirtCrewNeck.svg", 6 },
                    { 90, "shirtScoopNeck", "ShirtScoopNeck", "avatar/ropa/shirtScoopNeck.svg", 6 },
                    { 91, "shirtVNeck", "ShirtVNeck", "avatar/ropa/shirtVNeck.svg", 6 },
                    { 92, "614335", "614335", "avatar/colorpiel/614335.svg", 7 },
                    { 93, "ae5d29", "ae5d29", "avatar/colorpiel/ae5d29.svg", 7 },
                    { 94, "d08b5b", "d08b5b", "avatar/colorpiel/d08b5b.svg", 7 },
                    { 95, "edb98a", "edb98a", "avatar/colorpiel/edb98a.svg", 7 },
                    { 96, "f8d25c", "f8d25c", "avatar/colorpiel/f8d25c.svg", 7 },
                    { 97, "fd9841", "fd9841", "avatar/colorpiel/fd9841.svg", 7 },
                    { 98, "ffdbb4", "ffdbb4", "avatar/colorpiel/ffdbb4.svg", 7 },
                    { 99, "2c1b18", "2c1b18", "avatar/colorpelo/2c1b18.svg", 8 },
                    { 100, "4a312c", "4a312c", "avatar/colorpelo/4a312c.svg", 8 },
                    { 101, "724133", "724133", "avatar/colorpelo/724133.svg", 8 },
                    { 102, "a55728", "a55728", "avatar/colorpelo/a55728.svg", 8 },
                    { 103, "b58143", "b58143", "avatar/colorpelo/b58143.svg", 8 },
                    { 104, "c93305", "c93305", "avatar/colorpelo/c93305.svg", 8 },
                    { 105, "d6b370", "d6b370", "avatar/colorpelo/d6b370.svg", 8 },
                    { 106, "e8e1e1", "e8e1e1", "avatar/colorpelo/e8e1e1.svg", 8 },
                    { 107, "ecdcbf", "ecdcbf", "avatar/colorpelo/ecdcbf.svg", 8 },
                    { 108, "f59797", "f59797", "avatar/colorpelo/f59797.svg", 8 },
                    { 109, "2c1b18", "2c1b18", "avatar/colorbarba/2c1b18.svg", 9 },
                    { 110, "4a312c", "4a312c", "avatar/colorbarba/4a312c.svg", 9 },
                    { 111, "724133", "724133", "avatar/colorbarba/724133.svg", 9 },
                    { 112, "a55728", "a55728", "avatar/colorbarba/a55728.svg", 9 },
                    { 113, "b58143", "b58143", "avatar/colorbarba/b58143.svg", 9 },
                    { 114, "c93305", "c93305", "avatar/colorbarba/c93305.svg", 9 },
                    { 115, "d6b370", "d6b370", "avatar/colorbarba/d6b370.svg", 9 },
                    { 116, "e8e1e1", "e8e1e1", "avatar/colorbarba/e8e1e1.svg", 9 },
                    { 117, "ecdcbf", "ecdcbf", "avatar/colorbarba/ecdcbf.svg", 9 },
                    { 118, "f59797", "f59797", "avatar/colorbarba/f59797.svg", 9 },
                    { 119, "3c4f5c", "3c4f5c", "avatar/colorropa/3c4f5c.svg", 10 },
                    { 120, "65c9ff", "65c9ff", "avatar/colorropa/65c9ff.svg", 10 },
                    { 121, "262e33", "262e33", "avatar/colorropa/262e33.svg", 10 },
                    { 122, "5199e4", "5199e4", "avatar/colorropa/5199e4.svg", 10 },
                    { 123, "25557c", "25557c", "avatar/colorropa/25557c.svg", 10 },
                    { 124, "929598", "929598", "avatar/colorropa/929598.svg", 10 },
                    { 125, "a7ffc4", "a7ffc4", "avatar/colorropa/a7ffc4.svg", 10 },
                    { 126, "b1e2ff", "b1e2ff", "avatar/colorropa/b1e2ff.svg", 10 },
                    { 127, "e6e6e6", "e6e6e6", "avatar/colorropa/e6e6e6.svg", 10 },
                    { 128, "ff5c5c", "ff5c5c", "avatar/colorropa/ff5c5c.svg", 10 },
                    { 129, "ff488e", "ff488e", "avatar/colorropa/ff488e.svg", 10 },
                    { 130, "ffafb9", "ffafb9", "avatar/colorropa/ffafb9.svg", 10 },
                    { 131, "ffffb1", "ffffb1", "avatar/colorropa/ffffb1.svg", 10 },
                    { 132, "ffffff", "ffffff", "avatar/colorropa/ffffff.svg", 10 },
                    { 133, "3c4f5c", "3c4f5c", "avatar/colorgafas/3c4f5c.svg", 11 },
                    { 134, "65c9ff", "65c9ff", "avatar/colorgafas/65c9ff.svg", 11 },
                    { 135, "262e33", "262e33", "avatar/colorgafas/262e33.svg", 11 },
                    { 136, "5199e4", "5199e4", "avatar/colorgafas/5199e4.svg", 11 },
                    { 137, "25557c", "25557c", "avatar/colorgafas/25557c.svg", 11 },
                    { 138, "929598", "929598", "avatar/colorgafas/929598.svg", 11 },
                    { 139, "a7ffc4", "a7ffc4", "avatar/colorgafas/a7ffc4.svg", 11 },
                    { 140, "b1e2ff", "b1e2ff", "avatar/colorgafas/b1e2ff.svg", 11 },
                    { 141, "e6e6e6", "e6e6e6", "avatar/colorgafas/e6e6e6.svg", 11 },
                    { 142, "ff5c5c", "ff5c5c", "avatar/colorgafas/ff5c5c.svg", 11 },
                    { 143, "ff488e", "ff488e", "avatar/colorgafas/ff488e.svg", 11 },
                    { 144, "ffafb9", "ffafb9", "avatar/colorgafas/ffafb9.svg", 11 },
                    { 145, "ffdeb5", "ffdeb5", "avatar/colorgafas/ffdeb5.svg", 11 },
                    { 146, "ffffb1", "ffffb1", "avatar/colorgafas/ffffb1.svg", 11 },
                    { 147, "ffffff", "ffffff", "avatar/colorgafas/ffffff.svg", 11 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "AtributosAvatar",
                keyColumn: "Id",
                keyValue: 147);
        }
    }
}
