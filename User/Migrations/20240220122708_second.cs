using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KalendarId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "UlogaId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "KorisnikId",
                keyValue: new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                columns: new[] { "LozinkaKorisnika", "Salt" },
                values: new object[] { "VCNlK4EAsI6IlHsXHrCo3PefXRWxB+i5CulkbBrQe5EfJUaVi74Osos+rlHwhkgLuYMKpPD+4OIVcqqGF65Xh36d6xonGkUFgKneetFS/mclT+2/zHOf3hkrvbEM7j3A+CYkcz/4xEMX8wIqB9e374kxAfafiBE3QTotvP95opS7xVSD7RyJLFYhnJGtZm6qWHpeSNxvlv0k4I2nYOJAQoT/MSrj5LwEZuZ4TcWak8Dq/X5iV7XI8Hatp0WOIrrzRPEP3dKqWu3NaRxyD30NQl5WGo/cKgG60Cm3jBYa/mz7LNwh7ReNY3jMdyHHqTUwEQ8bpv4KBDgjE4oSsbb8DQ==", "BW+cL4E0Jg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UlogaId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "KalendarId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "KorisnikId",
                keyValue: new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                columns: new[] { "KalendarId", "LozinkaKorisnika", "Salt" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c399700"), "/u9fSR1udvzFOl0+OlNEyOV5ax+GO5o4laz6RGXQ5bUeDasDvgNOEizA3+MdMMVNd0lehN1XglPJ4fRBqtN/edabeEk9ewDv9cT0i2SCNqpCk1zg9oNflXtOF9sUP9VD4o7bl8eAvfmsY2eSv2gTtFa8QpOqE1Z6G87pCanIUiBnBiRs/UWtBmhHI8uleAd4mC5Hu3nRyOp0TGRmaw993icSH/sOqdDL2OuAr2gUKB3lv3suE022mG2kt+P86p0c8a/SM1N17iwY5NUqbH75zHVEuzXpNYsnsMiHrrdenRH7O/jhNPQWVK8edysMPkZf917zVpwdolcFDB5112S1IQ==", "zcCFz5y1Fw==" });
        }
    }
}
