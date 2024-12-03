using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class ChartOfAccdefaultvalueset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "cd_s_chart_of_acc_version_id",
                table: "cd_sabha",
                type: "int",
                nullable: true,
                defaultValueSql: "'0'",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "cd_s_chart_of_acc_version_id",
                table: "cd_sabha",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "'0'");
        }
    }
}
