using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemlemPharmacy.Migrations
{
    public partial class AddedMedicineTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicineTrack",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Invoice = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineTrack", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineTrack_BatchNo_Invoice",
                table: "MedicineTrack",
                columns: new[] { "BatchNo", "Invoice" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineTrack");
        }
    }
}
