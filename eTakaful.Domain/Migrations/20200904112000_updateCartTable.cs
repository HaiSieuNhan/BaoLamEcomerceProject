using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Domain.Migrations
{
    public partial class updateCartTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CartName",
                table: "Carts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartStatus",
                table: "Carts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartName",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartStatus",
                table: "Carts");
        }
    }
}
