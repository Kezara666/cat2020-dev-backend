using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalBalanceIsHoldStatusChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "sr_bal_is_shop_hold",
                table: "sr_shopRental_balance",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "sr_bal_is_shop_hold",
                table: "sr_shopRental_balance",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
