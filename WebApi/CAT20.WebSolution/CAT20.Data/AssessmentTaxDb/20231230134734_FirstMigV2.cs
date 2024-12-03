using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "assmt_has_journal_request",
                table: "assmt_assessments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "assessment_journals",
                columns: table => new
                {
                    assmt_doc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_doc_assessment_id = table.Column<int>(type: "int", nullable: true),
                    assmt_jnl_old_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_new_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_old_excess_payment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_new_excess_payment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_old_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_new_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_old_ly_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_new_ly_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_old_ty_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_new_ty_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_new_ty_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_old_ty_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_jnl_change_properties = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    assmt_jnl_draft_approve_reject = table.Column<int>(type: "int", nullable: false),
                    assmt_jnl_request_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    assmt_jnl_request_by = table.Column<int>(type: "int", nullable: false),
                    assmt_jnl_request_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_jnl_action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_jnl_action_by = table.Column<int>(type: "int", nullable: true),
                    assmt_jnl_action_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessment_journals", x => x.assmt_doc_id);
                    table.ForeignKey(
                        name: "FK_assessment_journals_assmt_assessments_assmt_doc_assessment_id",
                        column: x => x.assmt_doc_assessment_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_assessment_journals_assmt_doc_assessment_id",
                table: "assessment_journals",
                column: "assmt_doc_assessment_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assessment_journals");

            migrationBuilder.DropColumn(
                name: "assmt_has_journal_request",
                table: "assmt_assessments");
        }
    }
}
