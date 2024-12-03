using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "assmt_descriptions",
                columns: table => new
                {
                    assm_des_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assm_des_no = table.Column<int>(type: "int", nullable: false),
                    assm_des_text = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_des_sabha_id = table.Column<int>(type: "int", nullable: true),
                    assm_des_status = table.Column<int>(type: "int(1)", nullable: true),
                    assmt_des_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assm_des_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_des_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_des_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_descriptions", x => x.assm_des_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_processes",
                columns: table => new
                {
                    assmt_process_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_process_action_by = table.Column<int>(type: "int", nullable: true),
                    assmt_process_year = table.Column<int>(type: "int", nullable: false),
                    assmt_process_sbaha_id = table.Column<int>(type: "int", nullable: false),
                    assmt_process_type = table.Column<int>(type: "int", nullable: false),
                    assmt_proceed_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_process_backupkey = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_processes", x => x.assmt_process_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_property_types",
                columns: table => new
                {
                    assmt_property_type_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_property_type_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_property_type_no = table.Column<int>(type: "int(11)", nullable: false),
                    assmt_property_type_quarter_rate = table.Column<int>(type: "int(11)", precision: 5, scale: 2, nullable: false),
                    assmt_property_type_warrant_rate = table.Column<int>(type: "int(11)", precision: 5, scale: 2, nullable: false),
                    assmt_property_type_sabha_id = table.Column<int>(type: "int(11)", nullable: false),
                    assmt_property_type_status = table.Column<int>(type: "int(11)", nullable: true),
                    assmt_property_type_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assm_property_type_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_property_type_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_property_type_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_property_types", x => x.assmt_property_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_rates",
                columns: table => new
                {
                    assmt_rates_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_annual_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    assmt_quater_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_rates", x => x.assmt_rates_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_vote_payment_types",
                columns: table => new
                {
                    assmt_vt_pay_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assm_vt_pay_type_desc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assm_vt_pay_type_status = table.Column<int>(type: "int", nullable: true),
                    assm_vt_pay_type_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assm_vt_pay_type_cat_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assm_vt_pay_type_created_by = table.Column<int>(type: "int", nullable: true),
                    assm_vt_pay_type_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_vote_payment_types", x => x.assmt_vt_pay_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_wards",
                columns: table => new
                {
                    assmt_ward_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_ward_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_ward_no = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_ward_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_ward_office_id = table.Column<int>(type: "int", nullable: false),
                    assmt_ward_sabha_id = table.Column<int>(type: "int", nullable: false),
                    assmt_ward_status = table.Column<int>(type: "int", nullable: true),
                    assmt_ward_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_ward_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_ward_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_ward_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_wards", x => x.assmt_ward_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_vote_assigns",
                columns: table => new
                {
                    assmt_vtasgn_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_vtasgn_sabha_id = table.Column<int>(type: "int", nullable: false),
                    assmt_vtasgn_votetype_id = table.Column<int>(type: "int", nullable: false),
                    assmt_vtasgn_vote = table.Column<int>(type: "int", nullable: false),
                    assmt_vtasgn_status = table.Column<int>(type: "int", nullable: true),
                    assmt_vtasgn_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_vtasgn_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_vtasgn_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_vtasgn_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_vote_assigns", x => x.assmt_vtasgn_id);
                    table.ForeignKey(
                        name: "fk_assmt_vtasgn_vote_type",
                        column: x => x.assmt_vtasgn_votetype_id,
                        principalTable: "assmt_vote_payment_types",
                        principalColumn: "assmt_vt_pay_type_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_streets",
                columns: table => new
                {
                    assmt_street_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_street_name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_street_no = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_street_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_street_ward_id = table.Column<int>(type: "int", nullable: false),
                    assmt_street_status = table.Column<int>(type: "int", nullable: true),
                    assmt_streett_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assm_street_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_street_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_street_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_streets", x => x.assmt_street_id);
                    table.ForeignKey(
                        name: "fk_assmt_street_assm_ward",
                        column: x => x.assmt_street_ward_id,
                        principalTable: "assmt_wards",
                        principalColumn: "assmt_ward_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_assessments",
                columns: table => new
                {
                    assmt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_partner_id = table.Column<int>(type: "int", nullable: true),
                    assmt_sub_partner_id = table.Column<int>(type: "int", nullable: true),
                    assmt_street_id = table.Column<int>(type: "int", nullable: false),
                    assmt_property_type_id = table.Column<int>(type: "int(11)", nullable: false),
                    assmt_description_id = table.Column<int>(type: "int", nullable: false),
                    assmt_order = table.Column<int>(type: "int", nullable: false),
                    assmt_no = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_status = table.Column<int>(type: "int", nullable: true),
                    assmt_syn = table.Column<int>(type: "int", nullable: true),
                    assmt_comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_obsolete = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_office_id = table.Column<int>(type: "int", nullable: false),
                    assmt_sabha_id = table.Column<int>(type: "int", nullable: false),
                    assmt_is_warrant = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    assmt_is_partner_updated = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_is_sub_partner_updated = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_has_property_type_change_request = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_has_description_change_request = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_has_allocation_change_request = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_has_delete_request = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_assmt_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_assmt_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_assmt_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_assmt_updated_by = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_assessments", x => x.assmt_id);
                    table.ForeignKey(
                        name: "fk_assmt_description_id",
                        column: x => x.assmt_description_id,
                        principalTable: "assmt_descriptions",
                        principalColumn: "assm_des_id");
                    table.ForeignKey(
                        name: "fk_assmt_property_type",
                        column: x => x.assmt_property_type_id,
                        principalTable: "assmt_property_types",
                        principalColumn: "assmt_property_type_id");
                    table.ForeignKey(
                        name: "fk_assmt_street_id",
                        column: x => x.assmt_street_id,
                        principalTable: "assmt_streets",
                        principalColumn: "assmt_street_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assessment_documents",
                columns: table => new
                {
                    assmt_doc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_doc_type = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_doc_uri = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_doc_assessment_id = table.Column<int>(type: "int", nullable: true),
                    assmt_doc_status = table.Column<int>(type: "int", nullable: true),
                    wb_doc_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_doc_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_doc_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_doc_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessment_documents", x => x.assmt_doc_id);
                    table.ForeignKey(
                        name: "FK_assessment_documents_assmt_assessments_assmt_doc_assessment_~",
                        column: x => x.assmt_doc_assessment_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assm_temp_partners",
                columns: table => new
                {
                    assmt_tmp_ptnr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_tmp_ptnr_name = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_tmp_ptnr_nic = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_tmp_ptnr_mobile_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_tmp_ptnr_street1 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_tmp_ptnr_street2 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_tmp_ptnr_assmt_id = table.Column<int>(type: "int", nullable: false),
                    assmt_tmp_ptnr_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_tmp_ptnr_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_tmp_ptnr_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_tmp_ptnr_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assm_temp_partners", x => x.assmt_tmp_ptnr_id);
                    table.ForeignKey(
                        name: "FK_assm_temp_partners_assmt_assessments_assmt_tmp_ptnr_assmt_id",
                        column: x => x.assmt_tmp_ptnr_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assm_temp_sub_partner",
                columns: table => new
                {
                    assmt_tmp_sub_ptnr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_tmp_sub_ptnr_name = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_tmp_sub_ptnr_nic = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_tmp_sub_ptnr_mobile_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_tmp_sub_ptnr_street1 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_tmp_ptnr_street2 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_tmp_sub_ptnr_assmt_id = table.Column<int>(type: "int", nullable: false),
                    assmt_tmp_sub_ptnr_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_tmp_sub_ptnr_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_tmp_sub_ptnr_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_tmp_sub_ptnr_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assm_temp_sub_partner", x => x.assmt_tmp_sub_ptnr_id);
                    table.ForeignKey(
                        name: "FK_assm_temp_sub_partner_assmt_assessments_assmt_tmp_sub_ptnr_a~",
                        column: x => x.assmt_tmp_sub_ptnr_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_allocations",
                columns: table => new
                {
                    assmt_allocation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_allocation_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_allocation_changed_date = table.Column<DateOnly>(type: "date", nullable: true),
                    assmt_allocation_description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_allocation_assmt_id = table.Column<int>(type: "int", nullable: false),
                    assmt_allocation_status = table.Column<int>(type: "int(11)", nullable: true),
                    assmt_allocation_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_allocation_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_allocation_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_allocation_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_allocations", x => x.assmt_allocation_id);
                    table.ForeignKey(
                        name: "FK_assmt_allocations_assmt_assessments_assmt_allocation_assmt_id",
                        column: x => x.assmt_allocation_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_audit_logs",
                columns: table => new
                {
                    assmt_atl_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_atl_assmt_id = table.Column<int>(type: "int", nullable: false),
                    assmt_atl_action = table.Column<int>(type: "int", nullable: false),
                    assmt_atl_time_stamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_action_by = table.Column<int>(type: "int", nullable: true),
                    assmt_entity_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_audit_logs", x => x.assmt_atl_id);
                    table.ForeignKey(
                        name: "fk_atl_assmt_id",
                        column: x => x.assmt_atl_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_balance_history",
                columns: table => new
                {
                    assmt_bal_hstry_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_bal_hstry_assmt_id = table.Column<int>(type: "int", nullable: true),
                    assmt_bal_hstry_year = table.Column<int>(type: "int", nullable: false),
                    assmt_bal_hstry_start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    assmt_bal_hstry_end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    assmt_bal_hstry_excess_payment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_ly_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_ty_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_ty_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_over_payment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_discount_rate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_is_completed = table.Column<bool>(type: "tinyint(1)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_hstry_status = table.Column<int>(type: "int", nullable: true),
                    assmt_bal_hstry_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_bal_hstry_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_bal_hstry_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_bal_hstry_updated_by = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_balance_history", x => x.assmt_bal_hstry_id);
                    table.ForeignKey(
                        name: "fk_assmt_bal_hstry_id",
                        column: x => x.assmt_bal_hstry_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_balances",
                columns: table => new
                {
                    assmt_bal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_bal_assmt_id = table.Column<int>(type: "int", nullable: false),
                    assmt_bal_year = table.Column<int>(type: "int", nullable: false),
                    assmt_bal_start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    ExcessPayment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_ly_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_ty_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_ty_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_annual_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    assmt_bal_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_over_payment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_discount_rate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_bal_current_quarter = table.Column<int>(type: "int", nullable: true),
                    assmt_bal_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    assmt_bal_has_transaction = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    assmt_bal_status = table.Column<int>(type: "int", nullable: true),
                    assmt_bal_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_bal_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_bal_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_bal_updated_by = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_balances", x => x.assmt_bal_id);
                    table.ForeignKey(
                        name: "FK_assmt_balances_assmt_assessments_assmt_bal_assmt_id",
                        column: x => x.assmt_bal_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_description_logs",
                columns: table => new
                {
                    assmt_deslog_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_deslog_des_id = table.Column<int>(type: "int", nullable: false),
                    assmt_deslog_cmt = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_deslog_action_by = table.Column<int>(type: "int", nullable: true),
                    assmt_deslog_action_note = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_deslog_activate_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_deslog_assmt_id = table.Column<int>(type: "int", nullable: false),
                    assmt_deslog_status = table.Column<int>(type: "int", nullable: true),
                    assmt_deslog_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_deslog_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_deslog_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_deslog_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_description_logs", x => x.assmt_deslog_id);
                    table.ForeignKey(
                        name: "FK_assmt_description_logs_assmt_descriptions_assmt_deslog_des_id",
                        column: x => x.assmt_deslog_des_id,
                        principalTable: "assmt_descriptions",
                        principalColumn: "assm_des_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_deslog_assmt_id",
                        column: x => x.assmt_deslog_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_property_types_logs",
                columns: table => new
                {
                    assmt_proptypeslog_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_proptypeslog_des_id = table.Column<int>(type: "int(11)", nullable: false),
                    assmt_proptypeslog_cmt = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_proptypeslog_action_by = table.Column<int>(type: "int", nullable: true),
                    assmt_proptypeslog_action_note = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_proptypeslog_activate_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_proptypeslog_assmt_id = table.Column<int>(type: "int", nullable: false),
                    assmt_proptypeslog_status = table.Column<int>(type: "int", nullable: true),
                    assmt_proptypeslog_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_proptypeslog_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_proptypeslog_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_proptypeslog_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_property_types_logs", x => x.assmt_proptypeslog_id);
                    table.ForeignKey(
                        name: "FK_assmt_property_types_logs_assmt_property_types_assmt_proptyp~",
                        column: x => x.assmt_proptypeslog_des_id,
                        principalTable: "assmt_property_types",
                        principalColumn: "assmt_property_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_proptypes_log_assmt_id",
                        column: x => x.assmt_proptypeslog_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_transactions",
                columns: table => new
                {
                    assmt_tr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_tr_date_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_tr_type = table.Column<int>(type: "int", nullable: false),
                    assmt_tr_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_ly_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_ty_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_ty_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_q1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_q2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_q3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_q4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_rn_overpay = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_rn_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_rn_total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_tr_assmt_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_transactions", x => x.assmt_tr_id);
                    table.ForeignKey(
                        name: "FK_assmt_transactions_assmt_assessments_assmt_tr_assmt_id",
                        column: x => x.assmt_tr_assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_allocation_logs",
                columns: table => new
                {
                    assmt_alg_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_allocation_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_alg_from_date = table.Column<DateOnly>(type: "date", nullable: true),
                    assmt_alg_to_date = table.Column<DateOnly>(type: "date", nullable: true),
                    assmt_alg_description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assmt_alg_allocation_id = table.Column<int>(type: "int", nullable: false),
                    assmt_alg_status = table.Column<int>(type: "int(11)", nullable: true),
                    assmt_alg_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assmt_alg_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    assmt_alg_created_by = table.Column<int>(type: "int", nullable: true),
                    assmt_alg_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_allocation_logs", x => x.assmt_alg_id);
                    table.ForeignKey(
                        name: "fk_assmt_alg_allocation_id",
                        column: x => x.assmt_alg_allocation_id,
                        principalTable: "assmt_allocations",
                        principalColumn: "assmt_allocation_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_qh1",
                columns: table => new
                {
                    assmt_qh1_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_qh1_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh1_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh1_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh1_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh1_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh1_warrant_method = table.Column<int>(type: "int", nullable: true),
                    assmt_qh1_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_qh1_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_qh1_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_qh1_assmt_balance_hstry_id = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_qh1", x => x.assmt_qh1_id);
                    table.ForeignKey(
                        name: "FK_assmt_qh1_assmt_balance_history_assmt_qh1_assmt_balance_hstr~",
                        column: x => x.assmt_qh1_assmt_balance_hstry_id,
                        principalTable: "assmt_balance_history",
                        principalColumn: "assmt_bal_hstry_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_qh2",
                columns: table => new
                {
                    assmt_qh2_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_qh2_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh2_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh2_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh2_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh2_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh2_warrant_method = table.Column<int>(type: "int", nullable: true),
                    assmt_qh2_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_qh2_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_qh2_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_qh2_assmt_balance_hstry_id = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_qh2", x => x.assmt_qh2_id);
                    table.ForeignKey(
                        name: "FK_assmt_qh2_assmt_balance_history_assmt_qh2_assmt_balance_hstr~",
                        column: x => x.assmt_qh2_assmt_balance_hstry_id,
                        principalTable: "assmt_balance_history",
                        principalColumn: "assmt_bal_hstry_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_qh3",
                columns: table => new
                {
                    assmt_qh3_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_qh3_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh3_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh3_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh3_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh3_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh3_warrant_method = table.Column<int>(type: "int", nullable: true),
                    assmt_qh3_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_qh3_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_qh3_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_qh3_assmt_balance_hstry_id = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_qh3", x => x.assmt_qh3_id);
                    table.ForeignKey(
                        name: "FK_assmt_qh3_assmt_balance_history_assmt_qh3_assmt_balance_hstr~",
                        column: x => x.assmt_qh3_assmt_balance_hstry_id,
                        principalTable: "assmt_balance_history",
                        principalColumn: "assmt_bal_hstry_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_qh4",
                columns: table => new
                {
                    assmt_qh4_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_qh4_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh4_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh4_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh4_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh4_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_qh4_warrant_method = table.Column<int>(type: "int", nullable: true),
                    assmt_qh4_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_qh4_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_qh4_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_qh4_assmt_balance_hstry_id = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_qh4", x => x.assmt_qh4_id);
                    table.ForeignKey(
                        name: "FK_assmt_qh4_assmt_balance_history_assmt_qh4_assmt_balance_hstr~",
                        column: x => x.assmt_qh4_assmt_balance_hstry_id,
                        principalTable: "assmt_balance_history",
                        principalColumn: "assmt_bal_hstry_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_q1",
                columns: table => new
                {
                    assmt_q1_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_q1_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q1_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q1_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q1_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q1_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q1_warrant_method = table.Column<int>(type: "int", nullable: true),
                    assmt_q1_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_q1_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_q1_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_q1_is_over = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_q1_assmt_balance_id = table.Column<int>(type: "int", nullable: false),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_q1", x => x.assmt_q1_id);
                    table.ForeignKey(
                        name: "FK_assmt_q1_assmt_balances_assmt_q1_assmt_balance_id",
                        column: x => x.assmt_q1_assmt_balance_id,
                        principalTable: "assmt_balances",
                        principalColumn: "assmt_bal_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_q2",
                columns: table => new
                {
                    assmt_q2_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_q2_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q2_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q2_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q2_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q2_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q2_warrant_method = table.Column<int>(type: "int", nullable: true),
                    assmt_q2_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_q2_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_q2_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_q2_is_over = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_q2_assmt_balance_id = table.Column<int>(type: "int", nullable: false),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_q2", x => x.assmt_q2_id);
                    table.ForeignKey(
                        name: "FK_assmt_q2_assmt_balances_assmt_q2_assmt_balance_id",
                        column: x => x.assmt_q2_assmt_balance_id,
                        principalTable: "assmt_balances",
                        principalColumn: "assmt_bal_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_q3",
                columns: table => new
                {
                    assmt_q3_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_q3_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q3_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q3_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q3_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q3_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q3_warrant_method = table.Column<int>(type: "int", nullable: true),
                    assmt_q3_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_q3_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_q3_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_q3_is_over = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_q3_assmt_balance_id = table.Column<int>(type: "int", nullable: false),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_q3", x => x.assmt_q3_id);
                    table.ForeignKey(
                        name: "FK_assmt_q3_assmt_balances_assmt_q3_assmt_balance_id",
                        column: x => x.assmt_q3_assmt_balance_id,
                        principalTable: "assmt_balances",
                        principalColumn: "assmt_bal_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_q4",
                columns: table => new
                {
                    assmt_q4_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_q4_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q4_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q4_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q4_discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q4_warrant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_q4_warrant_method = table.Column<int>(type: "int", nullable: true),
                    assmt_q4_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_q4_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_q4_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_q4_is_over = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    assmt_q4_assmt_balance_id = table.Column<int>(type: "int", nullable: false),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_q4", x => x.assmt_q4_id);
                    table.ForeignKey(
                        name: "FK_assmt_q4_assmt_balances_assmt_q4_assmt_balance_id",
                        column: x => x.assmt_q4_assmt_balance_id,
                        principalTable: "assmt_balances",
                        principalColumn: "assmt_bal_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_assessment_documents_assmt_doc_assessment_id",
                table: "assessment_documents",
                column: "assmt_doc_assessment_id");

            migrationBuilder.CreateIndex(
                name: "IX_assm_temp_partners_assmt_tmp_ptnr_assmt_id",
                table: "assm_temp_partners",
                column: "assmt_tmp_ptnr_assmt_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assm_temp_sub_partner_assmt_tmp_sub_ptnr_assmt_id",
                table: "assm_temp_sub_partner",
                column: "assmt_tmp_sub_ptnr_assmt_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_allocation_logs_assmt_alg_allocation_id",
                table: "assmt_allocation_logs",
                column: "assmt_alg_allocation_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_allocations_assmt_allocation_assmt_id",
                table: "assmt_allocations",
                column: "assmt_allocation_assmt_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_assessments_assmt_description_id",
                table: "assmt_assessments",
                column: "assmt_description_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_assessments_assmt_no_assmt_street_id",
                table: "assmt_assessments",
                columns: new[] { "assmt_no", "assmt_street_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_assessments_assmt_property_type_id",
                table: "assmt_assessments",
                column: "assmt_property_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_assessments_assmt_street_id",
                table: "assmt_assessments",
                column: "assmt_street_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_audit_logs_assmt_atl_assmt_id",
                table: "assmt_audit_logs",
                column: "assmt_atl_assmt_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_balance_history_assmt_bal_hstry_assmt_id",
                table: "assmt_balance_history",
                column: "assmt_bal_hstry_assmt_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_balances_assmt_bal_assmt_id",
                table: "assmt_balances",
                column: "assmt_bal_assmt_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_description_logs_assmt_deslog_assmt_id",
                table: "assmt_description_logs",
                column: "assmt_deslog_assmt_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_description_logs_assmt_deslog_des_id",
                table: "assmt_description_logs",
                column: "assmt_deslog_des_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_descriptions_assm_des_no_assmt_des_sabha_id",
                table: "assmt_descriptions",
                columns: new[] { "assm_des_no", "assmt_des_sabha_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_processes_assmt_process_year_assmt_process_sbaha_id_as~",
                table: "assmt_processes",
                columns: new[] { "assmt_process_year", "assmt_process_sbaha_id", "assmt_process_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_property_types_assmt_property_type_no_assmt_property_t~",
                table: "assmt_property_types",
                columns: new[] { "assmt_property_type_no", "assmt_property_type_sabha_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_property_types_logs_assmt_proptypeslog_assmt_id",
                table: "assmt_property_types_logs",
                column: "assmt_proptypeslog_assmt_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_property_types_logs_assmt_proptypeslog_des_id",
                table: "assmt_property_types_logs",
                column: "assmt_proptypeslog_des_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_q1_assmt_q1_assmt_balance_id",
                table: "assmt_q1",
                column: "assmt_q1_assmt_balance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_q2_assmt_q2_assmt_balance_id",
                table: "assmt_q2",
                column: "assmt_q2_assmt_balance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_q3_assmt_q3_assmt_balance_id",
                table: "assmt_q3",
                column: "assmt_q3_assmt_balance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_q4_assmt_q4_assmt_balance_id",
                table: "assmt_q4",
                column: "assmt_q4_assmt_balance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_qh1_assmt_qh1_assmt_balance_hstry_id",
                table: "assmt_qh1",
                column: "assmt_qh1_assmt_balance_hstry_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_qh2_assmt_qh2_assmt_balance_hstry_id",
                table: "assmt_qh2",
                column: "assmt_qh2_assmt_balance_hstry_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_qh3_assmt_qh3_assmt_balance_hstry_id",
                table: "assmt_qh3",
                column: "assmt_qh3_assmt_balance_hstry_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_qh4_assmt_qh4_assmt_balance_hstry_id",
                table: "assmt_qh4",
                column: "assmt_qh4_assmt_balance_hstry_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_streets_assmt_street_no_assmt_street_ward_id",
                table: "assmt_streets",
                columns: new[] { "assmt_street_no", "assmt_street_ward_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_streets_assmt_street_ward_id",
                table: "assmt_streets",
                column: "assmt_street_ward_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_transactions_assmt_tr_assmt_id",
                table: "assmt_transactions",
                column: "assmt_tr_assmt_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_vote_assigns_assmt_vtasgn_sabha_id_assmt_vtasgn_votety~",
                table: "assmt_vote_assigns",
                columns: new[] { "assmt_vtasgn_sabha_id", "assmt_vtasgn_votetype_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_vote_assigns_assmt_vtasgn_votetype_id",
                table: "assmt_vote_assigns",
                column: "assmt_vtasgn_votetype_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_wards_assmt_ward_no_assmt_ward_sabha_id",
                table: "assmt_wards",
                columns: new[] { "assmt_ward_no", "assmt_ward_sabha_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assessment_documents");

            migrationBuilder.DropTable(
                name: "assm_temp_partners");

            migrationBuilder.DropTable(
                name: "assm_temp_sub_partner");

            migrationBuilder.DropTable(
                name: "assmt_allocation_logs");

            migrationBuilder.DropTable(
                name: "assmt_audit_logs");

            migrationBuilder.DropTable(
                name: "assmt_description_logs");

            migrationBuilder.DropTable(
                name: "assmt_processes");

            migrationBuilder.DropTable(
                name: "assmt_property_types_logs");

            migrationBuilder.DropTable(
                name: "assmt_q1");

            migrationBuilder.DropTable(
                name: "assmt_q2");

            migrationBuilder.DropTable(
                name: "assmt_q3");

            migrationBuilder.DropTable(
                name: "assmt_q4");

            migrationBuilder.DropTable(
                name: "assmt_qh1");

            migrationBuilder.DropTable(
                name: "assmt_qh2");

            migrationBuilder.DropTable(
                name: "assmt_qh3");

            migrationBuilder.DropTable(
                name: "assmt_qh4");

            migrationBuilder.DropTable(
                name: "assmt_rates");

            migrationBuilder.DropTable(
                name: "assmt_transactions");

            migrationBuilder.DropTable(
                name: "assmt_vote_assigns");

            migrationBuilder.DropTable(
                name: "assmt_allocations");

            migrationBuilder.DropTable(
                name: "assmt_balances");

            migrationBuilder.DropTable(
                name: "assmt_balance_history");

            migrationBuilder.DropTable(
                name: "assmt_vote_payment_types");

            migrationBuilder.DropTable(
                name: "assmt_assessments");

            migrationBuilder.DropTable(
                name: "assmt_descriptions");

            migrationBuilder.DropTable(
                name: "assmt_property_types");

            migrationBuilder.DropTable(
                name: "assmt_streets");

            migrationBuilder.DropTable(
                name: "assmt_wards");
        }
    }
}
