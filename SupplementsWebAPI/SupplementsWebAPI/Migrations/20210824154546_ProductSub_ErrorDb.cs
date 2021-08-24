using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementsWebAPI.Migrations
{
    public partial class ProductSub_ErrorDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductSubCategoryId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductSubCategories_ProductSubCategoryId",
                table: "Products",
                column: "ProductSubCategoryId",
                principalTable: "ProductSubCategories",
                principalColumn: "ProductSubCategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductSubCategories_ProductSubCategoryId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductSubCategoryId",
                table: "Products",
                column: "ProductSubCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "ProductCategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
