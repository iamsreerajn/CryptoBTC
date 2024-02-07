using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoBTC.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoinBTCs",
                table: "CoinBTCs");

            migrationBuilder.RenameTable(
                name: "CoinBTCs",
                newName: "CryptoBTC");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CryptoBTC",
                table: "CryptoBTC",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CryptoBTC",
                table: "CryptoBTC");

            migrationBuilder.RenameTable(
                name: "CryptoBTC",
                newName: "CoinBTCs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoinBTCs",
                table: "CoinBTCs",
                column: "Id");
        }
    }
}
