using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.MixinDb
{
    public partial class FirstMigV_empyeeid_vs_partnerid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "res_partner_id",
                table: "mixin_order",
                type: "int",
                nullable: true,
                defaultValue: 60,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "hrm_employee_id",
                table: "mixin_order",
                type: "int",
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "res_partner_id",
                table: "mixin_order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 60);

            migrationBuilder.AlterColumn<int>(
                name: "hrm_employee_id",
                table: "mixin_order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 1);
        }
    }
}
