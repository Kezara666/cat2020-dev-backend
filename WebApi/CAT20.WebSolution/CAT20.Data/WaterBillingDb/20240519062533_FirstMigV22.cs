using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceivedOverPayVAT",
                table: "wb_balance_month_end_report",
                newName: "wb_bal_rpt_received_overpay_vat");

            migrationBuilder.RenameColumn(
                name: "ReceivedOverPay",
                table: "wb_balance_month_end_report",
                newName: "wb_bal_rpt_received_overpay");

            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_rpt_received_overpay_vat",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_rpt_received_overpay",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "wb_bal_rpt_received_overpay_vat",
                table: "wb_balance_month_end_report",
                newName: "ReceivedOverPayVAT");

            migrationBuilder.RenameColumn(
                name: "wb_bal_rpt_received_overpay",
                table: "wb_balance_month_end_report",
                newName: "ReceivedOverPay");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReceivedOverPayVAT",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReceivedOverPay",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true,
                oldDefaultValue: 0m);
        }
    }
}
