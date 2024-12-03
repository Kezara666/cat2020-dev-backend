using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalProcessTableUpdatedWithNewFieldsMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sr_process_congig_id",
                table: "shopRental_processes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sr_process_day",
                table: "shopRental_processes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_process_month",
                table: "shopRental_processes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_process_congig_id",
                table: "shopRental_processes");

            migrationBuilder.DropColumn(
                name: "sr_process_day",
                table: "shopRental_processes");

            migrationBuilder.DropColumn(
                name: "sr_process_month",
                table: "shopRental_processes");
        }
    }
}
