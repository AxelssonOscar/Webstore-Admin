using Microsoft.EntityFrameworkCore.Migrations;

namespace Webstore_Admin.Migrations
{
    public partial class SeedProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "UnitsInStock" },
                values: new object[,]
                {
                    { 1, 1, "Royal Gala", 4.95m, 100 },
                    { 2, 1, "Pink Lady", 3.95m, 100 },
                    { 3, 1, "Granny Smith", 5.95m, 100 },
                    { 4, 1, "Red Delicious", 2.95m, 100 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
