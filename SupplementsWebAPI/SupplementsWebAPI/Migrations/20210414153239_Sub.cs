using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementsWebAPI.Migrations
{
    public partial class Sub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductSubCategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductSubCategoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductSubCategoryId",
                table: "Products",
                column: "ProductSubCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "ProductCategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductSubCategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductSubCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductSubCategoryId",
                table: "Products",
                column: "ProductSubCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "ProductCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
