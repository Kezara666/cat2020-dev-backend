using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigV14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bdgt_Q4_amount",
                table: "vt_budget",
                newName: "bdgt_q4_amount");

            migrationBuilder.RenameColumn(
                name: "bdgt_Q3_amount",
                table: "vt_budget",
                newName: "bdgt_q3_amount");

            migrationBuilder.RenameColumn(
                name: "bdgt_Q2_amount",
                table: "vt_budget",
                newName: "bdgt_q2_amount");

            migrationBuilder.RenameColumn(
                name: "bdgt_Q1_amount",
                table: "vt_budget",
                newName: "bdgt_q1_amount");

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
                name: "bdgt_total",
                table: "vt_budget",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bdgt_total",
                table: "vt_budget");

            migrationBuilder.RenameColumn(
                name: "bdgt_q4_amount",
                table: "vt_budget",
                newName: "bdgt_Q4_amount");

            migrationBuilder.RenameColumn(
                name: "bdgt_q3_amount",
                table: "vt_budget",
                newName: "bdgt_Q3_amount");

            migrationBuilder.RenameColumn(
                name: "bdgt_q2_amount",
                table: "vt_budget",
                newName: "bdgt_Q2_amount");

            migrationBuilder.RenameColumn(
                name: "bdgt_q1_amount",
                table: "vt_budget",
                newName: "bdgt_Q1_amount");

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
