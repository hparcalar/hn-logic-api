using Microsoft.EntityFrameworkCore.Migrations;

namespace hn_logic_api.Migrations
{
    public partial class waitUntilConditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConditionRealizeTimeout",
                table: "ProcessSteps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WaitUntilConditionRealized",
                table: "ProcessSteps",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConditionRealizeTimeout",
                table: "ProcessSteps");

            migrationBuilder.DropColumn(
                name: "WaitUntilConditionRealized",
                table: "ProcessSteps");
        }
    }
}
