using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemlemPharmacy.Migrations
{
    public partial class UpdatedBinCardIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BinCard_BatchNo",
                table: "BinCard");

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_BatchNo_Id",
                table: "BinCard",
                columns: new[] { "BatchNo", "Id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BinCard_BatchNo_Id",
                table: "BinCard");

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_BatchNo",
                table: "BinCard",
                column: "BatchNo",
                unique: true);
        }
    }
}
