using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class classficationAndMLAchnages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vt_mainLedgerAccount_vt_classification_ClassificationId",
                table: "vt_mainLedgerAccount");

            migrationBuilder.RenameTable(
                name: "vt_mainLedgerAccount",
                newName: "vt_main_ledger_account");

            migrationBuilder.RenameColumn(
                name: "ClassificationId",
                table: "vt_main_ledger_account",
                newName: "vt_mla_classification_id");

            migrationBuilder.RenameIndex(
                name: "IX_vt_mainLedgerAccount_ClassificationId",
                table: "vt_main_ledger_account",
                newName: "IX_vt_main_ledger_account_vt_mla_classification_id");

            migrationBuilder.AddForeignKey(
                name: "FK_vt_main_ledger_account_vt_classification_vt_mla_classificati~",
                table: "vt_main_ledger_account",
                column: "vt_mla_classification_id",
                principalTable: "vt_classification",
                principalColumn: "vt_clsf_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vt_main_ledger_account_vt_classification_vt_mla_classificati~",
                table: "vt_main_ledger_account");

            migrationBuilder.RenameTable(
                name: "vt_main_ledger_account",
                newName: "vt_mainLedgerAccount");

            migrationBuilder.RenameColumn(
                name: "vt_mla_classification_id",
                table: "vt_mainLedgerAccount",
                newName: "ClassificationId");

            migrationBuilder.RenameIndex(
                name: "IX_vt_main_ledger_account_vt_mla_classification_id",
                table: "vt_mainLedgerAccount",
                newName: "IX_vt_mainLedgerAccount_ClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_vt_mainLedgerAccount_vt_classification_ClassificationId",
                table: "vt_mainLedgerAccount",
                column: "ClassificationId",
                principalTable: "vt_classification",
                principalColumn: "vt_clsf_id");
        }
    }
}
