using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.MixinDb
{
    public partial class banking_orderid_unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "banking_order_id_UN",
                table: "banking",
                column: "order_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "banking_order_id_UN",
                table: "banking");
        }
    }
}
