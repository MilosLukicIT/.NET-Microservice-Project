using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generalizacija.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sastanaks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sastanak_type = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    nesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    razlog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tim = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sastanaks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sastanaks");
        }
    }
}
