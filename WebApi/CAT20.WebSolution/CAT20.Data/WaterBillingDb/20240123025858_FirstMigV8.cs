using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "wb_bal_is_over_pay_taken",
                table: "wb_balances",
                type: "tinyint(1)",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "wb_bal_over_pay",
                table: "wb_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wb_bal_is_over_pay_taken",
                table: "wb_balances");

            migrationBuilder.DropColumn(
                name: "wb_bal_over_pay",
                table: "wb_balances");
        }
    }
}
