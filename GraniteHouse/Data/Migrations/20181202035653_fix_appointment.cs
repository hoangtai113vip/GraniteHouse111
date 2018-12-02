using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GraniteHouse.Data.Migrations
{
    public partial class fix_appointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetProductsSelectedForAppointments_Appoitments_AppointmentId",
                table: "GetProductsSelectedForAppointments");

            migrationBuilder.DropTable(
                name: "Appoitments");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppointmentDate = table.Column<DateTime>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerPhoneNumber = table.Column<string>(nullable: true),
                    CustomerEmail = table.Column<string>(nullable: true),
                    isConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GetProductsSelectedForAppointments_Appointments_AppointmentId",
                table: "GetProductsSelectedForAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetProductsSelectedForAppointments_Appointments_AppointmentId",
                table: "GetProductsSelectedForAppointments");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.CreateTable(
                name: "Appoitments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppoimentDate = table.Column<DateTime>(nullable: false),
                    CustormerEmail = table.Column<string>(nullable: true),
                    CustormerName = table.Column<string>(nullable: true),
                    CustormerPhoneNumber = table.Column<string>(nullable: true),
                    isConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoitments", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GetProductsSelectedForAppointments_Appoitments_AppointmentId",
                table: "GetProductsSelectedForAppointments",
                column: "AppointmentId",
                principalTable: "Appoitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
