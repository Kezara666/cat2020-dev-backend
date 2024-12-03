using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wb_bal_additional_charge",
                table: "wb_balances");

            migrationBuilder.DropColumn(
                name: "wb_bal_additional_type",
                table: "wb_balances");

            migrationBuilder.DropColumn(
                name: "wb_bal_is_over_pay_taken",
                table: "wb_balances");

            migrationBuilder.DropColumn(
                name: "wb_bal_reading_date",
                table: "wb_balances");

            migrationBuilder.RenameColumn(
                name: "wb_bal_print_arrears",
                table: "wb_balances",
                newName: "wb_bal_print_last_balance");

            migrationBuilder.AddColumn<decimal>(
                name: "wb_wc_running_overpay",
                table: "wb_water_connections",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_water_charge",
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
                name: "wb_bal_vat_rate",
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
                name: "wb_bal_vat_amount",
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

            migrationBuilder.AlterColumn<bool>(
                name: "wb_bal_is_processed",
                table: "wb_balances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "wb_bal_is_filled",
                table: "wb_balances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "wb_bal_is_completed",
                table: "wb_balances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_fix_charge",
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
            migrationBuilder.DropColumn(
                name: "wb_wc_running_overpay",
                table: "wb_water_connections");

            migrationBuilder.RenameColumn(
                name: "wb_bal_print_last_balance",
                table: "wb_balances",
                newName: "wb_bal_print_arrears");

            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_water_charge",
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
                name: "wb_bal_vat_rate",
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
                name: "wb_bal_vat_amount",
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

            migrationBuilder.AlterColumn<bool>(
                name: "wb_bal_is_processed",
                table: "wb_balances",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "wb_bal_is_filled",
                table: "wb_balances",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "wb_bal_is_completed",
                table: "wb_balances",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "wb_bal_fix_charge",
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

            migrationBuilder.AddColumn<decimal>(
                name: "wb_bal_additional_charge",
                table: "wb_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "wb_bal_additional_type",
                table: "wb_balances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "wb_bal_is_over_pay_taken",
                table: "wb_balances",
                type: "tinyint(1)",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "wb_bal_reading_date",
                table: "wb_balances",
                type: "date",
                nullable: true);
        }
    }
}
