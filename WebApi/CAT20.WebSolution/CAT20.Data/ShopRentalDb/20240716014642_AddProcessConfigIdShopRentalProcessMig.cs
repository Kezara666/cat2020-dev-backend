using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class AddProcessConfigIdShopRentalProcessMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sr_process_config_id",
                table: "shopRental_processes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_process_config_id",
                table: "shopRental_processes");
        }
    }
}
