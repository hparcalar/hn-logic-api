using Microsoft.EntityFrameworkCore.Migrations;

namespace hn_logic_api.Migrations
{
    public partial class conditionSatisfiedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConditionSatisfiedTime",
                table: "ProcessSteps",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConditionSatisfiedTime",
                table: "ProcessSteps");
        }
    }
}
