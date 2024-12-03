using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "assessment_atd",
                columns: table => new
                {
                    assmt_atd_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_atd_assessment_id = table.Column<int>(type: "int", nullable: true),
                    assmt_atd_request_status = table.Column<int>(type: "int", nullable: false),
                    assmt_atd_request_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    assmt_atd_request_by = table.Column<int>(type: "int", nullable: false),
                    assmt_atd_request_note = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_atd_action_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_atd_action_by = table.Column<int>(type: "int", nullable: true),
                    assmt_atd_action_note = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_atd_office_id = table.Column<int>(type: "int", nullable: true),
                    assmt_atd_sabha_id = table.Column<int>(type: "int", nullable: true),
                    assmt_atd_status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessment_atd", x => x.assmt_atd_id);
                    table.ForeignKey(
                        name: "FK_assessment_atd_assmt_assessments_assmt_atd_assessment_id",
                        column: x => x.assmt_atd_assessment_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assessment_atd_ownerslog",
                columns: table => new
                {
                    atd_ownrlog_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    atd_ownrlog_assmt_atd_id = table.Column<int>(type: "int", nullable: false),
                    atd_ownrlog_partner_id = table.Column<int>(type: "int", nullable: true),
                    OwnerNIC = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    atd_ownrlog_owner_full_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    atd_ownrlog_owner_address_line_1 = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    atd_ownrlog_owner_address_line_2 = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    atd_ownrlog_type_of_owner = table.Column<int>(type: "int", nullable: false),
                    atd_ownrlog_status_of_owner = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessment_atd_ownerslog", x => x.atd_ownrlog_id);
                    table.ForeignKey(
                        name: "FK_assessment_atd_ownerslog_assessment_atd_atd_ownrlog_assmt_at~",
                        column: x => x.atd_ownrlog_assmt_atd_id,
                        principalTable: "assessment_atd",
                        principalColumn: "assmt_atd_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_assessment_atd_assmt_atd_assessment_id",
                table: "assessment_atd",
                column: "assmt_atd_assessment_id");

            migrationBuilder.CreateIndex(
                name: "IX_assessment_atd_ownerslog_atd_ownrlog_assmt_atd_id",
                table: "assessment_atd_ownerslog",
                column: "atd_ownrlog_assmt_atd_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assessment_atd_ownerslog");

            migrationBuilder.DropTable(
                name: "assessment_atd");
        }
    }
}
