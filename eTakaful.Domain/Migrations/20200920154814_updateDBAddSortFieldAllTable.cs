using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Domain.Migrations
{
    public partial class updateDBAddSortFieldAllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Users",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Suppliers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "ProductSizes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Products",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "ProductImages",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "ProductComments",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "ProductCommentReplys",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "ProductCommentImages",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "ProductColors",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "ProductAttributes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Orders",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "OrderHistories",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Manufacturers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Coupons",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Contacts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Collections",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Categories",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Carts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "CartDetails",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "ProductCommentReplys");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "ProductCommentImages");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "OrderHistories");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "CartDetails");
        }
    }
}
