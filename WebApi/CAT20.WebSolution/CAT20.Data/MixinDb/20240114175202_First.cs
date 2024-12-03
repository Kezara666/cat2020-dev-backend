using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.MixinDb
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "online_payment_det_id",
                table: "mixin_order",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "online_payment_det_id",
                table: "mixin_order");
        }
    }
}
