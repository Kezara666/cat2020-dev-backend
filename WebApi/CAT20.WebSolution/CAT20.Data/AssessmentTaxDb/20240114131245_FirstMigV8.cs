using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_assm_temp_sub_partner_assmt_tmp_sub_ptnr_assmt_id",
            //    table: "assm_temp_sub_partner");

            migrationBuilder.CreateIndex(
                name: "IX_assm_temp_sub_partner_assmt_tmp_sub_ptnr_assmt_id",
                table: "assm_temp_sub_partner",
                column: "assmt_tmp_sub_ptnr_assmt_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_assm_temp_sub_partner_assmt_tmp_sub_ptnr_assmt_id",
            //    table: "assm_temp_sub_partner");

            migrationBuilder.CreateIndex(
                name: "IX_assm_temp_sub_partner_assmt_tmp_sub_ptnr_assmt_id",
                table: "assm_temp_sub_partner",
                column: "assmt_tmp_sub_ptnr_assmt_id",
                unique: true);
        }
    }
}
