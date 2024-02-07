using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoBTC.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_coinBTCs",
                table: "coinBTCs");

            migrationBuilder.RenameTable(
                name: "coinBTCs",
                newName: "CoinBTCs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoinBTCs",
                table: "CoinBTCs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoinBTCs",
                table: "CoinBTCs");

            migrationBuilder.RenameTable(
                name: "CoinBTCs",
                newName: "coinBTCs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_coinBTCs",
                table: "coinBTCs",
                column: "Id");
        }
    }
}
