using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh4_discount_rate",
                table: "assmt_qh4",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_qh4_warrant_by",
                table: "assmt_qh4",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh4_warrant_rate",
                table: "assmt_qh4",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh3_discount_rate",
                table: "assmt_qh3",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_qh3_warrant_by",
                table: "assmt_qh3",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh3_warrant_rate",
                table: "assmt_qh3",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh2_discount_rate",
                table: "assmt_qh2",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_qh2_warrant_by",
                table: "assmt_qh2",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh2_warrant_rate",
                table: "assmt_qh2",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh1_discount_rate",
                table: "assmt_qh1",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_qh1_warrant_by",
                table: "assmt_qh1",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_qh1_warrant_rate",
                table: "assmt_qh1",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q4_discount_rate",
                table: "assmt_q4",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_q4_warrant_by",
                table: "assmt_q4",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q4_warrant_rate",
                table: "assmt_q4",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q3_discount_rate",
                table: "assmt_q3",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_q3_warrant_by",
                table: "assmt_q3",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q3_warrant_rate",
                table: "assmt_q3",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q2_discount_rate",
                table: "assmt_q2",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_q2_warrant_by",
                table: "assmt_q2",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q2_warrant_rate",
                table: "assmt_q2",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q1_discount_rate",
                table: "assmt_q1",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_q1_warrant_by",
                table: "assmt_q1",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_q1_warrant_rate",
                table: "assmt_q1",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assmt_qh4_discount_rate",
                table: "assmt_qh4");

            migrationBuilder.DropColumn(
                name: "assmt_qh4_warrant_by",
                table: "assmt_qh4");

            migrationBuilder.DropColumn(
                name: "assmt_qh4_warrant_rate",
                table: "assmt_qh4");

            migrationBuilder.DropColumn(
                name: "assmt_qh3_discount_rate",
                table: "assmt_qh3");

            migrationBuilder.DropColumn(
                name: "assmt_qh3_warrant_by",
                table: "assmt_qh3");

            migrationBuilder.DropColumn(
                name: "assmt_qh3_warrant_rate",
                table: "assmt_qh3");

            migrationBuilder.DropColumn(
                name: "assmt_qh2_discount_rate",
                table: "assmt_qh2");

            migrationBuilder.DropColumn(
                name: "assmt_qh2_warrant_by",
                table: "assmt_qh2");

            migrationBuilder.DropColumn(
                name: "assmt_qh2_warrant_rate",
                table: "assmt_qh2");

            migrationBuilder.DropColumn(
                name: "assmt_qh1_discount_rate",
                table: "assmt_qh1");

            migrationBuilder.DropColumn(
                name: "assmt_qh1_warrant_by",
                table: "assmt_qh1");

            migrationBuilder.DropColumn(
                name: "assmt_qh1_warrant_rate",
                table: "assmt_qh1");

            migrationBuilder.DropColumn(
                name: "assmt_q4_discount_rate",
                table: "assmt_q4");

            migrationBuilder.DropColumn(
                name: "assmt_q4_warrant_by",
                table: "assmt_q4");

            migrationBuilder.DropColumn(
                name: "assmt_q4_warrant_rate",
                table: "assmt_q4");

            migrationBuilder.DropColumn(
                name: "assmt_q3_discount_rate",
                table: "assmt_q3");

            migrationBuilder.DropColumn(
                name: "assmt_q3_warrant_by",
                table: "assmt_q3");

            migrationBuilder.DropColumn(
                name: "assmt_q3_warrant_rate",
                table: "assmt_q3");

            migrationBuilder.DropColumn(
                name: "assmt_q2_discount_rate",
                table: "assmt_q2");

            migrationBuilder.DropColumn(
                name: "assmt_q2_warrant_by",
                table: "assmt_q2");

            migrationBuilder.DropColumn(
                name: "assmt_q2_warrant_rate",
                table: "assmt_q2");

            migrationBuilder.DropColumn(
                name: "assmt_q1_discount_rate",
                table: "assmt_q1");

            migrationBuilder.DropColumn(
                name: "assmt_q1_warrant_by",
                table: "assmt_q1");

            migrationBuilder.DropColumn(
                name: "assmt_q1_warrant_rate",
                table: "assmt_q1");
        }
    }
}
