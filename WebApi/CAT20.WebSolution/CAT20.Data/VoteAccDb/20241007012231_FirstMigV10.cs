using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigV10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "vt_custom_vote_balance",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(6)",
                oldRowVersion: true,
                oldNullable: true)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.CreateTable(
                name: "vt_obl_single_open",
                columns: table => new
                {
                    single_bal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    single_bal_ledger_account_id = table.Column<int>(type: "int", nullable: false),
                    single_bal_custom_vote_id = table.Column<int>(type: "int", nullable: false),
                    single_bal_description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    single_bal_office_id = table.Column<int>(type: "int", nullable: false),
                    single_bal_sabha_id = table.Column<int>(type: "int", nullable: false),
                    single_bal_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    single_bal_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    single_bal_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    single_bal_create_by = table.Column<int>(type: "int", nullable: false),
                    single_bal_update_by = table.Column<int>(type: "int", nullable: true),
                    single_bal_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_single_open", x => x.single_bal_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vt_obl_single_open");

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "vt_custom_vote_balance",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(6)",
                oldRowVersion: true,
                oldNullable: true)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);
        }
    }
}
