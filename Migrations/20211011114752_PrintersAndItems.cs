using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace hn_logic_api.Migrations
{
    public partial class PrintersAndItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "ProcessResults",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ItemCode = table.Column<string>(type: "text", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "PrintQueues",
                columns: table => new
                {
                    PrintQueueId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    ItemCode = table.Column<string>(type: "text", nullable: true),
                    IsPrinted = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintQueues", x => x.PrintQueueId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessResults_ItemId",
                table: "ProcessResults",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessResults_Items_ItemId",
                table: "ProcessResults",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessResults_Items_ItemId",
                table: "ProcessResults");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "PrintQueues");

            migrationBuilder.DropIndex(
                name: "IX_ProcessResults_ItemId",
                table: "ProcessResults");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ProcessResults");
        }
    }
}
