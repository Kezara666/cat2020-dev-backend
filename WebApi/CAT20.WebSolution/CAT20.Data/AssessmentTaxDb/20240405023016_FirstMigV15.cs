using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "assmt_jnl_new_prop_type",
                table: "assessment_journals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_jnl_old_prop_type",
                table: "assessment_journals",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assmt_jnl_new_prop_type",
                table: "assessment_journals");

            migrationBuilder.DropColumn(
                name: "assmt_jnl_old_prop_type",
                table: "assessment_journals");
        }
    }
}
