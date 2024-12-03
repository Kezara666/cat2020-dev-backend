using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopAgreementActivityLog1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sr_shop_agr_activity_log",
                columns: table => new
                {
                    sr_shop_agr_activity_log_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_shop_agr_activity_log_shop_id = table.Column<int>(type: "int", maxLength: 45, nullable: false),
                    sr_shop_agr_activity_log_current_agreement_end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    sr_shop_agr_activity_log_agreement_extended_date = table.Column<DateOnly>(type: "date", nullable: true),
                    sr_shop_agr_activity_log_office_id = table.Column<int>(type: "int", nullable: true),
                    sr_shop_agr_activity_log_sabha_id = table.Column<int>(type: "int", nullable: true),
                    sr_shop_agr_activity_log_approved_by = table.Column<int>(type: "int", nullable: true),
                    sr_shop_agr_activity_log_approve_comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_shop_agr_activity_log_created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_shop_agr_activity_log_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_shop_agr_activity_log_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_shop_agr_activity_log_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_shop_agr_activity_log", x => x.sr_shop_agr_activity_log_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_shop_agr_activity_log");
        }
    }
}
