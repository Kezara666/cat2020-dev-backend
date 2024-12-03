using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.HRMDb
{
    public partial class MIG_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "hr_pf_emp_type",
                table: "hr_pf_employee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "hr_pf_emp_programme_id",
                table: "hr_pf_employee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "hr_pf_emp_no",
                table: "hr_pf_employee",
                type: "varchar(255)",
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hr_pf_emp_last_name",
                table: "hr_pf_employee",
                type: "varchar(255)",
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hr_pf_emp_initials",
                table: "hr_pf_employee",
                type: "varchar(255)",
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<int>(
                name: "hr_pf_emp_gender",
                table: "hr_pf_employee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "hr_pf_emp_full_name",
                table: "hr_pf_employee",
                type: "varchar(500)",
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hr_pf_emp_first_name",
                table: "hr_pf_employee",
                type: "varchar(255)",
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "hr_pf_emp_dob",
                table: "hr_pf_employee",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "hr_pf_emp_civil_status",
                table: "hr_pf_employee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "hr_pf_emp_carder_status",
                table: "hr_pf_employee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "hr_pf_emp_type",
                table: "hr_pf_employee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "hr_pf_emp_programme_id",
                table: "hr_pf_employee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "hr_pf_employee",
                keyColumn: "hr_pf_emp_no",
                keyValue: null,
                column: "hr_pf_emp_no",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "hr_pf_emp_no",
                table: "hr_pf_employee",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.UpdateData(
                table: "hr_pf_employee",
                keyColumn: "hr_pf_emp_last_name",
                keyValue: null,
                column: "hr_pf_emp_last_name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "hr_pf_emp_last_name",
                table: "hr_pf_employee",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.UpdateData(
                table: "hr_pf_employee",
                keyColumn: "hr_pf_emp_initials",
                keyValue: null,
                column: "hr_pf_emp_initials",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "hr_pf_emp_initials",
                table: "hr_pf_employee",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<int>(
                name: "hr_pf_emp_gender",
                table: "hr_pf_employee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "hr_pf_employee",
                keyColumn: "hr_pf_emp_full_name",
                keyValue: null,
                column: "hr_pf_emp_full_name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "hr_pf_emp_full_name",
                table: "hr_pf_employee",
                type: "varchar(500)",
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.UpdateData(
                table: "hr_pf_employee",
                keyColumn: "hr_pf_emp_first_name",
                keyValue: null,
                column: "hr_pf_emp_first_name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "hr_pf_emp_first_name",
                table: "hr_pf_employee",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "hr_pf_emp_dob",
                table: "hr_pf_employee",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "hr_pf_emp_civil_status",
                table: "hr_pf_employee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "hr_pf_emp_carder_status",
                table: "hr_pf_employee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
