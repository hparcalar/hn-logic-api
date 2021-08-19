using Microsoft.EntityFrameworkCore.Migrations;

namespace hn_logic_api.Migrations
{
    public partial class procStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcStatus",
                table: "HnProcesses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcStatus",
                table: "HnProcesses");
        }
    }
}
