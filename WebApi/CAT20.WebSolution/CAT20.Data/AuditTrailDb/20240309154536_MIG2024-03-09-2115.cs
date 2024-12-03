using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    public partial class MIG202403092115 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "AuditTrailDetails");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "AuditTrails",
                newName: "ActionBy");

            migrationBuilder.AddColumn<int>(
                name: "ActionBy",
                table: "AuditTrailDetails",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionBy",
                table: "AuditTrailDetails");

            migrationBuilder.RenameColumn(
                name: "ActionBy",
                table: "AuditTrails",
                newName: "UserID");

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "AuditTrails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "AuditTrailDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
