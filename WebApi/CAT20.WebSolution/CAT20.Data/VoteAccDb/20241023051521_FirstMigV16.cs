using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigV16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "system_create_at",
                table: "vt_obl_fixed_assets",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

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
                name: "DepreciationRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubTitleCode = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SabhaId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepreciationRates", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_ldger_acc_grp_assign",
                columns: table => new
                {
                    ldger_acc_grp_asgn_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ldger_acc_grp_asgn_subtitle_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ldger_acc_grp_asgn_subtitle_desc = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ldger_acc_grp_asgn_category = table.Column<int>(type: "int", nullable: false),
                    ldger_acc_grp_asgn_catgry_desc = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ldger_acc_grp_asgn_account_system_version = table.Column<int>(type: "int", nullable: false),
                    ldger_acc_grp_asgn_created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ldger_acc_grp_asgn_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ldger_acc_grp_asgn_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepreciationRates");

            migrationBuilder.DropTable(
                name: "vt_ldger_acc_grp_assign");

            migrationBuilder.AlterColumn<DateTime>(
                name: "system_create_at",
                table: "vt_obl_fixed_assets",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

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
