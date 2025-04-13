using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAndFound.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedPostAndItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecificLocation",
                table: "Items",
                newName: "Location");

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LostAt",
                table: "Items",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LostAt",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Items",
                newName: "SpecificLocation");
        }
    }
}
