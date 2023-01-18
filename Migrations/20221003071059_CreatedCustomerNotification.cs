using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemlemPharmacy.Migrations
{
    public partial class CreatedCustomerNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerNotification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerNotification_Customer_PhoneNo",
                        column: x => x.PhoneNo,
                        principalTable: "Customer",
                        principalColumn: "PhoneNo",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerNotification_Medicine_BatchNo",
                        column: x => x.BatchNo,
                        principalTable: "Medicine",
                        principalColumn: "BatchNo",
						onUpdate: ReferentialAction.Cascade,
						onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotification_BatchNo",
                table: "CustomerNotification",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotification_PhoneNo",
                table: "CustomerNotification",
                column: "PhoneNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerNotification");
        }
    }
}
