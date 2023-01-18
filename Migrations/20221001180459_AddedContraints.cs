using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemlemPharmacy.Migrations
{
    public partial class AddedContraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BinCard_BatchNo",
                table: "BinCard");

            migrationBuilder.AlterColumn<string>(
                name: "Invoice",
                table: "BinCard",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_BatchNo_Invoice",
                table: "BinCard",
                columns: new[] { "BatchNo", "Invoice" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BinCard_BatchNo_Invoice",
                table: "BinCard");

            migrationBuilder.AlterColumn<string>(
                name: "Invoice",
                table: "BinCard",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_BatchNo",
                table: "BinCard",
                column: "BatchNo");
        }
    }
}
