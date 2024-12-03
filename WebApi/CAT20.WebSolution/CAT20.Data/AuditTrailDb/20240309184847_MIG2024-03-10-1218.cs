using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    public partial class MIG202403101218 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActionBy",
                table: "AuditTrails",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "ActionBy",
                table: "AuditTrailDetails",
                newName: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "AuditTrails",
                newName: "ActionBy");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "AuditTrailDetails",
                newName: "ActionBy");
        }
    }
}
