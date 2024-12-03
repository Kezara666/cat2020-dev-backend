using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalProcessCnfigNewColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "sr_processConfig_fine_fixed_amount",
                table: "sr_process_configuration",
                type: "decimal(65,30)",
                nullable: true,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_fixed_amount",
                table: "sr_process_configuration");
        }
    }
}
