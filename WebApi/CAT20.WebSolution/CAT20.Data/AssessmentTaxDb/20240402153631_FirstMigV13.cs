using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "assmt_new_allocation_requests",
                columns: table => new
                {
                    assmt_new_allocation_req_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_new_allocation_req_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_new_allocation_req_description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_new_allocation_req_assmt_id = table.Column<int>(type: "int", nullable: false),
                    assmt_new_allocation_req_status = table.Column<int>(type: "int(11)", nullable: true),
                    assmt_new_allocation_req_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_new_allocation_req_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_new_allocation_req_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_new_allocation_req_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_new_allocation_requests", x => x.assmt_new_allocation_req_id);
                    table.ForeignKey(
                        name: "FK_assmt_new_allocation_requests_assmt_assessments_assmt_new_al~",
                        column: x => x.assmt_new_allocation_req_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_new_allocation_requests_assmt_new_allocation_req_assmt~",
                table: "assmt_new_allocation_requests",
                column: "assmt_new_allocation_req_assmt_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assmt_new_allocation_requests");
        }
    }
}
