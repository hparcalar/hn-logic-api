using Microsoft.EntityFrameworkCore.Migrations;

namespace hn_logic_api.Migrations
{
    public partial class ProcessSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessSteps_HnProcesses_HnProcessId",
                table: "ProcessSteps");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "ProcessSteps");

            migrationBuilder.AlterColumn<int>(
                name: "HnProcessId",
                table: "ProcessSteps",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DelayAfter",
                table: "ProcessSteps",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DelayBefore",
                table: "ProcessSteps",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessSteps_HnProcesses_HnProcessId",
                table: "ProcessSteps",
                column: "HnProcessId",
                principalTable: "HnProcesses",
                principalColumn: "HnProcessId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessSteps_HnProcesses_HnProcessId",
                table: "ProcessSteps");

            migrationBuilder.DropColumn(
                name: "DelayAfter",
                table: "ProcessSteps");

            migrationBuilder.DropColumn(
                name: "DelayBefore",
                table: "ProcessSteps");

            migrationBuilder.AlterColumn<int>(
                name: "HnProcessId",
                table: "ProcessSteps",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "ProcessSteps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessSteps_HnProcesses_HnProcessId",
                table: "ProcessSteps",
                column: "HnProcessId",
                principalTable: "HnProcesses",
                principalColumn: "HnProcessId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
