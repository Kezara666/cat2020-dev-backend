using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "surcharge_amount",
                table: "vt_lgr_pmnt_voucher_line_log",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "surcharge_amount",
                table: "vt_lgr_pmnt_voucher_line",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "advanced_b_id",
                table: "vt_lgr_pmnt_voucher",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "surcharge_amount",
                table: "vt_lgr_pmnt_voucher_line_log");

            migrationBuilder.DropColumn(
                name: "surcharge_amount",
                table: "vt_lgr_pmnt_voucher_line");

            migrationBuilder.DropColumn(
                name: "advanced_b_id",
                table: "vt_lgr_pmnt_voucher");
        }
    }
}
