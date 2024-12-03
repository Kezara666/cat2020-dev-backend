using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalBalanceReportFieldsAddedMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_current_rental_amount",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_current_service_charge_amount",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_ly_arreas",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_paid_current_rental_amount",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_paid_current_service_charge_amount",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_paid_ly_arreas",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_paid_ly_fine",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_paid_ty_arreas",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_paid_ty_fine",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_ty_arreas",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_ty_fine",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AlterColumn<decimal>(
                name: "sr_shop_service_charge",
                table: "sr_shop",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "sr_shop_security_deposit",
                table: "sr_shop",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "sr_shop_rental",
                table: "sr_shop",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "sr_shop_key_money",
                table: "sr_shop",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_bal_current_rental_amount",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_current_service_charge_amount",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_ly_arreas",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_paid_current_rental_amount",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_paid_current_service_charge_amount",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_paid_ly_arreas",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_paid_ly_fine",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_paid_ty_arreas",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_paid_ty_fine",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_ty_arreas",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_ty_fine",
                table: "sr_shopRental_balance");

            migrationBuilder.AlterColumn<decimal>(
                name: "sr_shop_service_charge",
                table: "sr_shop",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true,
                oldDefaultValueSql: "'0'");

            migrationBuilder.AlterColumn<decimal>(
                name: "sr_shop_security_deposit",
                table: "sr_shop",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true,
                oldDefaultValueSql: "'0'");

            migrationBuilder.AlterColumn<decimal>(
                name: "sr_shop_rental",
                table: "sr_shop",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true,
                oldDefaultValueSql: "'0'");

            migrationBuilder.AlterColumn<decimal>(
                name: "sr_shop_key_money",
                table: "sr_shop",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true,
                oldDefaultValueSql: "'0'");
        }
    }
}
