using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    public partial class _202403101703 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClaimedOfficeID",
                table: "AuditTrails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClaimedSabhaID",
                table: "AuditTrails",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimedOfficeID",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "ClaimedSabhaID",
                table: "AuditTrails");
        }
    }
}
