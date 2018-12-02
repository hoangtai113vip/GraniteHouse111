using Microsoft.EntityFrameworkCore.Migrations;

namespace GraniteHouse.Data.Migrations
{
    public partial class fixdbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetProductsSelectedForAppointments_Appointments_AppointmentId",
                table: "GetProductsSelectedForAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_GetProductsSelectedForAppointments_Products_ProductId",
                table: "GetProductsSelectedForAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetProductsSelectedForAppointments",
                table: "GetProductsSelectedForAppointments");

            migrationBuilder.RenameTable(
                name: "GetProductsSelectedForAppointments",
                newName: "ProductsSelectedForAppointments");

            migrationBuilder.RenameIndex(
                name: "IX_GetProductsSelectedForAppointments_ProductId",
                table: "ProductsSelectedForAppointments",
                newName: "IX_ProductsSelectedForAppointments_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_GetProductsSelectedForAppointments_AppointmentId",
                table: "ProductsSelectedForAppointments",
                newName: "IX_ProductsSelectedForAppointments_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsSelectedForAppointments",
                table: "ProductsSelectedForAppointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSelectedForAppointments_Appointments_AppointmentId",
                table: "ProductsSelectedForAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSelectedForAppointments_Products_ProductId",
                table: "ProductsSelectedForAppointments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSelectedForAppointments_Appointments_AppointmentId",
                table: "ProductsSelectedForAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSelectedForAppointments_Products_ProductId",
                table: "ProductsSelectedForAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsSelectedForAppointments",
                table: "ProductsSelectedForAppointments");

            migrationBuilder.RenameTable(
                name: "ProductsSelectedForAppointments",
                newName: "GetProductsSelectedForAppointments");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsSelectedForAppointments_ProductId",
                table: "GetProductsSelectedForAppointments",
                newName: "IX_GetProductsSelectedForAppointments_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsSelectedForAppointments_AppointmentId",
                table: "GetProductsSelectedForAppointments",
                newName: "IX_GetProductsSelectedForAppointments_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetProductsSelectedForAppointments",
                table: "GetProductsSelectedForAppointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GetProductsSelectedForAppointments_Appointments_AppointmentId",
                table: "GetProductsSelectedForAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GetProductsSelectedForAppointments_Products_ProductId",
                table: "GetProductsSelectedForAppointments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
