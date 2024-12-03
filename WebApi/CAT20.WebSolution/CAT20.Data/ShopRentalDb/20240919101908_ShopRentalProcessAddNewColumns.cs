using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalProcessAddNewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sr_process_IsSkipped",
                table: "shopRental_processes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sr_process_description",
                table: "shopRental_processes",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_process_IsSkipped",
                table: "shopRental_processes");

            migrationBuilder.DropColumn(
                name: "sr_process_description",
                table: "shopRental_processes");
        }
    }
}
