using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class shopRentalProcessForeignKeyLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_shopRental_processes_sr_process_config_id",
                table: "shopRental_processes",
                column: "sr_process_config_id");

            migrationBuilder.AddForeignKey(
                name: "sr_process_configuration_ibfk_2",
                table: "shopRental_processes",
                column: "sr_process_config_id",
                principalTable: "sr_process_configuration",
                principalColumn: "sr_processConfig_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "sr_process_configuration_ibfk_2",
                table: "shopRental_processes");

            migrationBuilder.DropIndex(
                name: "IX_shopRental_processes_sr_process_config_id",
                table: "shopRental_processes");
        }
    }
}
