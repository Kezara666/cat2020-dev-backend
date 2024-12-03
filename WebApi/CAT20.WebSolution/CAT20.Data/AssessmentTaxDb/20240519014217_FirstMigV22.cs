using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "assmt_tmp_ptnr_email",
                table: "assm_temp_partners",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assmt_tmp_ptnr_email",
                table: "assm_temp_partners");
        }
    }
}
