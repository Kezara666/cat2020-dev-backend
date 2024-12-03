using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalProcessTableFieldNameRenamedFirstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_shopRental_processes_sr_process_year_sr_process_sbaha_id_sr_~",
                table: "shopRental_processes");

            migrationBuilder.RenameColumn(
                name: "sr_process_congig_id",
                table: "shopRental_processes",
                newName: "sr_process_config_assign_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sr_process_config_assign_id",
                table: "shopRental_processes",
                newName: "sr_process_congig_id");

            migrationBuilder.CreateIndex(
                name: "IX_shopRental_processes_sr_process_year_sr_process_sbaha_id_sr_~",
                table: "shopRental_processes",
                columns: new[] { "sr_process_year", "sr_process_sbaha_id", "sr_process_type" },
                unique: true);
        }
    }
}
