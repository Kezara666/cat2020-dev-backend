using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class AddBankBranchCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cd_bank_branches_cd_bank_details_cd_bb_branch_code",
                table: "cd_bank_branches");

            migrationBuilder.DropIndex(
                name: "IX_cd_bank_branches_cd_bb_branch_code",
                table: "cd_bank_branches");

            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "cd_bank_branches");

            migrationBuilder.AlterColumn<int>(
                name: "cd_bd_status",
                table: "cd_bank_details",
                type: "int",
                nullable: true,
                defaultValueSql: "'1'",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "cd_d_status",
                table: "cd_bank_branches",
                type: "int",
                maxLength: 10,
                nullable: true,
                defaultValueSql: "'1'",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_branch_code",
                table: "cd_bank_branches",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "cd_bb_bank_code",
                table: "cd_bank_branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_cd_bank_details_cd_bank_code",
                table: "cd_bank_details",
                column: "cd_bank_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cd_bank_branches_cd_bb_bank_code",
                table: "cd_bank_branches",
                column: "cd_bb_bank_code");

            migrationBuilder.AddForeignKey(
                name: "FK_cd_bank_branches_cd_bank_details_cd_bb_bank_code",
                table: "cd_bank_branches",
                column: "cd_bb_bank_code",
                principalTable: "cd_bank_details",
                principalColumn: "cd_bd_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cd_bank_branches_cd_bank_details_cd_bb_bank_code",
                table: "cd_bank_branches");

            migrationBuilder.DropIndex(
                name: "IX_cd_bank_details_cd_bank_code",
                table: "cd_bank_details");

            migrationBuilder.DropIndex(
                name: "IX_cd_bank_branches_cd_bb_bank_code",
                table: "cd_bank_branches");

            migrationBuilder.DropColumn(
                name: "cd_bb_bank_code",
                table: "cd_bank_branches");

            migrationBuilder.AlterColumn<int>(
                name: "cd_bd_status",
                table: "cd_bank_details",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "'1'");

            migrationBuilder.AlterColumn<int>(
                name: "cd_d_status",
                table: "cd_bank_branches",
                type: "int",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 10,
                oldNullable: true,
                oldDefaultValueSql: "'1'");

            migrationBuilder.AlterColumn<int>(
                name: "cd_bb_branch_code",
                table: "cd_bank_branches",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                table: "cd_bank_branches",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_cd_bank_branches_cd_bb_branch_code",
                table: "cd_bank_branches",
                column: "cd_bb_branch_code");

            migrationBuilder.AddForeignKey(
                name: "FK_cd_bank_branches_cd_bank_details_cd_bb_branch_code",
                table: "cd_bank_branches",
                column: "cd_bb_branch_code",
                principalTable: "cd_bank_details",
                principalColumn: "cd_bd_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
