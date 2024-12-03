using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.MixinDb
{
    public partial class FirstMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "mixin_order");

            migrationBuilder.AddColumn<decimal>(
                name: "discount_rate",
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
                name: "discount_rate",
                table: "mixin_order");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "mixin_order",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
