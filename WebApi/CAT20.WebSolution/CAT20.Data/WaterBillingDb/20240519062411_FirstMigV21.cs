using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ReceivedOverPay",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReceivedOverPayVAT",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedOverPay",
                table: "wb_balance_month_end_report");

            migrationBuilder.DropColumn(
                name: "ReceivedOverPayVAT",
                table: "wb_balance_month_end_report");
        }
    }
}
