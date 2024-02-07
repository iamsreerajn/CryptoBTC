using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoBTC.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoBTC");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "CandlesBTC",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "CandlesBTC",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "CandlesBTC",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "CandlesBTC");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "CandlesBTC");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "CandlesBTC");

            migrationBuilder.CreateTable(
                name: "CryptoBTC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClosedBTC = table.Column<double>(type: "float", nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedUSD = table.Column<double>(type: "float", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoBTC", x => x.Id);
                });
        }
    }
}
