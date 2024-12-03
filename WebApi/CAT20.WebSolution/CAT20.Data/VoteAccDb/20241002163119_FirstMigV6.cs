using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigV6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vt_balsheet_subledger_account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_balsheet_subledger_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_balsheet_subledger_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_balsheet_subledger_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_balsheet_subledger_code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_balsheet_subledger_balsheet_ledger_account_id = table.Column<int>(type: "int", nullable: true),
                    vt_balsheet_subledger_coa_version_id = table.Column<int>(type: "int", nullable: false),
                    vt_balsheet_subledger_status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_balsheet_subledger_account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vt_balsheet_subledger_account_vt_balsheet_subtitle_vt_balshe~",
                        column: x => x.vt_balsheet_subledger_balsheet_ledger_account_id,
                        principalTable: "vt_balsheet_subtitle",
                        principalColumn: "vt_balsheet_subtitle_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_inc_exp_subledger_account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_inc_exp_subledger_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_exp_subledger_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_exp_subledger_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_exp_subledger_code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_exp_subledger_ledger_account_id = table.Column<int>(type: "int", nullable: true),
                    vt_inc_exp_subledger_coa_version_id = table.Column<int>(type: "int", nullable: false),
                    vt_inc_exp_subledger_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_inc_exp_subledger_account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vt_inc_exp_subledger_account_vt_inc_subtitle_vt_inc_exp_subl~",
                        column: x => x.vt_inc_exp_subledger_ledger_account_id,
                        principalTable: "vt_inc_subtitle",
                        principalColumn: "vt_inc_subtitle_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_vt_balsheet_subledger_account_vt_balsheet_subledger_balsheet~",
                table: "vt_balsheet_subledger_account",
                column: "vt_balsheet_subledger_balsheet_ledger_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_inc_exp_subledger_account_vt_inc_exp_subledger_ledger_acc~",
                table: "vt_inc_exp_subledger_account",
                column: "vt_inc_exp_subledger_ledger_account_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vt_balsheet_subledger_account");

            migrationBuilder.DropTable(
                name: "vt_inc_exp_subledger_account");
        }
    }
}
