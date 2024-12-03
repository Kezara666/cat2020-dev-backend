using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalProcessConfigTableUpdatedMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sr_process_configuration_sr_processConfig_sabha_id_sr_proces~",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_rate",
                table: "sr_process_configuration");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_processConfig_fine_1st_month_rate",
                table: "sr_process_configuration",
                type: "decimal(65,30)",
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_processConfig_fine_2nd_month_rate",
                table: "sr_process_configuration",
                type: "decimal(65,30)",
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_processConfig_fine_3rd_month_rate",
                table: "sr_process_configuration",
                type: "decimal(65,30)",
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_processConfig_fine_daily_rate",
                table: "sr_process_configuration",
                type: "decimal(65,30)",
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_processConfig_fine_monthly_rate",
                table: "sr_process_configuration",
                type: "decimal(65,30)",
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<int>(
                name: "sr_processConfig_fine_rate_type",
                table: "sr_process_configuration",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_sr_process_configuration_sr_processConfig_sabha_id_sr_proces~",
                table: "sr_process_configuration",
                columns: new[] { "sr_processConfig_sabha_id", "sr_processConfig_fine_rate_type", "sr_processConfig_fine_daily_rate", "sr_processConfig_fine_monthly_rate", "sr_processConfig_fine_1st_month_rate", "sr_processConfig_fine_2nd_month_rate", "sr_processConfig_fine_3rd_month_rate", "sr_processConfig_fine_cal_type", "sr_processConfig_rental_payment_date_type" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sr_process_configuration_sr_processConfig_sabha_id_sr_proces~",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_1st_month_rate",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_2nd_month_rate",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_3rd_month_rate",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_daily_rate",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_monthly_rate",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_rate_type",
                table: "sr_process_configuration");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_processConfig_fine_rate",
                table: "sr_process_configuration",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_sr_process_configuration_sr_processConfig_sabha_id_sr_proces~",
                table: "sr_process_configuration",
                columns: new[] { "sr_processConfig_sabha_id", "sr_processConfig_fine_rate", "sr_processConfig_fine_cal_type", "sr_processConfig_rental_payment_date_type" },
                unique: true);
        }
    }
}
