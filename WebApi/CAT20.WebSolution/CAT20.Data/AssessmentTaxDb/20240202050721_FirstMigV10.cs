using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "assmt_property_type_name",
                table: "assmt_property_types",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "assmt_property_type_name",
                table: "assmt_property_types",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");
        }
    }
}
