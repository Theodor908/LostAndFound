using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAndFound.Migrations
{
    /// <inheritdoc />
    public partial class lol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportBugs_AspNetUsers_AppUserId",
                table: "ReportBugs");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "ReportBugs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportBugs_AspNetUsers_AppUserId",
                table: "ReportBugs",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportBugs_AspNetUsers_AppUserId",
                table: "ReportBugs");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "ReportBugs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportBugs_AspNetUsers_AppUserId",
                table: "ReportBugs",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
