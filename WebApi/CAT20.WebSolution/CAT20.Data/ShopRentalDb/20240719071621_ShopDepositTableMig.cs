using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopDepositTableMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sr_shop_deposits",
                columns: table => new
                {
                    sr_shop_deposit_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_shop_deposit_shop_id = table.Column<int>(type: "int", nullable: false),
                    sr_shop_deposit_depositID = table.Column<int>(type: "int", nullable: false),
                    sr_shop_deposit_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    sr_shop_deposit_mixinOrder_id = table.Column<int>(type: "int", nullable: true),
                    sr_shop_deposit_mixinOrderLine_id = table.Column<int>(type: "int", nullable: true),
                    sr_shop_deposit_receiptNo = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_shop_deposit_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sr_shop_deposit_session_id = table.Column<int>(type: "int", nullable: false),
                    sr_shop_deposit_type = table.Column<int>(type: "int", nullable: true),
                    sr_shop_deposit_isFullyRefund = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sr_shop_deposit_status = table.Column<int>(type: "int", nullable: true),
                    sr_shop_deposit_sabha_id = table.Column<int>(type: "int", nullable: false),
                    sr_shop_deposit_office_id = table.Column<int>(type: "int", nullable: false),
                    sr_shop_deposit_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_shop_deposit_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_shop_deposit_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_shop_deposit_updated_by = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_shop_deposits", x => x.sr_shop_deposit_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_shop_deposits");
        }
    }
}
