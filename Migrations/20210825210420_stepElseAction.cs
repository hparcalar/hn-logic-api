using Microsoft.EntityFrameworkCore.Migrations;

namespace hn_logic_api.Migrations
{
    public partial class stepElseAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ElseAction",
                table: "ProcessSteps",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElseAction",
                table: "ProcessSteps");
        }
    }
}
