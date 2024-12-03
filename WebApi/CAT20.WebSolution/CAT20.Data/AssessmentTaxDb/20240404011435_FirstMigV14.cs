using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "assmt_proptypeslog_act_quarter",
                table: "assmt_property_types_logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assmt_proptypeslog_act_year",
                table: "assmt_property_types_logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assmt_new_allocation_req_act_quarter",
                table: "assmt_new_allocation_requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assmt_new_allocation_req_act_year",
                table: "assmt_new_allocation_requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assmt_deslog_act_quarter",
                table: "assmt_description_logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assmt_deslog_act_year",
                table: "assmt_description_logs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assmt_proptypeslog_act_quarter",
                table: "assmt_property_types_logs");

            migrationBuilder.DropColumn(
                name: "assmt_proptypeslog_act_year",
                table: "assmt_property_types_logs");

            migrationBuilder.DropColumn(
                name: "assmt_new_allocation_req_act_quarter",
                table: "assmt_new_allocation_requests");

            migrationBuilder.DropColumn(
                name: "assmt_new_allocation_req_act_year",
                table: "assmt_new_allocation_requests");

            migrationBuilder.DropColumn(
                name: "assmt_deslog_act_quarter",
                table: "assmt_description_logs");

            migrationBuilder.DropColumn(
                name: "assmt_deslog_act_year",
                table: "assmt_description_logs");
        }
    }
}
