using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Domain.Migrations
{
    public partial class updaterenameTableCollection01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CollectionImages",
                table: "CollectionImages");

            migrationBuilder.RenameTable(
                name: "CollectionImages",
                newName: "Collections");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "CollectionImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollectionImages",
                table: "CollectionImages",
                column: "Id");
        }
    }
}
