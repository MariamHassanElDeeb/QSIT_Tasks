using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapConfig.APIs.Migrations
{
    /// <inheritdoc />
    public partial class intialcreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapTypeId",
                table: "Settings");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_MapSubTypeId",
                table: "Settings",
                column: "MapSubTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_MapSubType_MapSubTypeId",
                table: "Settings",
                column: "MapSubTypeId",
                principalTable: "MapSubType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_MapSubType_MapSubTypeId",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_MapSubTypeId",
                table: "Settings");

            migrationBuilder.AddColumn<int>(
                name: "MapTypeId",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
