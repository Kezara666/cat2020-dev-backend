using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.MixinDb
{
    public partial class addautotime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "mixin_order",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "mixin_order",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
