using Microsoft.EntityFrameworkCore.Migrations;

namespace hn_logic_api.Migrations
{
    public partial class processLiveStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConnectionResetMessage",
                table: "HnProcesses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConnectionResetMessageDelay",
                table: "HnProcesses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeviceConnected",
                table: "HnProcesses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionResetMessage",
                table: "HnProcesses");

            migrationBuilder.DropColumn(
                name: "ConnectionResetMessageDelay",
                table: "HnProcesses");

            migrationBuilder.DropColumn(
                name: "IsDeviceConnected",
                table: "HnProcesses");
        }
    }
}
