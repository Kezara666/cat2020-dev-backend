using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class add_agents_model_jfx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_agents",
                columns: table => new
                {
                    cd_agent_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_agent_branch_id = table.Column<int>(type: "int", nullable: true),
                    cd_agent_row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    cd_agent_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_agent_bank_account = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_agent_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    cd_agent_create_by = table.Column<int>(type: "int", nullable: false),
                    cd_agent_reference = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_agent_sabha_id = table.Column<int>(type: "int", nullable: false),
                    cd_agent_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    cd_agent_system_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    cd_agent_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    cd_agent_update_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cd_agent_id);
                    table.ForeignKey(
                        name: "cd_agent_bank_branch_id",
                        column: x => x.cd_agent_branch_id,
                        principalTable: "cd_bank_branches",
                        principalColumn: "cd_bb_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_cd_agents_cd_agent_branch_id",
                table: "cd_agents",
                column: "cd_agent_branch_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_agents");
        }
    }
}
