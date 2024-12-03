using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExcessPayment",
                table: "assmt_balances",
                newName: "assmt_bal_excess_payment");

            migrationBuilder.AddColumn<int>(
                name: "assmt_bal_number_of_payments",
                table: "assmt_balances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assmt_bal_histry_number_of_payments",
                table: "assmt_balance_history",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assmt_bal_number_of_payments",
                table: "assmt_balances");

            migrationBuilder.DropColumn(
                name: "assmt_bal_histry_number_of_payments",
                table: "assmt_balance_history");

            migrationBuilder.RenameColumn(
                name: "assmt_bal_excess_payment",
                table: "assmt_balances",
                newName: "ExcessPayment");
        }
    }
}
