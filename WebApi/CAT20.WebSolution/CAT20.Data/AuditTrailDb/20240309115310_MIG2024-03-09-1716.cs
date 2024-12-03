using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    public partial class MIG202403091716 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AuditTrails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "AuditTrails",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "AuditTrails",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "AuditTrailDetails",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "AuditTrailDetails",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "AuditTrails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "AuditTrails",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AuditTrails");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AuditTrails",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "AuditTrails",
                newName: "DateModified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AuditTrails",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "AuditTrailDetails",
                newName: "DateModified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AuditTrailDetails",
                newName: "DateCreated");
        }
    }
}
