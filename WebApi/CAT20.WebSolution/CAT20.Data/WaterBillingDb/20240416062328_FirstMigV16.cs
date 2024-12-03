using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LastYearArrears",
                table: "wb_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OverPayment",
                table: "wb_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ThisYearArrears",
                table: "wb_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "wb_bal_hstry_action_at",
                table: "wb_balance_history",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastYearArrears",
                table: "wb_balances");

            migrationBuilder.DropColumn(
                name: "OverPayment",
                table: "wb_balances");

            migrationBuilder.DropColumn(
                name: "ThisYearArrears",
                table: "wb_balances");

            migrationBuilder.AlterColumn<DateTime>(
                name: "wb_bal_hstry_action_at",
                table: "wb_balance_history",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
