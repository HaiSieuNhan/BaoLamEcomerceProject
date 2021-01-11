using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Domain.Migrations
{
    public partial class updateProductCommentTable01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rating",
                table: "ProductComments",
                newName: "Rating");

            migrationBuilder.AddColumn<string>(
                name: "Header",
                table: "ProductComments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProducCommentStatus",
                table: "ProductComments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductCommentImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ImageLink = table.Column<string>(nullable: true),
                    ProductCommentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCommentImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCommentImages_ProductComments_ProductCommentId",
                        column: x => x.ProductCommentId,
                        principalTable: "ProductComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCommentImages_ProductCommentId",
                table: "ProductCommentImages",
                column: "ProductCommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCommentImages");

            migrationBuilder.DropColumn(
                name: "Header",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "ProducCommentStatus",
                table: "ProductComments");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "ProductComments",
                newName: "rating");
        }
    }
}
