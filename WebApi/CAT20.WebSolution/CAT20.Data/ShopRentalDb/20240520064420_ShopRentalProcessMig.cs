using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalProcessMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shopRental_processes",
                columns: table => new
                {
                    sr_process_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_process_action_by = table.Column<int>(type: "int", nullable: true),
                    sr_process_year = table.Column<int>(type: "int", nullable: false),
                    sr_process_sbaha_id = table.Column<int>(type: "int", nullable: false),
                    sr_process_type = table.Column<int>(type: "int", nullable: false),
                    sr_proceed_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_process_backupkey = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopRental_processes", x => x.sr_process_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_shopRental_processes_sr_process_year_sr_process_sbaha_id_sr_~",
                table: "shopRental_processes",
                columns: new[] { "sr_process_year", "sr_process_sbaha_id", "sr_process_type" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shopRental_processes");
        }
    }
}
