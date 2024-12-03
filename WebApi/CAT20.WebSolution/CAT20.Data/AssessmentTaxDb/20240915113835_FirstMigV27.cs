using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "assmt_bal_hstry_session_date",
                table: "assmt_balance_history",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "assmt_bal_hstry_transaction_type",
                table: "assmt_balance_history",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "derived_assmt_id",
                table: "assmt_amalgamation_subdivision",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "requested_action",
                table: "assmt_amalgamation_subdivision",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "assmt_amalgamation_subdivision",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "assmt_quarter_report_logs",
                columns: table => new
                {
                    assmt_qrt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_tr_date_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_qrt_year = table.Column<int>(type: "int", nullable: false),
                    assmt_qrt_quater_no = table.Column<int>(type: "int", nullable: false),
                    assmt_qrt_annual_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    assmt_qrt_q_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_q_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_q_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_m1_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_m2_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_m3_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_ly_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_ty_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_ty_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_running_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_assmt_id = table.Column<int>(type: "int", nullable: true),
                    assmt_used_tr_type = table.Column<int>(type: "int", nullable: false),
                    assmt_qrt_session_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_qrt_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_qrt_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_qrt_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_qrt_updated_by = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_quarter_report_logs", x => x.assmt_qrt_id);
                    table.ForeignKey(
                        name: "fk_assmt_q_rpt_log_id",
                        column: x => x.assmt_qrt_assmt_id,
                        principalTable: "assmt_quarter_report",
                        principalColumn: "assmt_qrt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sub_division",
                columns: table => new
                {
                    sub_division_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    subdivision_amalgamation_id = table.Column<int>(type: "int", nullable: true),
                    k_form_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_division", x => x.sub_division_id);
                    table.ForeignKey(
                        name: "fk_subdivision_amalgamation_id",
                        column: x => x.subdivision_amalgamation_id,
                        principalTable: "assmt_amalgamation_subdivision",
                        principalColumn: "assmt_amalgamation_assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_quarter_report_logs_assmt_qrt_assmt_id",
                table: "assmt_quarter_report_logs",
                column: "assmt_qrt_assmt_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_division_subdivision_amalgamation_id",
                table: "sub_division",
                column: "subdivision_amalgamation_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assmt_quarter_report_logs");

            migrationBuilder.DropTable(
                name: "sub_division");

            migrationBuilder.DropColumn(
                name: "assmt_bal_hstry_session_date",
                table: "assmt_balance_history");

            migrationBuilder.DropColumn(
                name: "assmt_bal_hstry_transaction_type",
                table: "assmt_balance_history");

            migrationBuilder.DropColumn(
                name: "derived_assmt_id",
                table: "assmt_amalgamation_subdivision");

            migrationBuilder.DropColumn(
                name: "requested_action",
                table: "assmt_amalgamation_subdivision");

            migrationBuilder.DropColumn(
                name: "type",
                table: "assmt_amalgamation_subdivision");
        }
    }
}
