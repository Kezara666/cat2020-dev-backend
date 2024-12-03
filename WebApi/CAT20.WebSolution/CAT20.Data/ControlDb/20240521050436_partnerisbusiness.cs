using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class partnerisbusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "business_reg_no",
                table: "res_partner",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "is_business",
                table: "res_partner",
                type: "int",
                nullable: true,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "business_reg_no",
                table: "res_partner");

            migrationBuilder.DropColumn(
                name: "is_business",
                table: "res_partner");
        }
    }
}
