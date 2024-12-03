using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class AllMigsUpTo20240504 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    User = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionName = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_app_category",
                columns: table => new
                {
                    cd_app_cat_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_app_cat_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_app_cat_status = table.Column<int>(type: "int", nullable: true),
                    cd_app_cat_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    cd_app_cat_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cd_app_cat_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_bank_details",
                columns: table => new
                {
                    cd_bd_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_bd_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_bd_status = table.Column<int>(type: "int", nullable: true),
                    cd_bd_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    cd_bd_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cd_bd_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_gender",
                columns: table => new
                {
                    cd_gender_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_gender = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cd_gender", x => x.cd_gender_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_gn_divisions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    office_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cd_gn_divisions", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_languages",
                columns: table => new
                {
                    cd_languages_id = table.Column<int>(type: "int", nullable: false),
                    cd_languages_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_language_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cd_languages", x => x.cd_languages_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_month",
                columns: table => new
                {
                    cd_month_id = table.Column<int>(type: "int", nullable: false),
                    cd_month = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cd_month", x => x.cd_month_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_office_type",
                columns: table => new
                {
                    cd_ot_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_ot_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_ot_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cd_ot_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_province",
                columns: table => new
                {
                    cd_p_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_p_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_p_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_p_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_p_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cd_p_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_selected_languages",
                columns: table => new
                {
                    cd_selected_languages_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_selected_languages_lang_id = table.Column<int>(type: "int", nullable: true),
                    cd_selected_languages_sabha_id = table.Column<int>(type: "int", nullable: true),
                    cd_selected_languages_status = table.Column<int>(type: "int", nullable: true),
                    cd_selected_languages_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    cd_selected_languages_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cd_selected_languages", x => x.cd_selected_languages_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_year",
                columns: table => new
                {
                    cd_year_id = table.Column<int>(type: "int", nullable: false),
                    cd_year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cd_year", x => x.cd_year_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cdb_customer_types",
                columns: table => new
                {
                    cdb_cus_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cdb_cus_type_name_in_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cdb_cus_type_name_in_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cdb_cus_type_name_in_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cdb_cus_type_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cdb_cus_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "document_sequence_numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    year = table.Column<int>(type: "int", nullable: false),
                    office_id = table.Column<int>(type: "int", nullable: false),
                    prefix = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_index = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_sequence_numbers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "EmailConfigurations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SystemMailAddress = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SystemMailPORT = table.Column<int>(type: "int", nullable: true),
                    SystemMailSMTP = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SystemMailPassword = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SystemURL = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'1'"),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    SabhaId = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfigurations", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "EmailOutBoxes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    Recipient = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bc = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cc = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsBodyHtml = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MailContent = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subject = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailSendAttempts = table.Column<int>(type: "int", nullable: false),
                    Attachment = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    SabhaId = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailOutBoxes", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "ir_session",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    module = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    start_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    stop_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    active = table.Column<sbyte>(type: "tinyint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    office_id = table.Column<int>(type: "int", nullable: false),
                    rescue = table.Column<int>(type: "int", nullable: false),
                    rescue_started_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    session_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ir_session", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "res_application",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_res_application", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "res_business_places",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    gn_division = table.Column<int>(type: "int", nullable: true),
                    ward = table.Column<int>(type: "int", nullable: true),
                    street = table.Column<int>(type: "int", nullable: true),
                    assessment_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_1 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_2 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zip = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<sbyte>(type: "tinyint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    business_id = table.Column<int>(type: "int", nullable: true),
                    property_owner_id = table.Column<int>(type: "int", nullable: true),
                    ri_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_res_business_places", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "res_businesses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    business_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_sub_owners = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_nature = table.Column<int>(type: "int", nullable: true),
                    business_sub_nature = table.Column<int>(type: "int", nullable: true),
                    business_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    business_reg_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tax_type = table.Column<int>(type: "int", nullable: true),
                    business_tel_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_email = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_web = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    no_of_employees = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<sbyte>(type: "tinyint", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    office_id = table.Column<int>(type: "int", nullable: true),
                    business_owner_id = table.Column<int>(type: "int", nullable: true),
                    property_owner_id = table.Column<int>(type: "int", nullable: true),
                    business_place_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_res_businesses", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "res_partner_title",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_res_partner_title", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "res_payment_method",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active = table.Column<sbyte>(type: "tinyint", nullable: false, defaultValueSql: "'1'"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_res_payment_method", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "res_payment_nbt",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount_percentage = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime", nullable: false),
                    date_modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_res_payment_nbt", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "res_payment_vat",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount_percentage = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime", nullable: false),
                    date_modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_res_payment_vat", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "SMSConfigurations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Provider = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SenderId = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiEndPoint = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiToken = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SabhaId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSConfigurations", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "SMSOutBoxes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Module = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subject = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SenderId = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Recipient = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SMSContent = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SMSStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SMSSendAttempts = table.Column<int>(type: "int", nullable: false),
                    SabhaID = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSOutBoxes", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "tax_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tax_type_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_main_tax = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax_types", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "res_partner",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nic_number = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mobile_number = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    street_1 = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    street_2 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zip = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active = table.Column<sbyte>(type: "tinyint", nullable: false, defaultValueSql: "'1'"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_editable = table.Column<sbyte>(type: "tinyint", nullable: false, defaultValueSql: "'1'"),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    gn_division_id = table.Column<int>(type: "int", nullable: true),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    is_tempory = table.Column<sbyte>(type: "tinyint", nullable: false, defaultValueSql: "'0'"),
                    is_business_owner = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    is_property_owner = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_res_partner", x => x.id);
                    table.ForeignKey(
                        name: "FK_res_partner_cd_gn_divisions_gn_division_id",
                        column: x => x.gn_division_id,
                        principalTable: "cd_gn_divisions",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_district",
                columns: table => new
                {
                    cd_d_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_d_cd_p_id = table.Column<int>(type: "int", nullable: false),
                    cd_d_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_d_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_d_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_d_status = table.Column<int>(type: "int", nullable: true),
                    cd_d_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    cd_d_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cd_d_id);
                    table.ForeignKey(
                        name: "fk_cd_d_cd_p_id",
                        column: x => x.cd_d_cd_p_id,
                        principalTable: "cd_province",
                        principalColumn: "cd_p_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "ConfigApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SessionConfigId = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigApplication_res_application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "res_application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "business_taxes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    business_id = table.Column<int>(type: "int", nullable: false),
                    current_year = table.Column<int>(type: "int", nullable: true),
                    application_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    license_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_year_value = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    annual_value = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    other_charges = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    tax_amount_by_nature = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    total_tax_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    office_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<sbyte>(type: "tinyint", nullable: true),
                    tax_state = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    is_moh_approved = table.Column<sbyte>(type: "tinyint", nullable: true),
                    is_secretary_approved = table.Column<sbyte>(type: "tinyint", nullable: true),
                    is_chairman_approved = table.Column<sbyte>(type: "tinyint", nullable: true),
                    moh_approved_by = table.Column<int>(type: "int", nullable: true),
                    secretary_approved_by = table.Column<int>(type: "int", nullable: true),
                    chairman_approved_by = table.Column<int>(type: "int", nullable: true),
                    moh_approved_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    secretary_approved_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    chairman_approved_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_business_taxes", x => x.id);
                    table.ForeignKey(
                        name: "business_taxes_FK",
                        column: x => x.business_id,
                        principalTable: "res_businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "partner_mobile",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nic_number = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nick_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mobile_no = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PartnerId = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_PartnerMobile_Partner_Id",
                        column: x => x.PartnerId,
                        principalTable: "res_partner",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "ptnr_permitted_trdparty_assmnts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    partner_id = table.Column<int>(type: "int", nullable: false),
                    assmt_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_ptnr_permitted_trdparty_assmnts_res_partner_partner_id",
                        column: x => x.partner_id,
                        principalTable: "res_partner",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_sabha",
                columns: table => new
                {
                    cd_s_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_s_cd_d_id = table.Column<int>(type: "int", nullable: true),
                    cd_s_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_logo_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_status = table.Column<int>(type: "int(1)", nullable: true, defaultValueSql: "'1'"),
                    cd_s_create_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    cd_s_tp_no1 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_tp_no2 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_address_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_address_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_address_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_account_system_versionid = table.Column<int>(type: "int", nullable: true),
                    cd_s_sabha_type = table.Column<int>(type: "int", nullable: true),
                    cd_s_latitude = table.Column<float>(type: "float", nullable: true),
                    cd_s_longitude = table.Column<float>(type: "float", nullable: true),
                    cd_s_secretary_sign_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_chairman_sign_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_s_created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    cd_s_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cd_s_id);
                    table.ForeignKey(
                        name: "fk_cd_s_cd_d_id",
                        column: x => x.cd_s_cd_d_id,
                        principalTable: "cd_district",
                        principalColumn: "cd_d_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "cd_office",
                columns: table => new
                {
                    cd_o_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_o_cd_s_id = table.Column<int>(type: "int", nullable: true),
                    cd_o_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_o_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_o_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_o_office_type_id = table.Column<int>(type: "int", nullable: false),
                    cd_o_status = table.Column<int>(type: "int(1)", nullable: true, defaultValueSql: "'1'"),
                    cd_o_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_o_create_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    cd_o_latitude = table.Column<float>(type: "float", nullable: true),
                    cd_o_longitude = table.Column<float>(type: "float", nullable: true),
                    cd_o_created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    cd_o_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cd_o_id);
                    table.ForeignKey(
                        name: "fk_cd_o_cd_s_id",
                        column: x => x.cd_o_cd_s_id,
                        principalTable: "cd_sabha",
                        principalColumn: "cd_s_id");
                    table.ForeignKey(
                        name: "fk_cd_o_office_type_id",
                        column: x => x.cd_o_office_type_id,
                        principalTable: "cd_office_type",
                        principalColumn: "cd_ot_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "business_taxes_FK",
                table: "business_taxes",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "fk_cd_d_cd_p_id",
                table: "cd_district",
                column: "cd_d_cd_p_id");

            migrationBuilder.CreateIndex(
                name: "cd_gn_divisions_FK",
                table: "cd_gn_divisions",
                column: "office_id");

            migrationBuilder.CreateIndex(
                name: "fk_cd_o_cd_s_id",
                table: "cd_office",
                column: "cd_o_cd_s_id");

            migrationBuilder.CreateIndex(
                name: "fk_cd_o_office_type_id",
                table: "cd_office",
                column: "cd_o_office_type_id");

            migrationBuilder.CreateIndex(
                name: "fk_cd_s_cd_d_id",
                table: "cd_sabha",
                column: "cd_s_cd_d_id");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigApplication_ApplicationId",
                table: "ConfigApplication",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_document_sequence_numbers_year_office_id",
                table: "document_sequence_numbers",
                columns: new[] { "year", "office_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ir_session_office_id_session_date",
                table: "ir_session",
                columns: new[] { "office_id", "session_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_partner_mobile_mobile_no_PartnerId",
                table: "partner_mobile",
                columns: new[] { "mobile_no", "PartnerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_partner_mobile_PartnerId",
                table: "partner_mobile",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ptnr_permitted_trdparty_assmnts_partner_id_assmt_id",
                table: "ptnr_permitted_trdparty_assmnts",
                columns: new[] { "partner_id", "assmt_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_res_partner_gn_division_id",
                table: "res_partner",
                column: "gn_division_id");

            migrationBuilder.CreateIndex(
                name: "res_partner_FK",
                table: "res_partner",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "res_partner_title_UN",
                table: "res_partner_title",
                column: "code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "business_taxes");

            migrationBuilder.DropTable(
                name: "cd_app_category");

            migrationBuilder.DropTable(
                name: "cd_bank_details");

            migrationBuilder.DropTable(
                name: "cd_gender");

            migrationBuilder.DropTable(
                name: "cd_languages");

            migrationBuilder.DropTable(
                name: "cd_month");

            migrationBuilder.DropTable(
                name: "cd_office");

            migrationBuilder.DropTable(
                name: "cd_selected_languages");

            migrationBuilder.DropTable(
                name: "cd_year");

            migrationBuilder.DropTable(
                name: "cdb_customer_types");

            migrationBuilder.DropTable(
                name: "ConfigApplication");

            migrationBuilder.DropTable(
                name: "document_sequence_numbers");

            migrationBuilder.DropTable(
                name: "EmailConfigurations");

            migrationBuilder.DropTable(
                name: "EmailOutBoxes");

            migrationBuilder.DropTable(
                name: "ir_session");

            migrationBuilder.DropTable(
                name: "partner_mobile");

            migrationBuilder.DropTable(
                name: "ptnr_permitted_trdparty_assmnts");

            migrationBuilder.DropTable(
                name: "res_business_places");

            migrationBuilder.DropTable(
                name: "res_partner_title");

            migrationBuilder.DropTable(
                name: "res_payment_method");

            migrationBuilder.DropTable(
                name: "res_payment_nbt");

            migrationBuilder.DropTable(
                name: "res_payment_vat");

            migrationBuilder.DropTable(
                name: "SMSConfigurations");

            migrationBuilder.DropTable(
                name: "SMSOutBoxes");

            migrationBuilder.DropTable(
                name: "tax_types");

            migrationBuilder.DropTable(
                name: "res_businesses");

            migrationBuilder.DropTable(
                name: "cd_sabha");

            migrationBuilder.DropTable(
                name: "cd_office_type");

            migrationBuilder.DropTable(
                name: "res_application");

            migrationBuilder.DropTable(
                name: "res_partner");

            migrationBuilder.DropTable(
                name: "cd_district");

            migrationBuilder.DropTable(
                name: "cd_gn_divisions");

            migrationBuilder.DropTable(
                name: "cd_province");
        }
    }
}
