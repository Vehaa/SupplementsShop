using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementsWebAPI.Migrations
{
    public partial class PhotoAsBase64_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoThumb",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "PhotoAsBase64",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoAsBase64",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoThumb",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
