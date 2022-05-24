using Microsoft.EntityFrameworkCore.Migrations;

namespace Webstore_Admin.Migrations
{
    public partial class EditSeedDataProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "UnitsInStock" },
                values: new object[,]
                {
                    { 18, 1, "Jonagold", 1.95m, 50 },
                    { 19, 1, "Golden Delicious", 2.95m, 50 },
                    { 20, 1, "Royal Gala EKO", 8.95m, 40 },
                    { 21, 1, "Discovery", 3.95m, 50 },
                    { 22, 1, "Ingrid Marie", 3.95m, 5 },
                    { 23, 1, "Rubinstar", 4.95m, 12 },
                    { 24, 1, "Cox Orange", 4.95m, 60 },
                    { 25, 1, "Äppelkorg", 34.95m, 8 },
                    { 26, 1, "Äppelpåse 1Kg", 19.95m, 20 },
                    { 27, 1, "Honey Crunch", 12.95m, 33 },
                    { 28, 1, "Granny Smith EKO", 7.95m, 12 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);
        }
    }
}
