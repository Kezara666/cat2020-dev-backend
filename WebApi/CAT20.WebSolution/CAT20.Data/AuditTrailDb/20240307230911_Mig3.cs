using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    public partial class Mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditTrailDetails_AuditTrails_AuditTrailID",
                table: "AuditTrailDetails");

            migrationBuilder.DropColumn(
                name: "AuditReference",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "AuditReference",
                table: "AuditTrailDetails");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AuditTrailDetails");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "AuditTrails",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "AuditTrailDetails",
                newName: "UserCreated");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "AuditTrails",
                type: "Timestamp",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "longblob");

            migrationBuilder.AlterColumn<string>(
                name: "EntityType",
                table: "AuditTrails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "AuditTrails",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "AuditTrails",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "AuditTrails",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserCreated",
                table: "AuditTrails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "AuditTrailDetails",
                type: "Timestamp",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "longblob");

            migrationBuilder.AlterColumn<string>(
                name: "Property",
                table: "AuditTrailDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "PreviousValue",
                table: "AuditTrailDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "NewValue",
                table: "AuditTrailDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "EntityType",
                table: "AuditTrailDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "EntityID",
                table: "AuditTrailDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "AuditTrailDetails",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "AuditTrailDetails",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuditTrailID",
                table: "AuditTrailDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserCreated",
                table: "AuditTrailDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditTrailDetails_AuditTrails_AuditTrailID",
                table: "AuditTrailDetails",
                column: "AuditTrailID",
                principalTable: "AuditTrails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditTrailDetails_AuditTrails_AuditTrailID",
                table: "AuditTrailDetails");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "AuditTrails",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "AuditTrailDetails",
                newName: "User");

            migrationBuilder.AlterColumn<byte[]>(
                name: "TimeStamp",
                table: "AuditTrails",
                type: "longblob",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(DateTime),
                oldType: "Timestamp",
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EntityType",
                table: "AuditTrails",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "AuditTrails",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "AuditTrails",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "AuditTrails",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "AuditTrails",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AuditReference",
                table: "AuditTrails",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "AuditTrails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte[]>(
                name: "TimeStamp",
                table: "AuditTrailDetails",
                type: "longblob",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(DateTime),
                oldType: "Timestamp",
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Property",
                table: "AuditTrailDetails",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PreviousValue",
                table: "AuditTrailDetails",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "NewValue",
                table: "AuditTrailDetails",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "EntityType",
                table: "AuditTrailDetails",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "EntityID",
                table: "AuditTrailDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "AuditTrailDetails",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "AuditTrailDetails",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuditTrailID",
                table: "AuditTrailDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "AuditTrailDetails",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AuditReference",
                table: "AuditTrailDetails",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "AuditTrailDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditTrailDetails_AuditTrails_AuditTrailID",
                table: "AuditTrailDetails",
                column: "AuditTrailID",
                principalTable: "AuditTrails",
                principalColumn: "ID");
        }
    }
}
