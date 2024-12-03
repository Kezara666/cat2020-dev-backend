using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalBalanceReportFieldLYFineFieldRenamedMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_ly_fine",
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
                name: "sr_bal_ly_fine",
                table: "sr_shopRental_balance");
        }
    }
}
