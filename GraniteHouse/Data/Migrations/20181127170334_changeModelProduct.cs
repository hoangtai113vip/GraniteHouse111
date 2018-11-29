using Microsoft.EntityFrameworkCore.Migrations;

namespace GraniteHouse.Data.Migrations
{
    public partial class changeModelProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SpecialTags_ProductTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SpecialTagsId",
                table: "Products",
                newName: "SpecialTagsID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SpecialTags",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SpecialTagsID",
                table: "Products",
                column: "SpecialTagsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SpecialTags_SpecialTagsID",
                table: "Products",
                column: "SpecialTagsID",
                principalTable: "SpecialTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SpecialTags_SpecialTagsID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SpecialTagsID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SpecialTagsID",
                table: "Products",
                newName: "SpecialTagsId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SpecialTags",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SpecialTags_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "SpecialTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
