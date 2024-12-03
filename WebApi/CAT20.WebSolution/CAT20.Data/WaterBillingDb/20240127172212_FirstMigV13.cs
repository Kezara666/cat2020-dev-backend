using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "wb_wc_running_vat_rate",
                table: "wb_water_connections",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wb_wc_running_vat_rate",
                table: "wb_water_connections");
        }
    }
}
