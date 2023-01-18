using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemlemPharmacy.Migrations
{
    public partial class UpdatedConstraints2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BinCard_Invoice",
                table: "BinCard");

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_Invoice",
                table: "BinCard",
                column: "Invoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BinCard_Invoice",
                table: "BinCard");

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_Invoice",
                table: "BinCard",
                column: "Invoice",
                unique: true);
        }
    }
}
