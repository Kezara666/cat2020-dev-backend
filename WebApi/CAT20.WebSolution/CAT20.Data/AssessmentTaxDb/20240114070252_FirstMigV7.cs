using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "assmt_tr_rn_discount_rate",
                table: "assmt_transactions",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "assmt_bal_number_of_cancels",
                table: "assmt_balances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assmt_bal_histry_number_of_cancels",
                table: "assmt_balance_history",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assmt_tr_rn_discount_rate",
                table: "assmt_transactions");

            migrationBuilder.DropColumn(
                name: "assmt_bal_number_of_cancels",
                table: "assmt_balances");

            migrationBuilder.DropColumn(
                name: "assmt_bal_histry_number_of_cancels",
                table: "assmt_balance_history");
        }
    }
}
