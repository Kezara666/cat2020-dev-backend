using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class DailyFineProcessLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shopRental_dailyfineprocesslog",
                columns: table => new
                {
                    sr_dailyfineprocesslog_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_dailyfineprocesslog_no_of_dates = table.Column<int>(type: "int", nullable: false),
                    sr_dailyfineprocesslog_daily_fine_rate = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    sr_dailyfineprocesslog_daily_fixed_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    sr_dailyfineprocesslog_total_fine_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    sr_dailyfineprocesslog_shop_id = table.Column<int>(type: "int", nullable: false),
                    sr_dailyfineprocesslog_process_configuration_id = table.Column<int>(type: "int", nullable: false),
                    sr_dailyfineprocesslog_created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    sr_dailyfineprocesslog_created_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopRental_dailyfineprocesslog", x => x.sr_dailyfineprocesslog_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shopRental_dailyfineprocesslog");
        }
    }
}
