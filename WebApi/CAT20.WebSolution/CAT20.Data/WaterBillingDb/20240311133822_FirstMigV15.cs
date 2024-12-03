using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wb_balance_history",
                columns: table => new
                {
                    wb_bal_hstry_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_bal_hstry_bal_id = table.Column<int>(type: "int", nullable: false),
                    wb_bal_hstry_wc_primary_id = table.Column<int>(type: "int", nullable: false),
                    wb_bal_hstry_tr_type = table.Column<int>(type: "int", nullable: true),
                    wb_bal_hstry_action_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_bal_hstry_action_by = table.Column<int>(type: "int", nullable: true),
                    wb_bal_hstry_mci_conn_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_hstry_bar_code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_hstry_invoice_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_hstry_year = table.Column<int>(type: "int", nullable: false),
                    wb_bal_hstry_month = table.Column<int>(type: "int", nullable: false),
                    wb_bal_hstry_from_date = table.Column<DateOnly>(type: "date", nullable: false),
                    wb_bal_hstry_to_date = table.Column<DateOnly>(type: "date", nullable: true),
                    wb_bal_hstry_read_by = table.Column<int>(type: "int", nullable: true),
                    wb_bal_hstry_bill_process_date = table.Column<DateOnly>(type: "date", nullable: false),
                    wb_bal_hstry_meter_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_hstry_prev_meter_reading = table.Column<int>(type: "int", nullable: false),
                    wb_bal_hstry_this_month_meter_reading = table.Column<int>(type: "int", nullable: true),
                    wb_bal_hstry_water_charge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_hstry_fix_charge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_hstry_vat_rate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_hstry_vat_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_hstry_this_month_charges = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_hstry_this_month_charges_with_vat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_hstry_total_due = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_hstry_metercondition = table.Column<int>(name: "wb_bal_hstry_meter-condition", type: "int", nullable: true),
                    wb_bal_hstry_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_hstry_ontime_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_hstry_late_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_hstry_over_pay = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_hstry_payments = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_hstry_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    wb_bal_hstry_is_filled = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    wb_bal_hstry_is_processed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    wb_bal_hstry_no_of_payments = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    wb_bal_hstry_no_of_cancels = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    wb_bal_hstry_print_last_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    wb_bal_hstry_cal_string = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_hstry_print_last_bill_year_month = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_hstry_print_billing_details = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_hstry_print_balance_b_f = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    wb_bal_hstry_print_last_month_payments = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    wb_bal_hstry_num_print = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_balance_history", x => x.wb_bal_hstry_id);
                    table.ForeignKey(
                        name: "fk_bal_hstry_wc_id",
                        column: x => x.wb_bal_hstry_wc_primary_id,
                        principalTable: "wb_water_connections",
                        principalColumn: "wb_wc_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_wb_balance_history_wb_bal_hstry_wc_primary_id",
                table: "wb_balance_history",
                column: "wb_bal_hstry_wc_primary_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wb_balance_history");
        }
    }
}
