using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalUsualAndIncomeVoteDetailIdAddedMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sr_vote_detail_last_year_arreas_amount_id",
                table: "sr_shopRental_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_vote_detail_last_year_fine_id",
                table: "sr_shopRental_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_vote_detail_over_payment_id",
                table: "sr_shopRental_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_vote_detail_property_rental_id",
                table: "sr_shopRental_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_vote_detail_service_charge_arreas_amount_id",
                table: "sr_shopRental_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_vote_detail_service_charge_id",
                table: "sr_shopRental_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_vote_detail_this_year_arreas_amount_id",
                table: "sr_shopRental_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_vote_detail_this_year_fine_id",
                table: "sr_shopRental_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_vote_detail_last_year_arreas_amount_id",
                table: "sr_shopRental_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_vote_detail_last_year_fine_id",
                table: "sr_shopRental_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_vote_detail_over_payment_id",
                table: "sr_shopRental_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_vote_detail_property_rental_id",
                table: "sr_shopRental_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_vote_detail_service_charge_arreas_amount_id",
                table: "sr_shopRental_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_vote_detail_service_charge_id",
                table: "sr_shopRental_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_vote_detail_this_year_arreas_amount_id",
                table: "sr_shopRental_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_vote_detail_this_year_fine_id",
                table: "sr_shopRental_vote_assign");

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
    }
}
