using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "row_version",
                table: "wb_water_connections",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "row_version",
                table: "wb_application_for_connections",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<bool>(
                name: "wb_afc_is_assigned",
                table: "wb_application_for_connections",
                type: "tinyint(1)",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "row_version",
                table: "wb_water_connections");

            migrationBuilder.DropColumn(
                name: "row_version",
                table: "wb_application_for_connections");

            migrationBuilder.DropColumn(
                name: "wb_afc_is_assigned",
                table: "wb_application_for_connections");
        }
    }
}
