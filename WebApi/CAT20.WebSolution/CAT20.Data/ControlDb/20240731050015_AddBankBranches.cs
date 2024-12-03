using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class AddBankBranches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cd_bank_code",
                table: "cd_bank_details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "cd_bank_branches",
                columns: table => new
                {
                    cd_bb_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_bb_branch_code = table.Column<int>(type: "int", nullable: false),
                    cd_bb_branch_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BranchCode = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_bb_branch_address = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_bb_tel1 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_bb_tel2 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_bb_tel3 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_bb_tel4 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_bb_fax_no = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_bb_district = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_d_status = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    cd_d_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    cd_d_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cd_bb_id);
                    table.ForeignKey(
                        name: "FK_cd_bank_branches_cd_bank_details_cd_bb_branch_code",
                        column: x => x.cd_bb_branch_code,
                        principalTable: "cd_bank_details",
                        principalColumn: "cd_bd_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_cd_bank_branches_cd_bb_branch_code",
                table: "cd_bank_branches",
                column: "cd_bb_branch_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_bank_branches");

            migrationBuilder.DropColumn(
                name: "cd_bank_code",
                table: "cd_bank_details");
        }
    }
}
