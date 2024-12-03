using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class setbankcodeprimary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cd_bank_branches_cd_bank_details_cd_bb_bank_code",
                table: "cd_bank_branches");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_cd_bank_details_cd_bank_code",
                table: "cd_bank_details",
                column: "cd_bank_code");

            migrationBuilder.AddForeignKey(
                name: "FK_cd_bank_branches_cd_bank_details_cd_bb_bank_code",
                table: "cd_bank_branches",
                column: "cd_bb_bank_code",
                principalTable: "cd_bank_details",
                principalColumn: "cd_bank_code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cd_bank_branches_cd_bank_details_cd_bb_bank_code",
                table: "cd_bank_branches");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_cd_bank_details_cd_bank_code",
                table: "cd_bank_details");

            migrationBuilder.AddForeignKey(
                name: "FK_cd_bank_branches_cd_bank_details_cd_bb_bank_code",
                table: "cd_bank_branches",
                column: "cd_bb_bank_code",
                principalTable: "cd_bank_details",
                principalColumn: "cd_bd_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
