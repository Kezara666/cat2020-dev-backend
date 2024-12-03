using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "wb_bal_read_by",
                table: "wb_balances",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wb_bal_read_by",
                table: "wb_balances");
        }
    }
}
