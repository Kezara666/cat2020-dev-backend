using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class RemoverelsBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_res_business_places_res_businesses_business_id",
                table: "res_business_places");

            migrationBuilder.DropForeignKey(
                name: "FK_res_businesses_res_partner_business_owner_id",
                table: "res_businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_res_businesses_res_partner_property_owner_id",
                table: "res_businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_res_businesses_tax_types_tax_type",
                table: "res_businesses");

            migrationBuilder.DropIndex(
                name: "IX_res_businesses_business_owner_id",
                table: "res_businesses");

            migrationBuilder.DropIndex(
                name: "IX_res_businesses_property_owner_id",
                table: "res_businesses");

            migrationBuilder.DropIndex(
                name: "IX_res_businesses_tax_type",
                table: "res_businesses");

            migrationBuilder.DropIndex(
                name: "IX_res_business_places_business_id",
                table: "res_business_places");

            migrationBuilder.AlterColumn<int>(
                name: "tax_type",
                table: "res_businesses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "tax_type",
                table: "res_businesses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_res_businesses_business_owner_id",
                table: "res_businesses",
                column: "business_owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_res_businesses_property_owner_id",
                table: "res_businesses",
                column: "property_owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_res_businesses_tax_type",
                table: "res_businesses",
                column: "tax_type");

            migrationBuilder.CreateIndex(
                name: "IX_res_business_places_business_id",
                table: "res_business_places",
                column: "business_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_res_business_places_res_businesses_business_id",
                table: "res_business_places",
                column: "business_id",
                principalTable: "res_businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_res_businesses_res_partner_business_owner_id",
                table: "res_businesses",
                column: "business_owner_id",
                principalTable: "res_partner",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_res_businesses_res_partner_property_owner_id",
                table: "res_businesses",
                column: "property_owner_id",
                principalTable: "res_partner",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_res_businesses_tax_types_tax_type",
                table: "res_businesses",
                column: "tax_type",
                principalTable: "tax_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
