using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class bankbranchtelnoslength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel4",
                table: "cd_bank_branches",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel3",
                table: "cd_bank_branches",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel2",
                table: "cd_bank_branches",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel1",
                table: "cd_bank_branches",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_fax_no",
                table: "cd_bank_branches",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel4",
                table: "cd_bank_branches",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel3",
                table: "cd_bank_branches",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel2",
                table: "cd_bank_branches",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_tel1",
                table: "cd_bank_branches",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cd_bb_fax_no",
                table: "cd_bank_branches",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }
    }
}
