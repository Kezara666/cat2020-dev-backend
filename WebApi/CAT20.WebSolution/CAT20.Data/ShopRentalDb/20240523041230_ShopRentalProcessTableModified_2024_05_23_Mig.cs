using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalProcessTableModified_2024_05_23_Mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sr_process_day",
                table: "shopRental_processes",
                newName: "sr_process_last_session_month");

            migrationBuilder.AddColumn<int>(
                name: "sr_process_last_session_day",
                table: "shopRental_processes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_process_last_session_day",
                table: "shopRental_processes");

            migrationBuilder.RenameColumn(
                name: "sr_process_last_session_month",
                table: "shopRental_processes",
                newName: "sr_process_day");
        }
    }
}
