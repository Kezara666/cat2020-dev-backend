using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_rpt_tm_vat",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_rpt_tm_chrage",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "wb_bal_monthly_bill_vat",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "wb_bal_rpt_remain_overpay",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "wb_bal_rpt_remain_overpay_vat",
                table: "wb_balance_month_end_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wb_bal_monthly_bill_vat",
                table: "wb_balance_month_end_report");

            migrationBuilder.DropColumn(
                name: "wb_bal_rpt_remain_overpay",
                table: "wb_balance_month_end_report");

            migrationBuilder.DropColumn(
                name: "wb_bal_rpt_remain_overpay_vat",
                table: "wb_balance_month_end_report");

            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_rpt_tm_vat",
                table: "wb_balance_month_end_report",
                type: "decimal(65,30)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_rpt_tm_chrage",
                table: "wb_balance_month_end_report",
                type: "decimal(65,30)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true,
                oldDefaultValue: 0m);
        }
    }
}
