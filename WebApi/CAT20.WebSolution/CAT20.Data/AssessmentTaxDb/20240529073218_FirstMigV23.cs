using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "assmt_vtasgn_vote_detail_id",
                table: "assmt_vote_assigns",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "assmt_tr_session_date",
                table: "assmt_transactions",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assmt_vtasgn_vote_detail_id",
                table: "assmt_vote_assigns");

            migrationBuilder.DropColumn(
                name: "assmt_tr_session_date",
                table: "assmt_transactions");
        }
    }
}
