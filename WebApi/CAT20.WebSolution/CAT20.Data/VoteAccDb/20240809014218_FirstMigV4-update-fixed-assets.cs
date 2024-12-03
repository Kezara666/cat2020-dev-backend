using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigV4updatefixedassets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fxa_balance_amount",
                table: "vt_obl_fixed_assets",
                newName: "fxa_original_or_revalued_amount");

            migrationBuilder.AddColumn<decimal>(
                name: "fxa_accumulated_depreciation",
                table: "vt_obl_fixed_assets",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fxa_accumulated_depreciation",
                table: "vt_obl_fixed_assets");

            migrationBuilder.RenameColumn(
                name: "fxa_original_or_revalued_amount",
                table: "vt_obl_fixed_assets",
                newName: "fxa_balance_amount");
        }
    }
}
