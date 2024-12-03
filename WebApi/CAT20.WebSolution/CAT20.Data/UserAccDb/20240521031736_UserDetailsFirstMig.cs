using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.UserAccDb
{
    public partial class UserDetailsFirstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SabhaID = table.Column<int>(type: "int", nullable: true),
                    UserCreatedID = table.Column<int>(type: "int", nullable: true),
                    UserModifiedID = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "user_details",
                columns: table => new
                {
                    ud_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ud_name_in_full = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ud_name_with_initials = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ud_username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ud_password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ud_nic = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ud_contact_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ud_birthday = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ud_sabha_id = table.Column<int>(type: "int", nullable: true),
                    ud_office_id = table.Column<int>(type: "int", nullable: true),
                    ud_active_status = table.Column<int>(type: "int", nullable: true),
                    ud_gender_id = table.Column<int>(type: "int", nullable: true, comment: "control db fk"),
                    ud_profile_pic_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ud_user_sign_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ud_q1_id = table.Column<int>(type: "int", nullable: true),
                    ud_answer1 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ud_q2_id = table.Column<int>(type: "int", nullable: true),
                    ud_answer2 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_admin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ud_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "user_login_activity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    login_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    logout_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_success_login = table.Column<int>(type: "int", nullable: true),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    office_id = table.Column<int>(type: "int", nullable: true),
                    operating_system = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    browser = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    device = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    os_version = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    browser_version = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    device_type = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    orientation = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ip_address = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rule_name = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_active_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "user_recover_questions",
                columns: table => new
                {
                    user_recover_questions_id = table.Column<int>(type: "int", nullable: false),
                    user_recover_questions = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_recover_questions", x => x.user_recover_questions_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "AuditLogUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Transaction = table.Column<int>(type: "int", nullable: false),
                    SourceID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RecordDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AuditLogUsers_user_details_UserID",
                        column: x => x.UserID,
                        principalTable: "user_details",
                        principalColumn: "ud_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UserCreatedID = table.Column<int>(type: "int", nullable: true),
                    UserModifiedID = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUsers_user_details_UserCreatedID",
                        column: x => x.UserCreatedID,
                        principalTable: "user_details",
                        principalColumn: "ud_id");
                    table.ForeignKey(
                        name: "FK_GroupUsers_user_details_UserID",
                        column: x => x.UserID,
                        principalTable: "user_details",
                        principalColumn: "ud_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUsers_user_details_UserModifiedID",
                        column: x => x.UserModifiedID,
                        principalTable: "user_details",
                        principalColumn: "ud_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Module = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserCreatedID = table.Column<int>(type: "int", nullable: true),
                    UserModifiedID = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rules_user_details_UserCreatedID",
                        column: x => x.UserCreatedID,
                        principalTable: "user_details",
                        principalColumn: "ud_id");
                    table.ForeignKey(
                        name: "FK_Rules_user_details_UserModifiedID",
                        column: x => x.UserModifiedID,
                        principalTable: "user_details",
                        principalColumn: "ud_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "GroupRules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    RuleID = table.Column<int>(type: "int", nullable: false),
                    UserCreatedID = table.Column<int>(type: "int", nullable: true),
                    UserModifiedID = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupRules_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupRules_Rules_RuleID",
                        column: x => x.RuleID,
                        principalTable: "Rules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupRules_user_details_UserCreatedID",
                        column: x => x.UserCreatedID,
                        principalTable: "user_details",
                        principalColumn: "ud_id");
                    table.ForeignKey(
                        name: "FK_GroupRules_user_details_UserModifiedID",
                        column: x => x.UserModifiedID,
                        principalTable: "user_details",
                        principalColumn: "ud_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogUsers_UserID",
                table: "AuditLogUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRules_GroupID",
                table: "GroupRules",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRules_RuleID",
                table: "GroupRules",
                column: "RuleID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRules_UserCreatedID",
                table: "GroupRules",
                column: "UserCreatedID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRules_UserModifiedID",
                table: "GroupRules",
                column: "UserModifiedID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupID",
                table: "GroupUsers",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserCreatedID",
                table: "GroupUsers",
                column: "UserCreatedID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserID",
                table: "GroupUsers",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserModifiedID",
                table: "GroupUsers",
                column: "UserModifiedID");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_UserCreatedID",
                table: "Rules",
                column: "UserCreatedID");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_UserModifiedID",
                table: "Rules",
                column: "UserModifiedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogUsers");

            migrationBuilder.DropTable(
                name: "GroupRules");

            migrationBuilder.DropTable(
                name: "GroupUsers");

            migrationBuilder.DropTable(
                name: "user_login_activity");

            migrationBuilder.DropTable(
                name: "user_recover_questions");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "user_details");
        }
    }
}
