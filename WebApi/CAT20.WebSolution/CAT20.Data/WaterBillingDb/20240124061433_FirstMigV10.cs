using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "wb_bal_this_month_charges_with_vat",
                table: "wb_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wb_bal_this_month_charges_with_vat",
                table: "wb_balances");
        }
    }
}
