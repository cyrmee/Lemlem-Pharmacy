using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemlemPharmacy.Migrations
{
    public partial class AddedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Medicine_BatchNo",
                table: "Medicine",
                column: "BatchNo");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");

            migrationBuilder.CreateTable(
                name: "BinCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Invoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountRecived = table.Column<int>(type: "int", nullable: false),
                    Damaged = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinCard_Medicine_BatchNo",
                        column: x => x.BatchNo,
                        principalTable: "Medicine",
                        principalColumn: "BatchNo",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.UniqueConstraint("AK_Customer_PhoneNo", x => x.PhoneNo);
                });

            migrationBuilder.CreateTable(
                name: "SoldMedicine",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PharmacistId = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SellingPrice = table.Column<float>(type: "real", nullable: false),
                    SellingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldMedicine", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_SoldMedicine_AspNetUsers_PharmacistId",
                        column: x => x.PharmacistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserName",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoldMedicine_Customer_CustomerPhone",
                        column: x => x.CustomerPhone,
                        principalTable: "Customer",
                        principalColumn: "PhoneNo");
                    table.ForeignKey(
                        name: "FK_SoldMedicine_Medicine_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
                        principalColumn: "Id",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_BatchNo",
                table: "Medicine",
                column: "BatchNo",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Medicine_Category",
                table: "Medicine",
                sql: "Category in ('Anti-Fungal', 'Anti-Allergy', 'Anti-Helmentic', 'Hormonal Drugs', 'ENT Drugs', 'NSAI', 'GIT', 'Anti-Respiratory', 'Narcotic and Anti-Psychotropic', 'Anti-Biotic', 'Vitamins and Minerals', 'CSV Drugs')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Medicine_Type",
                table: "Medicine",
                sql: "Type in ('LongTerm', 'ShortTerm')");

            migrationBuilder.CreateIndex(
                name: "IX_BinCard_BatchNo",
                table: "BinCard",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PhoneNo",
                table: "Customer",
                column: "PhoneNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoldMedicine_CustomerPhone",
                table: "SoldMedicine",
                column: "CustomerPhone");

            migrationBuilder.CreateIndex(
                name: "IX_SoldMedicine_MedicineId",
                table: "SoldMedicine",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_SoldMedicine_PharmacistId",
                table: "SoldMedicine",
                column: "PharmacistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinCard");

            migrationBuilder.DropTable(
                name: "SoldMedicine");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Medicine_BatchNo",
                table: "Medicine");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_BatchNo",
                table: "Medicine");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Medicine_Category",
                table: "Medicine");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Medicine_Type",
                table: "Medicine");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
