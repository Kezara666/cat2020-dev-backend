using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class setnullablevaluesbankbranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel4",
                table: "cd_bank_branches",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel3",
                table: "cd_bank_branches",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel2",
                table: "cd_bank_branches",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel1",
                table: "cd_bank_branches",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_fax_no",
                table: "cd_bank_branches",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_district",
                table: "cd_bank_branches",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_branch_address",
                table: "cd_bank_branches",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "cd_bank_branches",
                keyColumn: "cd_bb_tel4",
                keyValue: null,
                column: "cd_bb_tel4",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel4",
                table: "cd_bank_branches",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.UpdateData(
                table: "cd_bank_branches",
                keyColumn: "cd_bb_tel3",
                keyValue: null,
                column: "cd_bb_tel3",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel3",
                table: "cd_bank_branches",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.UpdateData(
                table: "cd_bank_branches",
                keyColumn: "cd_bb_tel2",
                keyValue: null,
                column: "cd_bb_tel2",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel2",
                table: "cd_bank_branches",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.UpdateData(
                table: "cd_bank_branches",
                keyColumn: "cd_bb_tel1",
                keyValue: null,
                column: "cd_bb_tel1",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel1",
                table: "cd_bank_branches",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.UpdateData(
                table: "cd_bank_branches",
                keyColumn: "cd_bb_fax_no",
                keyValue: null,
                column: "cd_bb_fax_no",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_fax_no",
                table: "cd_bank_branches",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.UpdateData(
                table: "cd_bank_branches",
                keyColumn: "cd_bb_district",
                keyValue: null,
                column: "cd_bb_district",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_district",
                table: "cd_bank_branches",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.UpdateData(
                table: "cd_bank_branches",
                keyColumn: "cd_bb_branch_address",
                keyValue: null,
                column: "cd_bb_branch_address",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_branch_address",
                table: "cd_bank_branches",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }
    }
}
