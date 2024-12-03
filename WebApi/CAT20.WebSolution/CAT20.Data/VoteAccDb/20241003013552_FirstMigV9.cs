using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigV9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stc_custom_vote_id",
                table: "vt_obl_stores_creditors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "r_ex_nex_custom_vote_id",
                table: "vt_obl_receivable_exchange_non_exchange",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "prepay_category_custom_vote_id",
                table: "vt_obl_pre_paid_payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "lal_loan_type_custom_vote",
                table: "vt_obl_la_loan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idb_custom_vote_id",
                table: "vt_obl_industrial_debtors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idc_creditor_custom_vote_id",
                table: "vt_obl_industrial_creditors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "fd_custom_vote_id",
                table: "vt_obl_fixed_deposits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fxa_acquired_date",
                table: "vt_obl_fixed_assets",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<int>(
                name: "fxa_custom_vote_id",
                table: "vt_obl_fixed_assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "fxa_remaining_life_time",
                table: "vt_obl_fixed_assets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fxa_revalue_date",
                table: "vt_obl_fixed_assets",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cr_bl_custom_vote_id",
                table: "vt_obl_creditor_billing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "vt_custom_vote_balance",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(6)",
                oldRowVersion: true,
                oldNullable: true)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<int>(
                name: "CustomVoteId",
                table: "deposits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stc_custom_vote_id",
                table: "vt_obl_stores_creditors");

            migrationBuilder.DropColumn(
                name: "r_ex_nex_custom_vote_id",
                table: "vt_obl_receivable_exchange_non_exchange");

            migrationBuilder.DropColumn(
                name: "prepay_category_custom_vote_id",
                table: "vt_obl_pre_paid_payments");

            migrationBuilder.DropColumn(
                name: "lal_loan_type_custom_vote",
                table: "vt_obl_la_loan");

            migrationBuilder.DropColumn(
                name: "idb_custom_vote_id",
                table: "vt_obl_industrial_debtors");

            migrationBuilder.DropColumn(
                name: "idc_creditor_custom_vote_id",
                table: "vt_obl_industrial_creditors");

            migrationBuilder.DropColumn(
                name: "fd_custom_vote_id",
                table: "vt_obl_fixed_deposits");

            migrationBuilder.DropColumn(
                name: "fxa_custom_vote_id",
                table: "vt_obl_fixed_assets");

            migrationBuilder.DropColumn(
                name: "fxa_remaining_life_time",
                table: "vt_obl_fixed_assets");

            migrationBuilder.DropColumn(
                name: "fxa_revalue_date",
                table: "vt_obl_fixed_assets");

            migrationBuilder.DropColumn(
                name: "cr_bl_custom_vote_id",
                table: "vt_obl_creditor_billing");

            migrationBuilder.DropColumn(
                name: "CustomVoteId",
                table: "deposits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fxa_acquired_date",
                table: "vt_obl_fixed_assets",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "vt_custom_vote_balance",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(6)",
                oldRowVersion: true,
                oldNullable: true)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);
        }
    }
}
