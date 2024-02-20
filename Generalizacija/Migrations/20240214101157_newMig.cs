using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generalizacija.Migrations
{
    /// <inheritdoc />
    public partial class newMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sastanak_type",
                table: "Sastanaks",
                newName: "Tip sastanka");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tip sastanka",
                table: "Sastanaks",
                newName: "sastanak_type");
        }
    }
}
