using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestSeminar.Data.Migrations
{
    public partial class productCategoryaddedtoProductMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryId",
                table: "Product",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryId",
                table: "Product",
                column: "ProductCategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductCategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "Product");
        }
    }
}
