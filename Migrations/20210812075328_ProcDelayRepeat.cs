using Microsoft.EntityFrameworkCore.Migrations;

namespace hn_logic_api.Migrations
{
    public partial class ProcDelayRepeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanRepeat",
                table: "HnProcesses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DelayAfter",
                table: "HnProcesses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DelayBefore",
                table: "HnProcesses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanRepeat",
                table: "HnProcesses");

            migrationBuilder.DropColumn(
                name: "DelayAfter",
                table: "HnProcesses");

            migrationBuilder.DropColumn(
                name: "DelayBefore",
                table: "HnProcesses");
        }
    }
}
