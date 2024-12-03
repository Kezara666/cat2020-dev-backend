using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.HRMDb
{
    public partial class MIG_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_employee_hr_pf_emp_nic_no",
                table: "hr_pf_employee",
                column: "hr_pf_emp_nic_no",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_hr_pf_employee_hr_pf_emp_nic_no",
                table: "hr_pf_employee");
        }
    }
}
