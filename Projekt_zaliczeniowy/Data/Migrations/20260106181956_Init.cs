using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projekt_zaliczeniowy.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priorytety",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorytety", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zgloszenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataUtworzenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KategoriaId = table.Column<int>(type: "int", nullable: false),
                    PriorytetId = table.Column<int>(type: "int", nullable: false),
                    UzytkownikId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zgloszenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zgloszenia_Kategorie_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zgloszenia_Priorytety_PriorytetId",
                        column: x => x.PriorytetId,
                        principalTable: "Priorytety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komentarze",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tresc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataDodania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ZgloszenieId = table.Column<int>(type: "int", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentarze", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komentarze_Zgloszenia_ZgloszenieId",
                        column: x => x.ZgloszenieId,
                        principalTable: "Zgloszenia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kategorie",
                columns: new[] { "Id", "Nazwa" },
                values: new object[,]
                {
                    { 1, "Frontend" },
                    { 2, "Backend" },
                    { 3, "Baza Danych" }
                });

            migrationBuilder.InsertData(
                table: "Priorytety",
                columns: new[] { "Id", "Nazwa" },
                values: new object[,]
                {
                    { 1, "Niski" },
                    { 2, "Wysoki" },
                    { 3, "Krytyczny" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Komentarze_ZgloszenieId",
                table: "Komentarze",
                column: "ZgloszenieId");

            migrationBuilder.CreateIndex(
                name: "IX_Zgloszenia_KategoriaId",
                table: "Zgloszenia",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zgloszenia_PriorytetId",
                table: "Zgloszenia",
                column: "PriorytetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komentarze");

            migrationBuilder.DropTable(
                name: "Zgloszenia");

            migrationBuilder.DropTable(
                name: "Kategorie");

            migrationBuilder.DropTable(
                name: "Priorytety");
        }
    }
}
