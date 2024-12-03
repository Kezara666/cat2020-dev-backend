using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    public partial class InitialMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeStamp = table.Column<byte[]>(type: "longblob", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AuditReference = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityType = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityID = table.Column<int>(type: "int", nullable: true),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Action = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "AuditTrailDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeStamp = table.Column<byte[]>(type: "longblob", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AuditReference = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuditTrailID = table.Column<int>(type: "int", nullable: true),
                    EntityType = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityID = table.Column<int>(type: "int", nullable: true),
                    Property = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PreviousValue = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NewValue = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Action = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrailDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AuditTrailDetails_AuditTrails_AuditTrailID",
                        column: x => x.AuditTrailID,
                        principalTable: "AuditTrails",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrailDetails_AuditTrailID",
                table: "AuditTrailDetails",
                column: "AuditTrailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrailDetails");

            migrationBuilder.DropTable(
                name: "AuditTrails");
        }
    }
}
