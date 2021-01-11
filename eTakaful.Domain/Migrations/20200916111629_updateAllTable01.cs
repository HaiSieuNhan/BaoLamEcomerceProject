using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Domain.Migrations
{
    public partial class updateAllTable01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_ProductSizes_ProductSizeId",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionImages_Categories_CategoryId",
                table: "CollectionImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Categories_CategoryId",
                table: "Coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ProductSizes_ProductSizeId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Products_ProductId",
                table: "ProductSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStatus_Products_ProductId",
                table: "ProductStatus");

            migrationBuilder.DropIndex(
                name: "IX_ProductStatus_ProductId",
                table: "ProductStatus");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_ProductId",
                table: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_CategoryId",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_CollectionImages_CategoryId",
                table: "CollectionImages");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductStatus");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductStatus");

            migrationBuilder.DropColumn(
                name: "CountStock",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CollectionImages");

            migrationBuilder.DropColumn(
                name: "IsCollection",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ProductSizeId",
                table: "OrderDetails",
                newName: "ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductSizeId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductAttributeId");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "CollectionImages",
                newName: "URLImage");

            migrationBuilder.RenameColumn(
                name: "ProductSizeId",
                table: "CartDetails",
                newName: "ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_ProductSizeId",
                table: "CartDetails",
                newName: "IX_CartDetails_ProductAttributeId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductSizes",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CollectionId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductStatusId",
                table: "Products",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Coupons",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "CollectionId",
                table: "Coupons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CollectionImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CollectionImages",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ProductSizeId = table.Column<Guid>(nullable: true),
                    ProductColorId = table.Column<Guid>(nullable: true),
                    CountStock = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_ProductAttributes_ProductAttributeId",
                table: "CartDetails",
                column: "ProductAttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ProductAttributes_ProductAttributeId",
                table: "OrderDetails",
                column: "ProductAttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_ProductAttributes_ProductAttributeId",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ProductAttributes_ProductAttributeId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductStatusId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CollectionImages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CollectionImages");

            migrationBuilder.RenameColumn(
                name: "ProductAttributeId",
                table: "OrderDetails",
                newName: "ProductSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductAttributeId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductSizeId");

            migrationBuilder.RenameColumn(
                name: "URLImage",
                table: "CollectionImages",
                newName: "URL");

            migrationBuilder.RenameColumn(
                name: "ProductAttributeId",
                table: "CartDetails",
                newName: "ProductSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_ProductAttributeId",
                table: "CartDetails",
                newName: "IX_CartDetails_ProductSizeId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductStatus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductStatus",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "CountStock",
                table: "ProductSizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProductSizes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductSizes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "ProductSizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Coupons",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "CollectionImages",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsCollection",
                table: "Categories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStatus_ProductId",
                table: "ProductStatus",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_ProductId",
                table: "ProductSizes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CategoryId",
                table: "Coupons",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionImages_CategoryId",
                table: "CollectionImages",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_ProductSizes_ProductSizeId",
                table: "CartDetails",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionImages_Categories_CategoryId",
                table: "CollectionImages",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Categories_CategoryId",
                table: "Coupons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ProductSizes_ProductSizeId",
                table: "OrderDetails",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Products_ProductId",
                table: "ProductSizes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStatus_Products_ProductId",
                table: "ProductStatus",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
