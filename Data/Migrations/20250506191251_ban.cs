using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAndFound.Migrations
{
    /// <inheritdoc />
    public partial class ban : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnbannedAt",
                table: "Bans",
                newName: "BannedUntil");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BannedUntil",
                table: "Bans",
                newName: "UnbannedAt");
        }
    }
}
