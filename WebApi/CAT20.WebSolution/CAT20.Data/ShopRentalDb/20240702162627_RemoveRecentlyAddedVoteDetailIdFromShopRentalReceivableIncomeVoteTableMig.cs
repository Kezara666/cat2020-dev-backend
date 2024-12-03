using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class RemoveRecentlyAddedVoteDetailIdFromShopRentalReceivableIncomeVoteTableMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_ri_vote_detail_property_fine_income_id",
                table: "sr_shopRental_receivable_income_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_ri_vote_detail_property_rental_income_id",
                table: "sr_shopRental_receivable_income_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_ri_vote_detail_property_service_charge_income_id",
                table: "sr_shopRental_receivable_income_vote_assign");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sr_ri_vote_detail_property_fine_income_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_ri_vote_detail_property_rental_income_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_ri_vote_detail_property_service_charge_income_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
