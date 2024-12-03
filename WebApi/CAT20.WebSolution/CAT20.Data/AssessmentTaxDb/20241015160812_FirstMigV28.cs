using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qrt_adjustment",
                table: "assmt_quarter_report",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh4_adjusment",
                table: "assmt_qh4",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh4_report_adjusment",
                table: "assmt_qh4",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh3_adjusment",
                table: "assmt_qh3",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh3_report_adjusment",
                table: "assmt_qh3",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh2_adjusment",
                table: "assmt_qh2",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh2_report_adjusment",
                table: "assmt_qh2",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh1_adjusment",
                table: "assmt_qh1",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh1_report_adjusment",
                table: "assmt_qh1",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q4_adjusment",
                table: "assmt_q4",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q4_report_adjusment",
                table: "assmt_q4",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q3_adjusment",
                table: "assmt_q3",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q3_report_adjusment",
                table: "assmt_q3",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q2_adjusment",
                table: "assmt_q2",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q2_report_adjusment",
                table: "assmt_q2",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q1_adjusment",
                table: "assmt_q1",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q1_report_adjusment",
                table: "assmt_q1",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_bal_lya_adjusment",
                table: "assmt_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_bal_lyw_adjusment",
                table: "assmt_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_bal_overpay_adjusment",
                table: "assmt_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_bal_tya_adjusment",
                table: "assmt_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_bal_tyw_adjusment",
                table: "assmt_balances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q1_adjustment",
                table: "assessment_journals",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q2_adjustment",
                table: "assessment_journals",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q3_adjustment",
                table: "assessment_journals",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q4_adjustment",
                table: "assessment_journals",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assmt_qrt_adjustment",
                table: "assmt_quarter_report");

            migrationBuilder.DropColumn(
                name: "assmt_qh4_adjusment",
                table: "assmt_qh4");

            migrationBuilder.DropColumn(
                name: "assmt_qh4_report_adjusment",
                table: "assmt_qh4");

            migrationBuilder.DropColumn(
                name: "assmt_qh3_adjusment",
                table: "assmt_qh3");

            migrationBuilder.DropColumn(
                name: "assmt_qh3_report_adjusment",
                table: "assmt_qh3");

            migrationBuilder.DropColumn(
                name: "assmt_qh2_adjusment",
                table: "assmt_qh2");

            migrationBuilder.DropColumn(
                name: "assmt_qh2_report_adjusment",
                table: "assmt_qh2");

            migrationBuilder.DropColumn(
                name: "assmt_qh1_adjusment",
                table: "assmt_qh1");

            migrationBuilder.DropColumn(
                name: "assmt_qh1_report_adjusment",
                table: "assmt_qh1");

            migrationBuilder.DropColumn(
                name: "assmt_q4_adjusment",
                table: "assmt_q4");

            migrationBuilder.DropColumn(
                name: "assmt_q4_report_adjusment",
                table: "assmt_q4");

            migrationBuilder.DropColumn(
                name: "assmt_q3_adjusment",
                table: "assmt_q3");

            migrationBuilder.DropColumn(
                name: "assmt_q3_report_adjusment",
                table: "assmt_q3");

            migrationBuilder.DropColumn(
                name: "assmt_q2_adjusment",
                table: "assmt_q2");

            migrationBuilder.DropColumn(
                name: "assmt_q2_report_adjusment",
                table: "assmt_q2");

            migrationBuilder.DropColumn(
                name: "assmt_q1_adjusment",
                table: "assmt_q1");

            migrationBuilder.DropColumn(
                name: "assmt_q1_report_adjusment",
                table: "assmt_q1");

            migrationBuilder.DropColumn(
                name: "assmt_bal_lya_adjusment",
                table: "assmt_balances");

            migrationBuilder.DropColumn(
                name: "assmt_bal_lyw_adjusment",
                table: "assmt_balances");

            migrationBuilder.DropColumn(
                name: "assmt_bal_overpay_adjusment",
                table: "assmt_balances");

            migrationBuilder.DropColumn(
                name: "assmt_bal_tya_adjusment",
                table: "assmt_balances");

            migrationBuilder.DropColumn(
                name: "assmt_bal_tyw_adjusment",
                table: "assmt_balances");

            migrationBuilder.DropColumn(
                name: "assmt_q1_adjustment",
                table: "assessment_journals");

            migrationBuilder.DropColumn(
                name: "assmt_q2_adjustment",
                table: "assessment_journals");

            migrationBuilder.DropColumn(
                name: "assmt_q3_adjustment",
                table: "assessment_journals");

            migrationBuilder.DropColumn(
                name: "assmt_q4_adjustment",
                table: "assessment_journals");
        }
    }
}
