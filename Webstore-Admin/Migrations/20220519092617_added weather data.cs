using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Webstore_Admin.Migrations
{
    public partial class addedweatherdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderCreated",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "WeatherType",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WindSpeed",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCreated",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WeatherType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WindSpeed",
                table: "Orders");
        }
    }
}
