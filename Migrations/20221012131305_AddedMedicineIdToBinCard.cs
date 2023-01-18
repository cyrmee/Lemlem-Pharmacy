using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemlemPharmacy.Migrations
{
    public partial class AddedMedicineIdToBinCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MedicineId",
                table: "BinCard",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_MedicineId",
                table: "BinCard",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_BinCard_Medicine_MedicineId",
                table: "BinCard",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinCard_Medicine_MedicineId",
                table: "BinCard");

            migrationBuilder.DropIndex(
                name: "IX_BinCard_MedicineId",
                table: "BinCard");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "BinCard");
        }
    }
}
