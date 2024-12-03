using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class UnnecessaryBalanceReportFieldRemovedFromBalancesTableMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_bal_ty_ly_overpayment",
                table: "sr_shopRental_balance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_ty_ly_overpayment",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");
        }
    }
}
