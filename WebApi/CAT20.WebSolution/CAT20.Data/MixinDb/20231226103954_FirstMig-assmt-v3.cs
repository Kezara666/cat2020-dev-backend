using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.MixinDb
{
    public partial class FirstMigassmtv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "assmt_discount_amount",
                table: "mixin_order_line",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_discount_rate",
                table: "mixin_order_line",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_gross_amount",
                table: "mixin_order_line",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_bal_by_excess_deduction",
                table: "mixin_order",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assmt_discount_amount",
                table: "mixin_order_line");

            migrationBuilder.DropColumn(
                name: "assmt_discount_rate",
                table: "mixin_order_line");

            migrationBuilder.DropColumn(
                name: "assmt_gross_amount",
                table: "mixin_order_line");

            migrationBuilder.DropColumn(
                name: "assmt_bal_by_excess_deduction",
                table: "mixin_order");
        }
    }
}
