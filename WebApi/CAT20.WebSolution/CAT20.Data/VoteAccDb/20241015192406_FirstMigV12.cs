using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigV12 : Migration
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
                name: "vt_budget",
                columns: table => new
                {
                    bdgt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    bdgt_budget_type = table.Column<int>(type: "int", nullable: true),
                    bdgt_custom_vote_id = table.Column<int>(type: "int", nullable: false),
                    bdgt_Q1_amount = table.Column<int>(type: "int", nullable: false),
                    bdgt_Q2_amount = table.Column<int>(type: "int", nullable: false),
                    bdgt_Q3_amount = table.Column<int>(type: "int", nullable: false),
                    bdgt_Q4_amount = table.Column<int>(type: "int", nullable: false),
                    bdgt_annual_amount = table.Column<int>(type: "int", nullable: false),
                    bdgt_january = table.Column<int>(type: "int", nullable: false),
                    bdgt_february = table.Column<int>(type: "int", nullable: false),
                    bdgt_march = table.Column<int>(type: "int", nullable: false),
                    bdgt_april = table.Column<int>(type: "int", nullable: false),
                    bdgt_may = table.Column<int>(type: "int", nullable: false),
                    bdgt_june = table.Column<int>(type: "int", nullable: false),
                    bdgt_july = table.Column<int>(type: "int", nullable: false),
                    bdgt_august = table.Column<int>(type: "int", nullable: false),
                    bdgt_september = table.Column<int>(type: "int", nullable: false),
                    bdgt_october = table.Column<int>(type: "int", nullable: false),
                    bdgt_november = table.Column<int>(type: "int", nullable: false),
                    bdgt_december = table.Column<int>(type: "int", nullable: false),
                    bdgt_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    bdgt_created_by = table.Column<int>(type: "int", nullable: true),
                    bdgt_updated_by = table.Column<int>(type: "int", nullable: true),
                    bdgt_sabha_id = table.Column<int>(type: "int", nullable: false),
                    bdgt_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    bdgt_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_budget", x => x.bdgt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vt_budget");

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
