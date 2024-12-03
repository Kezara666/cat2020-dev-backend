using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.UserAccDb
{
    public partial class AddPINtoUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ud_pin",
                table: "user_details",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ud_pin",
                table: "user_details");
        }
    }
}
