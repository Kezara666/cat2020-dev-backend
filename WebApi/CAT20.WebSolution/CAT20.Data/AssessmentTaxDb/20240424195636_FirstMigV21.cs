using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_assmt_quarter_report_assmt_qrt_year_assmt_qrt_quater_no_assm~",
                table: "assmt_quarter_report",
                columns: new[] { "assmt_qrt_year", "assmt_qrt_quater_no", "assmt_qrt_assmt_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_assmt_quarter_report_assmt_qrt_year_assmt_qrt_quater_no_assm~",
                table: "assmt_quarter_report");
        }
    }
}
