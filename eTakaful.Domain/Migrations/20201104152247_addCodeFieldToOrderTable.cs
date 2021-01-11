using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Domain.Migrations
{
    public partial class addCodeFieldToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Orders",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "OrderHistories",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatus",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatus",
                table: "OrderHistories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
