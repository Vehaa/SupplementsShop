using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementsWebAPI.Migrations
{
    public partial class Product_Photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureThumb",
                table: "Users",
                newName: "PhotoThumb");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Users",
                newName: "Photo");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "PhotoThumb",
                table: "Users",
                newName: "PictureThumb");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Users",
                newName: "Picture");
        }
    }
}
