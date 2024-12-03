using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopAgreementChangeRequestAddNewColumnDesciption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sr_agcr_description",
                table: "sr_agreement_change_request",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_agcr_description",
                table: "sr_agreement_change_request");
        }
    }
}
