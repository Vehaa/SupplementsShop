using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementsWebAPI.Migrations
{
    public partial class Address_U : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ShippingInformations_ShippingInformationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ShippingInformations");

            migrationBuilder.DropIndex(
                name: "IX_Users_ShippingInformationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShippingInformationId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ShippingInformationId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShippingInformations",
                columns: table => new
                {
                    ShippingInformationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingInformations", x => x.ShippingInformationId);
                    table.ForeignKey(
                        name: "FK_ShippingInformations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShippingInformationId",
                table: "Users",
                column: "ShippingInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingInformations_CityId",
                table: "ShippingInformations",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ShippingInformations_ShippingInformationId",
                table: "Users",
                column: "ShippingInformationId",
                principalTable: "ShippingInformations",
                principalColumn: "ShippingInformationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
