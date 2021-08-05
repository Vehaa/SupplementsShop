using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementsWebAPI.Migrations
{
    public partial class logoAsBase64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "Brands",
                newName: "LogoAsByteArray");

            migrationBuilder.AddColumn<string>(
                name: "LogoAsBase64",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoAsBase64",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "LogoAsByteArray",
                table: "Brands",
                newName: "Logo");
        }
    }
}
