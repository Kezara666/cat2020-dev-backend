using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalBalancesReportFieldUpdatedToAccordingToFineProcessMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_current_month_new_fine_amount",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_paid_current_month_new_fine_amount",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_bal_current_month_new_fine_amount",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_paid_current_month_new_fine_amount",
                table: "sr_shopRental_balance");
        }
    }
}
