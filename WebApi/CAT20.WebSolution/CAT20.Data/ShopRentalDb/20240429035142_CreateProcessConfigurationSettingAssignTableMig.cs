using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class CreateProcessConfigurationSettingAssignTableMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sr_process_configuration_setting_assign",
                columns: table => new
                {
                    sr_process_configuration_setting_assign_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_process_configuration_setting_assign_shop_id = table.Column<int>(type: "int", nullable: false),
                    sr_process_configuration_setting_assign_process_configaration_id = table.Column<int>(type: "int", nullable: false),
                    sr_process_configuration_setting_assign_status = table.Column<int>(type: "int", nullable: true),
                    sr_process_configuration_setting_assign_sabha_id = table.Column<int>(type: "int", nullable: true),
                    sr_process_configuration_setting_assign_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_process_configuration_setting_assign_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_process_configuration_setting_assign_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_process_configuration_setting_assign_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_process_configuration_setting_assign", x => x.sr_process_configuration_setting_assign_id);
                    table.ForeignKey(
                        name: "sr_process_configuration_setting_assign_ibfk_1",
                        column: x => x.sr_process_configuration_setting_assign_shop_id,
                        principalTable: "sr_shop",
                        principalColumn: "sr_shop_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sr_process_configuration_setting_assign_ibfk_2",
                        column: x => x.sr_process_configuration_setting_assign_process_configaration_id,
                        principalTable: "sr_process_configuration",
                        principalColumn: "sr_processConfig_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_sr_process_configuration_setting_assign_sr_process_configur~1",
                table: "sr_process_configuration_setting_assign",
                column: "sr_process_configuration_setting_assign_process_configaration_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_process_configuration_setting_assign_sr_process_configura~",
                table: "sr_process_configuration_setting_assign",
                column: "sr_process_configuration_setting_assign_shop_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_process_configuration_setting_assign");
        }
    }
}
