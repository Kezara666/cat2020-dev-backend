using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class RecievableIncomeTableModifiedMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_ri_vote_last_year_arreas_amount_id",
                table: "sr_shopRental_receivable_income_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_ri_vote_last_year_fine_id",
                table: "sr_shopRental_receivable_income_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_ri_vote_over_payment_id",
                table: "sr_shopRental_receivable_income_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_ri_vote_property_rental_id",
                table: "sr_shopRental_receivable_income_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_ri_vote_service_charge_arreas_amount_id",
                table: "sr_shopRental_receivable_income_vote_assign");

            migrationBuilder.RenameColumn(
                name: "sr_ri_vote_this_year_fine_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                newName: "sr_ri_vote_property_service_charge_income_id");

            migrationBuilder.RenameColumn(
                name: "sr_ri_vote_this_year_arreas_amount_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                newName: "sr_ri_vote_property_rental_income_id");

            migrationBuilder.RenameColumn(
                name: "sr_ri_vote_service_charge_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                newName: "sr_ri_vote_property_fine_income_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sr_ri_vote_property_service_charge_income_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                newName: "sr_ri_vote_this_year_fine_id");

            migrationBuilder.RenameColumn(
                name: "sr_ri_vote_property_rental_income_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                newName: "sr_ri_vote_this_year_arreas_amount_id");

            migrationBuilder.RenameColumn(
                name: "sr_ri_vote_property_fine_income_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                newName: "sr_ri_vote_service_charge_id");

            migrationBuilder.AddColumn<int>(
                name: "sr_ri_vote_last_year_arreas_amount_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_ri_vote_last_year_fine_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_ri_vote_over_payment_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_ri_vote_property_rental_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_ri_vote_service_charge_arreas_amount_id",
                table: "sr_shopRental_receivable_income_vote_assign",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
