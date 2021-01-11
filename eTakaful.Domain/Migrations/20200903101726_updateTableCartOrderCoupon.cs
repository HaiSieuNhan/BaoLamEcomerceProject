using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Domain.Migrations
{
    public partial class updateTableCartOrderCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Coupons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Carts",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NotionalPrice",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                table: "Carts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NotionalPrice",
                table: "Carts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponId",
                table: "Orders",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CouponId",
                table: "Carts",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Coupons_CouponId",
                table: "Carts",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Coupons_CouponId",
                table: "Orders",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Coupons_CouponId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Coupons_CouponId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CouponId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CouponId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NotionalPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "NotionalPrice",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Coupons",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Carts",
                newName: "TotalAmount");
        }
    }
}
