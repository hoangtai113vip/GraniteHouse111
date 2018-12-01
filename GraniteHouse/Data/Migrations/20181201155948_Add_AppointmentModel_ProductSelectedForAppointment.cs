using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GraniteHouse.Data.Migrations
{
    public partial class Add_AppointmentModel_ProductSelectedForAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appoitments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppoimentDate = table.Column<DateTime>(nullable: false),
                    CustormerName = table.Column<string>(nullable: true),
                    CustormerPhoneNumber = table.Column<string>(nullable: true),
                    CustormerEmail = table.Column<string>(nullable: true),
                    isConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoitments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GetProductsSelectedForAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppointmentId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetProductsSelectedForAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GetProductsSelectedForAppointments_Appoitments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appoitments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GetProductsSelectedForAppointments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GetProductsSelectedForAppointments_AppointmentId",
                table: "GetProductsSelectedForAppointments",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GetProductsSelectedForAppointments_ProductId",
                table: "GetProductsSelectedForAppointments",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetProductsSelectedForAppointments");

            migrationBuilder.DropTable(
                name: "Appoitments");
        }
    }
}
