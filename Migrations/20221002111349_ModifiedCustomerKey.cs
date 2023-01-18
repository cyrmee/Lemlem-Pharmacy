using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemlemPharmacy.Migrations
{
    public partial class ModifiedCustomerKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldMedicine_Customer_CustomerPhone",
                table: "SoldMedicine");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldMedicine_Customer_CustomerPhone",
                table: "SoldMedicine",
                column: "CustomerPhone",
                principalTable: "Customer",
                principalColumn: "PhoneNo",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldMedicine_Customer_CustomerPhone",
                table: "SoldMedicine");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldMedicine_Customer_CustomerPhone",
                table: "SoldMedicine",
                column: "CustomerPhone",
                principalTable: "Customer",
                principalColumn: "PhoneNo");
        }
    }
}
