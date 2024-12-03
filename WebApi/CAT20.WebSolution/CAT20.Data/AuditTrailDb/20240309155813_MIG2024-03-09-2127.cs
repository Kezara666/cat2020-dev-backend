using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    public partial class MIG202403092127 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Property",
                table: "AuditTrailDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "PreviousValue",
                table: "AuditTrailDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "NewValue",
                table: "AuditTrailDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AuditTrailDetails",
                keyColumn: "Property",
                keyValue: null,
                column: "Property",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Property",
                table: "AuditTrailDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AuditTrailDetails",
                keyColumn: "PreviousValue",
                keyValue: null,
                column: "PreviousValue",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PreviousValue",
                table: "AuditTrailDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AuditTrailDetails",
                keyColumn: "NewValue",
                keyValue: null,
                column: "NewValue",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NewValue",
                table: "AuditTrailDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
