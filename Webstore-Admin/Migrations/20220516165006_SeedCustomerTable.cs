using Microsoft.EntityFrameworkCore.Migrations;

namespace Webstore_Admin.Migrations
{
    public partial class SeedCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "City", "Email", "Name", "PhoneNumber", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Kneippbyn 15", "Visby", "pippi@langstrump.se", "Pippi Långstrump", "0701112233", "622 61" },
                    { 2, "Rörstrandsgatan 25", "Stockholm", "karlsson@taket.se", "Karlsson på taket", "0704445566", "113 41" },
                    { 3, "Katthult", "Katthult", "emil@svensson.se", "Emil Svensson", "0707778899", "598 92" },
                    { 4, "Stränge 16", "Mellerud", "ronja@rovardotter.se", "Ronja Rövardotter", "0701234567", "464 82" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
