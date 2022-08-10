using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestSeminar.Data.Migrations
{
    public partial class fileStorageMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImgUrl",
                table: "ProductViewModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductImgUrl",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImgUrl",
                table: "ProductViewModel");

            migrationBuilder.DropColumn(
                name: "ProductImgUrl",
                table: "Product");
        }
    }
}
