using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevIntel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameIntelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntelEntry_Users_CreatedById",
                table: "IntelEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntelEntry",
                table: "IntelEntry");

            migrationBuilder.RenameTable(
                name: "IntelEntry",
                newName: "IntelEntries");

            migrationBuilder.RenameIndex(
                name: "IX_IntelEntry_CreatedById",
                table: "IntelEntries",
                newName: "IX_IntelEntries_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntelEntries",
                table: "IntelEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IntelEntries_Users_CreatedById",
                table: "IntelEntries",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntelEntries_Users_CreatedById",
                table: "IntelEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntelEntries",
                table: "IntelEntries");

            migrationBuilder.RenameTable(
                name: "IntelEntries",
                newName: "IntelEntry");

            migrationBuilder.RenameIndex(
                name: "IX_IntelEntries_CreatedById",
                table: "IntelEntry",
                newName: "IX_IntelEntry_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntelEntry",
                table: "IntelEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IntelEntry_Users_CreatedById",
                table: "IntelEntry",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
