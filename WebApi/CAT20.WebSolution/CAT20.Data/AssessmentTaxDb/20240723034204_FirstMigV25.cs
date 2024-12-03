using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "assmt_has_bil_adj_request",
                table: "assmt_assessments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "assessment_bill_adjustment",
                columns: table => new
                {
                    assmt_bil_adj_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_bil_adj_assessment_id = table.Column<int>(type: "int", nullable: false),
                    assmt_bil_adj_mix_order_id = table.Column<int>(type: "int", nullable: false),
                    assmt_bil_adj_request_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    assmt_bil_adj_request_by = table.Column<int>(type: "int", nullable: false),
                    assmt_bil_adj_request_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_bil_adj_araft_approve_reject_withdraw = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    assmtbil_adj_action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_bil_adj_action_by = table.Column<int>(type: "int", nullable: true),
                    assmt_bil_adj_action_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessment_bill_adjustment", x => x.assmt_bil_adj_id);
                    table.ForeignKey(
                        name: "fk_bill_adj_assmt_id",
                        column: x => x.assmt_bil_adj_assessment_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_assessment_bill_adjustment_assmt_bil_adj_assessment_id",
                table: "assessment_bill_adjustment",
                column: "assmt_bil_adj_assessment_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assessment_bill_adjustment");

            migrationBuilder.DropColumn(
                name: "assmt_has_bil_adj_request",
                table: "assmt_assessments");
        }
    }
}
