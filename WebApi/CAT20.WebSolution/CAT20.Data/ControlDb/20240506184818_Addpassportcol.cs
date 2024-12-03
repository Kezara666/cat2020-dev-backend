using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class Addpassportcol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "passport_number",
                table: "res_partner",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passport_number",
                table: "res_partner");
        }
    }
}
