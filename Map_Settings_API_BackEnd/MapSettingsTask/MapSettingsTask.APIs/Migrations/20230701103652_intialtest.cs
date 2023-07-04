using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapConfig.APIs.Migrations
{
    /// <inheritdoc />
    public partial class intialtest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MapTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClusterRedius = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    IsGeofenced = table.Column<bool>(type: "bit", nullable: false),
                    TimeBuffer = table.Column<int>(type: "int", nullable: false),
                    LocationBuffer = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    MapTypeId = table.Column<int>(type: "int", nullable: false),
                    MapSubTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapSubType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapSubType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapSubType_MapTypes_MapTypeId",
                        column: x => x.MapTypeId,
                        principalTable: "MapTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MapSubType_MapTypeId",
                table: "MapSubType",
                column: "MapTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MapSubType");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "MapTypes");
        }
    }
}
