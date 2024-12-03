using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    public partial class assessmenet_user_activity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "assessment_User_Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    key = table.Column<int>(type: "int", nullable: false),
                    entity = table.Column<int>(type: "int", nullable: true),
                    service_no = table.Column<int>(type: "int", nullable: true),
                    action = table.Column<int>(type: "int", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    action_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessment_User_Activity", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assessment_User_Activity");
        }
    }
}
