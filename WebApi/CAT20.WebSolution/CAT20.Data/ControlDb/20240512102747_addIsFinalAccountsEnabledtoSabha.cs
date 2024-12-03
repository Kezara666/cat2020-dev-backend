using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class addIsFinalAccountsEnabledtoSabha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cd_s_is_final_accounts_enabled",
                table: "cd_sabha",
                type: "int",
                nullable: true,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cd_s_is_final_accounts_enabled",
                table: "cd_sabha");
        }
    }
}
