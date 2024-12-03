using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThisYearArrears",
                table: "wb_balances",
                newName: "wb_bal_ty_arrears");

            migrationBuilder.RenameColumn(
                name: "OverPayment",
                table: "wb_balances",
                newName: "wb_bal_rpt_overpay");

            migrationBuilder.RenameColumn(
                name: "LastYearArrears",
                table: "wb_balances",
                newName: "wb_bal_ly_arrears");

            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_ty_arrears",
                table: "wb_balances",
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
                name: "wb_bal_rpt_overpay",
                table: "wb_balances",
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
                name: "wb_bal_ly_arrears",
                table: "wb_balances",
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
                name: "wb_bal_ty_arrears",
                table: "wb_balances",
                newName: "ThisYearArrears");

            migrationBuilder.RenameColumn(
                name: "wb_bal_rpt_overpay",
                table: "wb_balances",
                newName: "OverPayment");

            migrationBuilder.RenameColumn(
                name: "wb_bal_ly_arrears",
                table: "wb_balances",
                newName: "LastYearArrears");

            migrationBuilder.AlterColumn<decimal>(
                name: "ThisYearArrears",
                table: "wb_balances",
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
                name: "OverPayment",
                table: "wb_balances",
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
                name: "LastYearArrears",
                table: "wb_balances",
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
