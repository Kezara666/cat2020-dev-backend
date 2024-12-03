using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class AddSettledMixinOrderIdtoBals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sr_bal_settled_mixin_order_id",
                table: "sr_shopRental_balance",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_bal_settled_mixin_order_id",
                table: "sr_shopRental_balance");
        }
    }
}
