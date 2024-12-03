using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class DailyFineProcessLogNewColumnNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sr_dailyfineprocesslog_balance_id",
                table: "shopRental_dailyfineprocesslog",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_dailyfineprocesslog_balance_id",
                table: "shopRental_dailyfineprocesslog");
        }
    }
}
