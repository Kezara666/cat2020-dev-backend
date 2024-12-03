using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.UserAccDb
{
    public partial class setProfilePicPathNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ud_user_sign_path",
                table: "user_details",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ud_profile_pic_path", 
                table: "user_details",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "user_details",
                keyColumn: "ud_user_sign_path",
                keyValue: null,
                column: "ud_user_sign_path",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ud_user_sign_path",
                table: "user_details",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.UpdateData(
                table: "user_details",
                keyColumn: "ud_profile_pic_path",
                keyValue: null,
                column: "ud_profile_pic_path",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ud_profile_pic_path",
                table: "user_details",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }
    }
}
