using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace hn_logic_api.Migrations
{
    public partial class InitialNodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HnApps",
                columns: table => new
                {
                    HnAppId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AppName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HnApps", x => x.HnAppId);
                });

            migrationBuilder.CreateTable(
                name: "HnProcesses",
                columns: table => new
                {
                    HnProcessId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    HnAppId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HnProcesses", x => x.HnProcessId);
                    table.ForeignKey(
                        name: "FK_HnProcesses_HnApps_HnAppId",
                        column: x => x.HnAppId,
                        principalTable: "HnApps",
                        principalColumn: "HnAppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessSteps",
                columns: table => new
                {
                    ProcessStepId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    Comparison = table.Column<string>(type: "text", nullable: true),
                    ResultAction = table.Column<string>(type: "text", nullable: true),
                    ProcessId = table.Column<int>(type: "integer", nullable: false),
                    HnProcessId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessSteps", x => x.ProcessStepId);
                    table.ForeignKey(
                        name: "FK_ProcessSteps_HnProcesses_HnProcessId",
                        column: x => x.HnProcessId,
                        principalTable: "HnProcesses",
                        principalColumn: "HnProcessId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProcessStepId = table.Column<int>(type: "integer", nullable: false),
                    StrResult = table.Column<string>(type: "text", nullable: true),
                    NumResult = table.Column<float>(type: "real", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessResults_ProcessSteps_ProcessStepId",
                        column: x => x.ProcessStepId,
                        principalTable: "ProcessSteps",
                        principalColumn: "ProcessStepId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HnProcesses_HnAppId",
                table: "HnProcesses",
                column: "HnAppId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessResults_ProcessStepId",
                table: "ProcessResults",
                column: "ProcessStepId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessSteps_HnProcessId",
                table: "ProcessSteps",
                column: "HnProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessResults");

            migrationBuilder.DropTable(
                name: "ProcessSteps");

            migrationBuilder.DropTable(
                name: "HnProcesses");

            migrationBuilder.DropTable(
                name: "HnApps");
        }
    }
}
