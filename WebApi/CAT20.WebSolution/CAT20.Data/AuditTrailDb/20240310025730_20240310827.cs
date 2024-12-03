using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    public partial class _20240310827 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AuditTrailDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AuditTrailDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AuditTrails",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AuditTrails",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AuditTrailDetails",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AuditTrailDetails",
                type: "datetime",
                nullable: true);
        }
    }
}
