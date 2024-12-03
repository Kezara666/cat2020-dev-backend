using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    public partial class _202403101718 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClaimedSabhaID",
                table: "AuditTrails",
                newName: "SabhaID");

            migrationBuilder.RenameColumn(
                name: "ClaimedOfficeID",
                table: "AuditTrails",
                newName: "OfficeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SabhaID",
                table: "AuditTrails",
                newName: "ClaimedSabhaID");

            migrationBuilder.RenameColumn(
                name: "OfficeID",
                table: "AuditTrails",
                newName: "ClaimedOfficeID");
        }
    }
}
