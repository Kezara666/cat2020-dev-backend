using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class Mig2024_07_07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_emp_loan_for_voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_id = table.Column<int>(type: "int", nullable: true),
                    LoanId = table.Column<int>(type: "int", nullable: true),
                    installment_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    interest_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_emp_loan_for_voucher_vt_lgr_pmnt_voucher_voucher~",
                        column: x => x.voucher_id,
                        principalTable: "vt_lgr_pmnt_voucher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_emp_loan_for_voucher_voucher_id",
                table: "vt_lgr_pmnt_emp_loan_for_voucher",
                column: "voucher_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_emp_loan_for_voucher");
        }
    }
}
