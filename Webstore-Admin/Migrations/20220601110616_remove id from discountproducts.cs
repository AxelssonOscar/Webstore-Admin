using Microsoft.EntityFrameworkCore.Migrations;

namespace Webstore_Admin.Migrations
{
    public partial class removeidfromdiscountproducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "DiscountProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DiscountProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
