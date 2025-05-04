using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAndFound.Migrations
{
    /// <inheritdoc />
    public partial class fm1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ban_AspNetUsers_AppUserId",
                table: "Ban");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ban",
                table: "Ban");

            migrationBuilder.RenameTable(
                name: "Ban",
                newName: "Bans");

            migrationBuilder.RenameIndex(
                name: "IX_Ban_AppUserId",
                table: "Bans",
                newName: "IX_Bans_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bans",
                table: "Bans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bans_AspNetUsers_AppUserId",
                table: "Bans",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bans_AspNetUsers_AppUserId",
                table: "Bans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bans",
                table: "Bans");

            migrationBuilder.RenameTable(
                name: "Bans",
                newName: "Ban");

            migrationBuilder.RenameIndex(
                name: "IX_Bans_AppUserId",
                table: "Ban",
                newName: "IX_Ban_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ban",
                table: "Ban",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ban_AspNetUsers_AppUserId",
                table: "Ban",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
