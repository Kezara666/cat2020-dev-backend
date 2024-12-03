using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assessment_journals_assmt_assessments_assmt_doc_assessment_id",
                table: "assessment_journals");

            migrationBuilder.RenameColumn(
                name: "assmt_doc_assessment_id",
                table: "assessment_journals",
                newName: "assmt_jnl_assessment_id");

            migrationBuilder.RenameColumn(
                name: "assmt_doc_id",
                table: "assessment_journals",
                newName: "assmt_jnl_id");

            migrationBuilder.RenameIndex(
                name: "IX_assessment_journals_assmt_doc_assessment_id",
                table: "assessment_journals",
                newName: "IX_assessment_journals_assmt_jnl_assessment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_assessment_journals_assmt_assessments_assmt_jnl_assessment_id",
                table: "assessment_journals",
                column: "assmt_jnl_assessment_id",
                principalTable: "assmt_assessments",
                principalColumn: "assmt_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assessment_journals_assmt_assessments_assmt_jnl_assessment_id",
                table: "assessment_journals");

            migrationBuilder.RenameColumn(
                name: "assmt_jnl_assessment_id",
                table: "assessment_journals",
                newName: "assmt_doc_assessment_id");

            migrationBuilder.RenameColumn(
                name: "assmt_jnl_id",
                table: "assessment_journals",
                newName: "assmt_doc_id");

            migrationBuilder.RenameIndex(
                name: "IX_assessment_journals_assmt_jnl_assessment_id",
                table: "assessment_journals",
                newName: "IX_assessment_journals_assmt_doc_assessment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_assessment_journals_assmt_assessments_assmt_doc_assessment_id",
                table: "assessment_journals",
                column: "assmt_doc_assessment_id",
                principalTable: "assmt_assessments",
                principalColumn: "assmt_id");
        }
    }
}
