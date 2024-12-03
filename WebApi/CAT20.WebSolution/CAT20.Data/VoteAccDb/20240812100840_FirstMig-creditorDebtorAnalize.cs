using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigcreditorDebtorAnalize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_industrial_creditors_sabha_fund_dource",
                table: "vt_obl_industrial_creditors");

            migrationBuilder.DropForeignKey(
                name: "fk_industrial_debtors_sabha_fund_dource",
                table: "vt_obl_industrial_debtors");

            migrationBuilder.DropIndex(
                name: "IX_vt_obl_industrial_debtors_idb_fund_source_id",
                table: "vt_obl_industrial_debtors");

            migrationBuilder.DropIndex(
                name: "IX_vt_obl_industrial_creditors_idc_fund_source_id",
                table: "vt_obl_industrial_creditors");

            migrationBuilder.AddColumn<int>(
                name: "SabhaFundSourceId",
                table: "vt_obl_industrial_debtors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SabhaFundSourceId",
                table: "vt_obl_industrial_creditors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_vt_obl_industrial_debtors_SabhaFundSourceId",
                table: "vt_obl_industrial_debtors",
                column: "SabhaFundSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_vt_obl_industrial_creditors_SabhaFundSourceId",
                table: "vt_obl_industrial_creditors",
                column: "SabhaFundSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_vt_obl_industrial_creditors_vt_obl_sabha_fund_source_SabhaFu~",
                table: "vt_obl_industrial_creditors",
                column: "SabhaFundSourceId",
                principalTable: "vt_obl_sabha_fund_source",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_vt_obl_industrial_debtors_vt_obl_sabha_fund_source_SabhaFund~",
                table: "vt_obl_industrial_debtors",
                column: "SabhaFundSourceId",
                principalTable: "vt_obl_sabha_fund_source",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vt_obl_industrial_creditors_vt_obl_sabha_fund_source_SabhaFu~",
                table: "vt_obl_industrial_creditors");

            migrationBuilder.DropForeignKey(
                name: "FK_vt_obl_industrial_debtors_vt_obl_sabha_fund_source_SabhaFund~",
                table: "vt_obl_industrial_debtors");

            migrationBuilder.DropIndex(
                name: "IX_vt_obl_industrial_debtors_SabhaFundSourceId",
                table: "vt_obl_industrial_debtors");

            migrationBuilder.DropIndex(
                name: "IX_vt_obl_industrial_creditors_SabhaFundSourceId",
                table: "vt_obl_industrial_creditors");

            migrationBuilder.DropColumn(
                name: "SabhaFundSourceId",
                table: "vt_obl_industrial_debtors");

            migrationBuilder.DropColumn(
                name: "SabhaFundSourceId",
                table: "vt_obl_industrial_creditors");

            migrationBuilder.CreateIndex(
                name: "IX_vt_obl_industrial_debtors_idb_fund_source_id",
                table: "vt_obl_industrial_debtors",
                column: "idb_fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_obl_industrial_creditors_idc_fund_source_id",
                table: "vt_obl_industrial_creditors",
                column: "idc_fund_source_id");

            migrationBuilder.AddForeignKey(
                name: "fk_industrial_creditors_sabha_fund_dource",
                table: "vt_obl_industrial_creditors",
                column: "idc_fund_source_id",
                principalTable: "vt_obl_sabha_fund_source",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_industrial_debtors_sabha_fund_dource",
                table: "vt_obl_industrial_debtors",
                column: "idb_fund_source_id",
                principalTable: "vt_obl_sabha_fund_source",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
