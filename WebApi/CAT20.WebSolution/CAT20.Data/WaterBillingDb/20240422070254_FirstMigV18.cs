using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wb_balance_month_end_report",
                columns: table => new
                {
                    wb_bal_rpt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_bal_rpt_wcon_id = table.Column<int>(type: "int", nullable: false),
                    wb_bal_rpt_year = table.Column<int>(type: "int", nullable: false),
                    wb_bal_rpt_month = table.Column<int>(type: "int", nullable: false),
                    wb_bal_rpt_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_rpt_lya_vat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_rpt_ty_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_rpt_tya_vat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_rpt_tm_chrage = table.Column<decimal>(type: "decimal(65,30)", nullable: true, defaultValue: 0m),
                    wb_bal_rpt_tm_vat = table.Column<decimal>(type: "decimal(65,30)", nullable: true, defaultValue: 0m),
                    wb_bal_rpt_overpay_vat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_rpt_ly_paying = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_rpt_ty_paying = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_rpt_tm_paying = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_rpt_over_paying = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    wb_bal_rpt_status = table.Column<int>(type: "int", nullable: true),
                    wb_bal_rpt_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_bal_rpt_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_bal_rpt_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_bal_rpt_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_balance_month_end_report", x => x.wb_bal_rpt_id);
                    table.ForeignKey(
                        name: "fk_bal_rpt_wc_id",
                        column: x => x.wb_bal_rpt_wcon_id,
                        principalTable: "wb_water_connections",
                        principalColumn: "wb_wc_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_wb_balance_month_end_report_wb_bal_rpt_wcon_id",
                table: "wb_balance_month_end_report",
                column: "wb_bal_rpt_wcon_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wb_balance_month_end_report");
        }
    }
}
