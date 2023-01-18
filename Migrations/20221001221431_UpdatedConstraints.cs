using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemlemPharmacy.Migrations
{
    public partial class UpdatedConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BinCard_BatchNo_Invoice",
                table: "BinCard");

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_BatchNo",
                table: "BinCard",
                column: "BatchNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_Invoice",
                table: "BinCard",
                column: "Invoice",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BinCard_BatchNo",
                table: "BinCard");

            migrationBuilder.DropIndex(
                name: "IX_BinCard_Invoice",
                table: "BinCard");

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_BatchNo_Invoice",
                table: "BinCard",
                columns: new[] { "BatchNo", "Invoice" },
                unique: true);
        }
    }
}
