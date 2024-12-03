using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vt_classification",
                columns: table => new
                {
                    vt_clsf_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_clsf_description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_clsf_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_clsf_status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'1'"),
                    vt_clsf_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    vt_clsf_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.vt_clsf_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_mainLedgerAccount",
                columns: table => new
                {
                    vt_mla_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_mla_description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_mla_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_mla_status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'1'"),
                    ClassificationId = table.Column<int>(type: "int", nullable: false),
                    vt_mla_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    vt_mla_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.vt_mla_id);
                    table.ForeignKey(
                        name: "FK_vt_mainLedgerAccount_vt_classification_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "vt_classification",
                        principalColumn: "vt_clsf_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_vt_mainLedgerAccount_ClassificationId",
                table: "vt_mainLedgerAccount",
                column: "ClassificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vt_mainLedgerAccount");

            migrationBuilder.DropTable(
                name: "vt_classification");
        }
    }
}
