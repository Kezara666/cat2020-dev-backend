using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "QDiscount",
            //    table: "assmt_quarter_report",
            //    newName: "assmt_qrt_q_discount");

            migrationBuilder.AddColumn<int>(
                name: "assmt_used_tr_type",
                table: "assmt_quarter_report",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assmt_used_tr_type",
                table: "assmt_quarter_report");

            //migrationBuilder.RenameColumn(
            //    name: "assmt_qrt_q_discount",
            //    table: "assmt_quarter_report",
            //    newName: "QDiscount");
        }
    }
}
