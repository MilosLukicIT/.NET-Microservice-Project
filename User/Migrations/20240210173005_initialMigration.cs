using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UlogaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivUloge = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UlogaId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    KorisnikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImeKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezimeKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailKorisnika = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LozinkaKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KontaktKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumRegistracije = table.Column<DateOnly>(type: "date", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrvoLogovanje = table.Column<bool>(type: "bit", nullable: false),
                    TimId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KalendarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UlogaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.KorisnikId);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UlogaId",
                        column: x => x.UlogaId,
                        principalTable: "UserRoles",
                        principalColumn: "UlogaId");
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UlogaId", "NazivUloge" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c397123"), "Product owner" },
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c397222"), "Scrum master" },
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c397456"), "Developer" },
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c397478"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "KorisnikId", "DatumRegistracije", "EmailKorisnika", "ImeKorisnika", "KalendarId", "KontaktKorisnika", "LozinkaKorisnika", "PrezimeKorisnika", "PrvoLogovanje", "Salt", "TimId", "UlogaId" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new DateOnly(2020, 11, 15), "milos@mail.com", "Milos", new Guid("6a411c13-a195-48f7-8dbd-67596c399700"), "065777888", "/u9fSR1udvzFOl0+OlNEyOV5ax+GO5o4laz6RGXQ5bUeDasDvgNOEizA3+MdMMVNd0lehN1XglPJ4fRBqtN/edabeEk9ewDv9cT0i2SCNqpCk1zg9oNflXtOF9sUP9VD4o7bl8eAvfmsY2eSv2gTtFa8QpOqE1Z6G87pCanIUiBnBiRs/UWtBmhHI8uleAd4mC5Hu3nRyOp0TGRmaw993icSH/sOqdDL2OuAr2gUKB3lv3suE022mG2kt+P86p0c8a/SM1N17iwY5NUqbH75zHVEuzXpNYsnsMiHrrdenRH7O/jhNPQWVK8edysMPkZf917zVpwdolcFDB5112S1IQ==", "Lukic", true, "zcCFz5y1Fw==", new Guid("6a411c13-a195-48f7-8dbd-67596c399586"), new Guid("6a411c13-a195-48f7-8dbd-67596c397478") });

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailKorisnika",
                table: "Users",
                column: "EmailKorisnika",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UlogaId",
                table: "Users",
                column: "UlogaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
