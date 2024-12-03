using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "assmt_has_assets_cng_request",
                table: "assmt_assessments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "assessment_assets_change",
                columns: table => new
                {
                    assmt_assets_cng_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_assets_cng_assessment_id = table.Column<int>(type: "int", nullable: true),
                    assmt_assets_cng_old_asmt_number = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_assets_cng_new_asmt_number = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_assets_cng_old_name = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_assets_cng_new_name = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_assets_cng_old_address_1 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_assets_cng_new_address_1 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_assets_cng_change_properties = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_assets_cng_draft_approve_reject = table.Column<int>(type: "int", nullable: false),
                    assmt_assets_cng_request_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    assmt_assets_cng_request_by = table.Column<int>(type: "int", nullable: false),
                    assmt_assets_cng_request_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_assets_cng_action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_assets_cng_action_by = table.Column<int>(type: "int", nullable: true),
                    assmt_assets_cng_action_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessment_assets_change", x => x.assmt_assets_cng_id);
                    table.ForeignKey(
                        name: "FK_assessment_assets_change_assmt_assessments_assmt_assets_cng_~",
                        column: x => x.assmt_assets_cng_assessment_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_assessment_assets_change_assmt_assets_cng_assessment_id",
                table: "assessment_assets_change",
                column: "assmt_assets_cng_assessment_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assessment_assets_change");

            migrationBuilder.DropColumn(
                name: "assmt_has_assets_cng_request",
                table: "assmt_assessments");
        }
    }
}
