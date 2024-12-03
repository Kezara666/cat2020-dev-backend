using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "assmt_bal_rpt_balance",
                table: "assmt_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "assmt_quarter_report",
                columns: table => new
                {
                    assmt_qrt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_tr_date_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_qrt_year = table.Column<int>(type: "int", nullable: false),
                    assmt_qrt_quater_no = table.Column<int>(type: "int", nullable: false),
                    assmt_qrt_q_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_m1_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_m2_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_m3_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_ly_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_ty_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_ty_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qrt_assmt_id = table.Column<int>(type: "int", nullable: true),
                    assmt_qrt_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_qrt_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_qrt_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_qrt_updated_by = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_quarter_report", x => x.assmt_qrt_id);
                    table.ForeignKey(
                        name: "fk_assmt_q_rpt_id",
                        column: x => x.assmt_qrt_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_quarter_report_assmt_qrt_assmt_id",
                table: "assmt_quarter_report",
                column: "assmt_qrt_assmt_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assmt_quarter_report");

            migrationBuilder.DropColumn(
                name: "assmt_bal_rpt_balance",
                table: "assmt_balances");
        }
    }
}
