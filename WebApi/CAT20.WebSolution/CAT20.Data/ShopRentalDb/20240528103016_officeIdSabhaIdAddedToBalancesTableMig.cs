using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class officeIdSabhaIdAddedToBalancesTableMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sr_bal_office_id",
                table: "sr_shopRental_balance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sr_bal_sabha_id",
                table: "sr_shopRental_balance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sr_bal_status",
                table: "sr_shopRental_balance",
                type: "int",
                nullable: true,
                defaultValueSql: "'1'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_bal_office_id",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_sabha_id",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_status",
                table: "sr_shopRental_balance");
        }
    }
}
