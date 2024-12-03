using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.MixinDb
{
    public partial class FirstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "mixin_order",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "discount_amount",
                table: "mixin_order",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "mixin_order");

            migrationBuilder.DropColumn(
                name: "discount_amount",
                table: "mixin_order");
        }
    }
}
