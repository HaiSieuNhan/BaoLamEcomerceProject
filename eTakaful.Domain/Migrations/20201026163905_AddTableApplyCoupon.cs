using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Domain.Migrations
{
    public partial class AddTableApplyCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponName",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "CouponName",
                table: "Orders",
                newName: "PaymentMethod");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Suppliers",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CouponApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CartId = table.Column<Guid>(nullable: false),
                    CouponId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponApplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponApplies_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CouponApplies_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouponApplies_CartId",
                table: "CouponApplies",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponApplies_CouponId",
                table: "CouponApplies",
                column: "CouponId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponApplies");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Orders",
                newName: "CouponName");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Suppliers",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "CouponName",
                table: "Carts",
                nullable: true);
        }
    }
}
