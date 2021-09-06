using Microsoft.EntityFrameworkCore.Migrations;

namespace hn_logic_api.Migrations
{
    public partial class parallelActionOfSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParallelAction",
                table: "ProcessSteps",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParallelAction",
                table: "ProcessSteps");
        }
    }
}
