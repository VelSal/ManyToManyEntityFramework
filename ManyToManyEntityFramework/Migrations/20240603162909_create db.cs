#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

using Microsoft.EntityFrameworkCore.Migrations;

namespace ManyToManyEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class createdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    AuteurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.AuteurId);
                });

            migrationBuilder.CreateTable(
                name: "Boeken",
                columns: table => new
                {
                    BoekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuteurId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsNewRelease = table.Column<bool>(type: "bit", nullable: false),
                    IsBestSeller = table.Column<bool>(type: "bit", nullable: false),
                    BindingType = table.Column<int>(type: "int", nullable: true),
                    Afbeeldingpad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boeken", x => x.BoekId);
                    table.ForeignKey(
                        name: "FK_Boeken_Auteurs_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteurs",
                        principalColumn: "AuteurId");
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BoekId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                    table.ForeignKey(
                        name: "FK_Genres_Boeken_BoekId",
                        column: x => x.BoekId,
                        principalTable: "Boeken",
                        principalColumn: "BoekId");
                });

            migrationBuilder.CreateTable(
                name: "BoekGenres",
                columns: table => new
                {
                    BoekId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoekGenres", x => new { x.BoekId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_BoekGenres_Boeken_BoekId",
                        column: x => x.BoekId,
                        principalTable: "Boeken",
                        principalColumn: "BoekId");
                    table.ForeignKey(
                        name: "FK_BoekGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId");
                });

            migrationBuilder.InsertData(
                table: "Auteurs",
                columns: new[] { "AuteurId", "Naam" },
                values: new object[,]
                {
                    { 1, "Jan Jansen" },
                    { 2, "Sara Smit" },
                    { 3, "Kenan Kurda" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "BoekId", "Naam" },
                values: new object[,]
                {
                    { 1, null, "Fictie" },
                    { 2, null, "Science Fiction" },
                    { 3, null, "Thriller" },
                    { 4, null, "Action" },
                    { 5, null, "Comedy" },
                    { 6, null, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "Boeken",
                columns: new[] { "BoekId", "Afbeeldingpad", "AuteurId", "BindingType", "IsAvailable", "IsBestSeller", "IsNewRelease", "Titel" },
                values: new object[,]
                {
                    { 1, "/images/default.jpg", 1, null, true, false, false, "De Ruimte Verkenner" },
                    { 2, "/images/default.jpg", 1, null, true, false, false, "Werelden Verbinden" },
                    { 3, "/images/default.jpg", 2, null, true, false, false, "De Laatste Dag" },
                    { 4, "/images/default.jpg", 2, null, true, false, false, "De Laatste Dag 2" },
                    { 5, "/images/default.jpg", 2, null, true, false, false, "De Laatste Dag 3" }
                });

            migrationBuilder.InsertData(
                table: "BoekGenres",
                columns: new[] { "BoekId", "GenreId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 3 },
                    { 5, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boeken_AuteurId",
                table: "Boeken",
                column: "AuteurId");

            migrationBuilder.CreateIndex(
                name: "IX_BoekGenres_GenreId",
                table: "BoekGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_BoekId",
                table: "Genres",
                column: "BoekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoekGenres");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Boeken");

            migrationBuilder.DropTable(
                name: "Auteurs");
        }
    }
}
