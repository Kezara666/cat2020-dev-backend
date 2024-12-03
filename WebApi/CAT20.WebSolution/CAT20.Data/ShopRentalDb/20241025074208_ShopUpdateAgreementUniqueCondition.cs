using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopUpdateAgreementUniqueCondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sr_shop_sr_shop_office_id_sr_shop_agreement_no",
                table: "sr_shop");

            migrationBuilder.CreateIndex(
                name: "IX_sr_shop_sr_shop_office_id_sr_shop_id_sr_shop_agreement_no",
                table: "sr_shop",
                columns: new[] { "sr_shop_office_id", "sr_shop_id", "sr_shop_agreement_no" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sr_shop_sr_shop_office_id_sr_shop_id_sr_shop_agreement_no",
                table: "sr_shop");

            migrationBuilder.CreateIndex(
                name: "IX_sr_shop_sr_shop_office_id_sr_shop_agreement_no",
                table: "sr_shop",
                columns: new[] { "sr_shop_office_id", "sr_shop_agreement_no" },
                unique: true);
        }
    }
}
