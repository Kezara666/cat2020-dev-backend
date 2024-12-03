using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalProcessTableModifiedWithDateFieldMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_process_month",
                table: "shopRental_processes");

            migrationBuilder.DropColumn(
                name: "sr_process_year",
                table: "shopRental_processes");

            migrationBuilder.AddColumn<DateOnly>(
                name: "sr_process_date",
                table: "shopRental_processes",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_process_date",
                table: "shopRental_processes");

            migrationBuilder.AddColumn<int>(
                name: "sr_process_month",
                table: "shopRental_processes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_process_year",
                table: "shopRental_processes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
