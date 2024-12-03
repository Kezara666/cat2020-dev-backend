using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalBalanceIsHoldColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "sr_bal_is_shop_hold",
                table: "sr_shopRental_balance",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sr_shopRental_balance_log",
                columns: table => new
                {
                    sr_bal_lg_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_bal_lg_proprty_id = table.Column<int>(type: "int", nullable: false),
                    sr_bal_lg_shop_id = table.Column<int>(type: "int", nullable: false),
                    sr_bal_lg_year = table.Column<int>(type: "int", nullable: false),
                    sr_bal_lg_month = table.Column<int>(type: "int", nullable: false),
                    sr_bal_lg_from_date = table.Column<DateOnly>(type: "date", nullable: false),
                    sr_bal_lg__to_date = table.Column<DateOnly>(type: "date", nullable: true),
                    sr_bal_lg_bill_process_date = table.Column<DateOnly>(type: "date", nullable: false),
                    sr_bal_previous_arrears_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_new_arrears_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_previous_paid_arrears_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_new_paid_arrears_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_previous_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_new_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_previous_paid_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_new_paid_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_previous_service_charge_arreas_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_new_service_charge_arreas_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_previous_paid_service_charge_arreas_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_new_paid_service_charge_arreas_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_previous_over_payment_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_over_new_payment_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sr_bal_is_processed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'0'"),
                    sr_bal_no_of_payments = table.Column<int>(type: "int", nullable: true),
                    sr_bal_status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'1'"),
                    sr_bal_office_id = table.Column<int>(type: "int", nullable: true),
                    sr_bal_sabha_id = table.Column<int>(type: "int", nullable: true),
                    sr_bal_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_bal_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_bal_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_bal_updated_by = table.Column<int>(type: "int", nullable: true),
                    sr_bal_has_transaction = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    sr_bal_lg_previous_ly_fine = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_ly_fine = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_paid_ly_fine = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_paid_ly_fine = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_paid_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_paid_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_ty_fine = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_ty_fine = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_paid_ty_fine = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_paid_ty_fine = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_ty_arreas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_ty_arreas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_paid_ty_arreas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_paid_ty_arreas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_ly_ty_service_charge_arreas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_ly_ty_service_charge_arreas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_paid_lg_previous_ly_ty_service_charge_arreas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_paid_lg_new_ly_ty_service_charge_arreas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_current_service_charge_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    NewCurrentServiceChargeAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PreviousPaidCurrentServiceChargeAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_lg_new_paid_current_service_charge_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_current_rental_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_current_rental_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_paid_current_rental_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_paid_current_rental_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_precious_current_month_new_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_new_current_month_new_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_lg_previous_paid_current_month_new_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_paid_lg_new_current_month_new_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_settled_mixin_order_id = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_shopRental_balance_log", x => x.sr_bal_lg_id);
                    table.ForeignKey(
                        name: "FK_sr_shopRental_balance_log_sr_property_sr_bal_lg_proprty_id",
                        column: x => x.sr_bal_lg_proprty_id,
                        principalTable: "sr_property",
                        principalColumn: "sr_property_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sr_shopRental_balance_log_sr_shop_sr_bal_lg_shop_id",
                        column: x => x.sr_bal_lg_shop_id,
                        principalTable: "sr_shop",
                        principalColumn: "sr_shop_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_sr_shopRental_balance_log_sr_bal_lg_proprty_id",
                table: "sr_shopRental_balance_log",
                column: "sr_bal_lg_proprty_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_shopRental_balance_log_sr_bal_lg_shop_id_sr_bal_lg_year_s~",
                table: "sr_shopRental_balance_log",
                columns: new[] { "sr_bal_lg_shop_id", "sr_bal_lg_year", "sr_bal_lg_month" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_shopRental_balance_log");

            migrationBuilder.DropColumn(
                name: "sr_bal_is_shop_hold",
                table: "sr_shopRental_balance");
        }
    }
}
