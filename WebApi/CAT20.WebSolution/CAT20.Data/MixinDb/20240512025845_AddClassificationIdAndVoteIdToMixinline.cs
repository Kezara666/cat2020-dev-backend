using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.MixinDb
{
    public partial class AddClassificationIdAndVoteIdToMixinline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "classification_id",
                table: "mixin_order_line",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "vote_detail_id",
                table: "mixin_order_line",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "classification_id",
                table: "mixin_order_line");

            migrationBuilder.DropColumn(
                name: "vote_detail_id",
                table: "mixin_order_line");
        }
    }
}
