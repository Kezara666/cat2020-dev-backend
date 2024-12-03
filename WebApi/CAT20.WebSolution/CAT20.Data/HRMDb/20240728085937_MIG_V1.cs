using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.HRMDb
{
    public partial class MIG_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "hr_advance_b_type_data",
                columns: table => new
                {
                    hr_ab_type_data_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_ab_description = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_ab_interest = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    hr_ab_max_instalment = table.Column<int>(type: "int", nullable: true),
                    hr_ab_has_interest = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    hr_ab_acc_system_ver_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_advance_b_type_data", x => x.hr_ab_type_data_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_agrahara_category_data",
                columns: table => new
                {
                    hr_pf_ac_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_ac_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_ac_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_agrahara_category_data", x => x.hr_pf_ac_dt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_appointment_type_data",
                columns: table => new
                {
                    hr_pf_at_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_at_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_at_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_appointment_type_data", x => x.hr_pf_at_dt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_carder_status_data",
                columns: table => new
                {
                    hr_pf_cs_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_cs_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_cs_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_carder_status_data", x => x.hr_pf_cs_dt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_class_level_data",
                columns: table => new
                {
                    hr_pf_cl_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_cl_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_cl_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_class_level_data", x => x.hr_pf_cl_dt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_employee_type_data",
                columns: table => new
                {
                    hr_pf_emp_typ_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_emp_typ_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_typ_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_employee_type_data", x => x.hr_pf_emp_typ_dt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_funding_source_data",
                columns: table => new
                {
                    hr_pf_fs_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_fs_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_fs_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_funding_source_data", x => x.hr_pf_fs_dt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_grade_level_data",
                columns: table => new
                {
                    hr_pf_gl_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_gl_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_gl_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_grade_level_data", x => x.hr_pf_gl_dt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_salary_structure_data",
                columns: table => new
                {
                    hr_pf_salst_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_salst_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_salst_dt_salary_code = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_salst_dt_initial_step = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    hr_pf_salst_dt_years1 = table.Column<int>(type: "int", nullable: true),
                    hr_pf_salst_dt_first_slab = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    hr_pf_salst_dt_years2 = table.Column<int>(type: "int", nullable: true),
                    hr_pf_salst_dt_second_slab = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    hr_pf_salst_dt_years3 = table.Column<int>(type: "int", nullable: true),
                    hr_pf_salst_dt_third_slab = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    hr_pf_salst_dt_years4 = table.Column<int>(type: "int", nullable: true),
                    hr_pf_salst_dt_fourth_slab = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    hr_pf_salst_dt_maximum = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    hr_pf_salst_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_salary_structure_data", x => x.hr_pf_salst_dt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_supporting_doc_type_data",
                columns: table => new
                {
                    hr_pf_sdt_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_sdt_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_sdt_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_supporting_doc_type_data", x => x.hr_pf_sdt_dt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hrm_sequence_numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    year = table.Column<int>(type: "int", nullable: false),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    module_type = table.Column<int>(type: "int", nullable: false),
                    prefix = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hrm_sequence_numbers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_advance_b_type_ledger_mapping",
                columns: table => new
                {
                    hr_ab_type_ledger_mapping_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_ab_type_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_ledger_code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_ab_loan_ledger_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_prefix = table.Column<string>(type: "varchar(50)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_ab_last_index = table.Column<int>(type: "int", nullable: false),
                    hr_ab_sabha_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_advance_b_type_ledger_mapping", x => x.hr_ab_type_ledger_mapping_id);
                    table.ForeignKey(
                        name: "FK_hr_advance_b_type_ledger_mapping_hr_advance_b_type_data_hr_a~",
                        column: x => x.hr_ab_type_id,
                        principalTable: "hr_advance_b_type_data",
                        principalColumn: "hr_ab_type_data_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_employee",
                columns: table => new
                {
                    hr_pf_emp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_emp_type = table.Column<int>(type: "int", nullable: false),
                    hr_pf_emp_carder_status = table.Column<int>(type: "int", nullable: false),
                    hr_pf_emp_nic_no = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_passport_no = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_personal_file_no = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_no = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_pay_no = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_title = table.Column<int>(type: "int", nullable: false),
                    hr_pf_emp_initials = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_first_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_middle_name = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_last_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_full_name = table.Column<string>(type: "varchar(500)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_gender = table.Column<int>(type: "int", nullable: false),
                    hr_pf_emp_dob = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    hr_pf_emp_civil_status = table.Column<int>(type: "int", nullable: false),
                    hr_pf_emp_married_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    hr_pf_emp_railway_warrant = table.Column<int>(type: "int", nullable: true),
                    hr_pf_emp_mobile_no = table.Column<string>(type: "varchar(10)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_personal_email = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_photo_path = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_emp_sabha_id = table.Column<int>(type: "int", nullable: false),
                    hr_pf_emp_office_id = table.Column<int>(type: "int", nullable: false),
                    hr_pf_emp_programme_id = table.Column<int>(type: "int", nullable: false),
                    hr_pf_emp_project_id = table.Column<int>(type: "int", nullable: true),
                    hr_pf_emp_sub_project_id = table.Column<int>(type: "int", nullable: true),
                    hr_pf_emp_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_pf_emp_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_pf_emp_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_emp_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_emp_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_employee", x => x.hr_pf_emp_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_emp_cs_dt_id",
                        column: x => x.hr_pf_emp_carder_status,
                        principalTable: "hr_pf_carder_status_data",
                        principalColumn: "hr_pf_cs_dt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hr_pf_emp_typ_dt_id",
                        column: x => x.hr_pf_emp_type,
                        principalTable: "hr_pf_employee_type_data",
                        principalColumn: "hr_pf_emp_typ_dt_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_service_type_data",
                columns: table => new
                {
                    hr_pf_st_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_st_sctgy_dt_id = table.Column<int>(type: "int", nullable: false),
                    hr_pf_st_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_st_dt_code = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_st_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_service_type_data", x => x.hr_pf_st_dt_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_ssd_dt_id",
                        column: x => x.hr_pf_st_sctgy_dt_id,
                        principalTable: "hr_pf_salary_structure_data",
                        principalColumn: "hr_pf_salst_dt_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_advance_b",
                columns: table => new
                {
                    hr_ab_loan_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_ab_employee_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_type_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_ledger_acc_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_is_new = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    hr_ab_voucher_id = table.Column<int>(type: "int", nullable: true),
                    hr_ab_voucher_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_ab_number = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_ab_loan_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    hr_ab_interest_percentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    hr_ab_installment_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    hr_ab_odd_installment_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    hr_ab_interest_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    hr_ab_number_of_installments = table.Column<int>(type: "int", nullable: false),
                    hr_ab_remaining_installments = table.Column<int>(type: "int", nullable: false),
                    hr_ab_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    hr_ab_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    hr_ab_bank_acc_id = table.Column<int>(type: "int", nullable: true),
                    hr_ab_description = table.Column<string>(type: "varchar(500)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_ab_guarantor1_id = table.Column<int>(type: "int", nullable: true),
                    hr_ab_guarantor2_id = table.Column<int>(type: "int", nullable: true),
                    hr_ab_sabha_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_office_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_advance_b_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    hr_ab_transfer_in_or_out_date = table.Column<DateOnly>(type: "date", nullable: true),
                    hr_ab_transfer_in_or_out_balance_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    hr_ab_deceased_date = table.Column<DateOnly>(type: "date", nullable: true),
                    hr_ab_deceased_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    hr_ab_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_ab_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_ab_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_ab_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_ab_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    hr_ab_system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_advance_b", x => x.hr_ab_loan_id);
                    table.ForeignKey(
                        name: "FK_hr_advance_b_hr_advance_b_type_data_hr_ab_type_id",
                        column: x => x.hr_ab_type_id,
                        principalTable: "hr_advance_b_type_data",
                        principalColumn: "hr_ab_type_data_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hr_advance_b_hr_pf_employee_hr_ab_employee_id",
                        column: x => x.hr_ab_employee_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hr_advance_b_hr_pf_employee_hr_ab_guarantor1_id",
                        column: x => x.hr_ab_guarantor1_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hr_advance_b_hr_pf_employee_hr_ab_guarantor2_id",
                        column: x => x.hr_ab_guarantor2_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_address",
                columns: table => new
                {
                    hr_pf_addr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_addr_employee_id = table.Column<int>(type: "int", nullable: true),
                    hr_pf_addr_typ_id = table.Column<int>(type: "int", nullable: false),
                    hr_pf_addr_line1 = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_addr_line2 = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_addr_city_town = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_addr_gn_division = table.Column<int>(type: "int", nullable: false),
                    hr_pf_addr_postal_code = table.Column<int>(type: "int", nullable: true),
                    hr_pf_addr_telephone = table.Column<string>(type: "varchar(10)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_addr_fax = table.Column<string>(type: "varchar(10)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_addr_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_pf_addr_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_pf_addr_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_addr_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_addr_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_address", x => x.hr_pf_addr_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_addrs_emp",
                        column: x => x.hr_pf_addr_employee_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_child_info",
                columns: table => new
                {
                    hr_pf_ci_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_ci_emp_id = table.Column<int>(type: "int", nullable: true),
                    hr_pf_ci_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_ci_dob = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    hr_pf_ci_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_pf_ci_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_pf_ci_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_ci_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_ci_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_child_info", x => x.hr_pf_ci_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_ci_emp",
                        column: x => x.hr_pf_ci_emp_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_net_salary_agent",
                columns: table => new
                {
                    hr_pf_nt_agnt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_nt_agnt_emp_id = table.Column<int>(type: "int", nullable: true),
                    hr_pf_nt_agnt_bank_name = table.Column<int>(type: "int", nullable: false),
                    hr_pf_nt_agnt_bank_code = table.Column<int>(type: "int", nullable: false),
                    hr_pf_nt_agnt_branch_code = table.Column<int>(type: "int", nullable: false),
                    hr_pf_nt_agnt_account_no = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_nt_agnt_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_pf_nt_agnt_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_pf_nt_agnt_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_nt_agnt_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_nt_agnt_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_net_salary_agent", x => x.hr_pf_nt_agnt_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_nt_agnt_emp",
                        column: x => x.hr_pf_nt_agnt_emp_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_other_remittance_agent",
                columns: table => new
                {
                    hr_pf_or_agnt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_or_agnt_emp_id = table.Column<int>(type: "int", nullable: true),
                    hr_pf_or_agnt_bank_name = table.Column<int>(type: "int", nullable: false),
                    hr_pf_or_agnt_bank_code = table.Column<int>(type: "int", nullable: false),
                    hr_pf_or_agnt_branch_code = table.Column<int>(type: "int", nullable: false),
                    hr_pf_or_agnt_account_no = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_or_agnt_agre_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    hr_pf_or_agnt_agre_min_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    hr_pf_or_agnt_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_pf_or_agnt_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_pf_or_agnt_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_or_agnt_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_or_agnt_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_other_remittance_agent", x => x.hr_pf_or_agnt_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_or_agnt_emp",
                        column: x => x.hr_pf_or_agnt_emp_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_salary_info",
                columns: table => new
                {
                    hr_pf_saly_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_saly_emp_id = table.Column<int>(type: "int", nullable: true),
                    hr_pf_saly_w_op_rates = table.Column<int>(type: "int", nullable: true),
                    hr_pf_saly_pspf_rate = table.Column<int>(type: "int", nullable: true),
                    hr_pf_emp_pspf_rate = table.Column<int>(type: "int", nullable: true),
                    hr_pf_la_pspf_rate = table.Column<int>(type: "int", nullable: true),
                    hr_pf_saly_ot_rate = table.Column<int>(type: "int", nullable: true),
                    hr_pf_saly_days_pay_rate = table.Column<int>(type: "int", nullable: true),
                    hr_pf_saly_etf_rate = table.Column<int>(type: "int", nullable: true),
                    hr_pf_saly_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_pf_saly_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_pf_saly_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_saly_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_saly_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_salary_info", x => x.hr_pf_saly_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_salin_emp",
                        column: x => x.hr_pf_saly_emp_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_spouser_info",
                columns: table => new
                {
                    hr_pf_si_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_si_emp_id = table.Column<int>(type: "int", nullable: true),
                    hr_pf_si_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_si_dob = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    hr_pf_si_job_title = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_si_work_place = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_si_gn_division = table.Column<int>(type: "int", nullable: true),
                    hr_pf_si_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_pf_si_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_pf_si_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_si_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_si_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_spouser_info", x => x.hr_pf_si_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_si_emp",
                        column: x => x.hr_pf_si_emp_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_supporting_document",
                columns: table => new
                {
                    hr_pf_sd_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_sd_emp_id = table.Column<int>(type: "int", nullable: true),
                    hr_pf_sd_type_id = table.Column<int>(type: "int", nullable: false),
                    hr_pf_sd_document_path = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_sd_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_pf_sd_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_pf_sd_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_sd_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_sd_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_supporting_document", x => x.hr_pf_sd_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_sd_emp",
                        column: x => x.hr_pf_sd_emp_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hr_pf_sdt_dt_id",
                        column: x => x.hr_pf_sd_type_id,
                        principalTable: "hr_pf_supporting_doc_type_data",
                        principalColumn: "hr_pf_sdt_dt_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_job_title_data",
                columns: table => new
                {
                    hr_pf_jt_dt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_jt_st_dt_id = table.Column<int>(type: "int", nullable: false),
                    hr_pf_jt_dt_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_jt_dt_code = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_tj_dt_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_job_title_data", x => x.hr_pf_jt_dt_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_std_dt_id",
                        column: x => x.hr_pf_jt_st_dt_id,
                        principalTable: "hr_pf_service_type_data",
                        principalColumn: "hr_pf_st_dt_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_advance_b_attachment",
                columns: table => new
                {
                    hr_ab_loan_attachment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_ab_loan_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_file_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_ab_file_path = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_ab_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_ab_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_ab_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_ab_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_ab_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AdvanceBId = table.Column<int>(type: "int", nullable: true),
                    hr_ab_system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_advance_b_attachment", x => x.hr_ab_loan_attachment_id);
                    table.ForeignKey(
                        name: "FK_hr_advance_b_attachment_hr_advance_b_AdvanceBId",
                        column: x => x.AdvanceBId,
                        principalTable: "hr_advance_b",
                        principalColumn: "hr_ab_loan_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_advance_b_settlement",
                columns: table => new
                {
                    hr_ab_settlement_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_ab_advance_b_id = table.Column<int>(type: "int", nullable: false),
                    hr_ab_settlemt_type = table.Column<int>(type: "int", nullable: true),
                    hr_ab_settlement_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_ab_pay_month = table.Column<int>(type: "int", nullable: false),
                    hr_ab_installment_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    hr_ab_interest_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    hr_ab_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    hr_ab_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_ab_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_ab_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_ab_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_ab_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    hr_ab_system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_advance_b_settlement", x => x.hr_ab_settlement_id);
                    table.ForeignKey(
                        name: "FK_hr_advance_b_settlement_hr_advance_b_hr_ab_advance_b_id",
                        column: x => x.hr_ab_advance_b_id,
                        principalTable: "hr_advance_b",
                        principalColumn: "hr_ab_loan_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "hr_pf_service_info",
                columns: table => new
                {
                    hr_pf_si_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hr_pf_si_emp_id = table.Column<int>(type: "int", nullable: true),
                    hr_pf_si_salary_structure_id = table.Column<int>(type: "int", nullable: false),
                    hr_pf_si_service_id = table.Column<int>(type: "int", nullable: false),
                    hr_pf_si_level = table.Column<int>(type: "int", nullable: false),
                    hr_pf_si_post_name = table.Column<int>(type: "int", nullable: false),
                    hr_pf_si_class = table.Column<int>(type: "int", nullable: false),
                    hr_pf_si_grade = table.Column<int>(type: "int", nullable: false),
                    hr_pf_si_salary_step = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_si_salary_step_level = table.Column<int>(type: "int", nullable: false),
                    hr_pf_si_basic_salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    hr_pf_si_increment_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    hr_pf_si_appointment_type = table.Column<int>(type: "int", nullable: false),
                    hr_pf_si_appointment_letter_number = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_si_appointment_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    hr_pf_si_funding_source = table.Column<int>(type: "int", nullable: false),
                    hr_pf_si_reimbursed_pct = table.Column<int>(type: "int", nullable: false),
                    hr_pf_si_agrahara_category = table.Column<int>(type: "int", nullable: true),
                    hr_pf_si_pension_number = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_si_wop_number = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_si_pspf_number = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hr_pf_si_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    hr_pf_si_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    hr_pf_si_created_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_si_updated_by = table.Column<int>(type: "int", nullable: true),
                    hr_pf_si_status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_pf_service_info", x => x.hr_pf_si_id);
                    table.ForeignKey(
                        name: "fk_hr_pf_serin_emp",
                        column: x => x.hr_pf_si_emp_id,
                        principalTable: "hr_pf_employee",
                        principalColumn: "hr_pf_emp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hr_pf_si_ac_id",
                        column: x => x.hr_pf_si_agrahara_category,
                        principalTable: "hr_pf_agrahara_category_data",
                        principalColumn: "hr_pf_ac_dt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hr_pf_si_Appt_dt_id",
                        column: x => x.hr_pf_si_appointment_type,
                        principalTable: "hr_pf_appointment_type_data",
                        principalColumn: "hr_pf_at_dt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hr_pf_si_cl_id",
                        column: x => x.hr_pf_si_class,
                        principalTable: "hr_pf_class_level_data",
                        principalColumn: "hr_pf_cl_dt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hr_pf_si_fs_dt_id",
                        column: x => x.hr_pf_si_funding_source,
                        principalTable: "hr_pf_funding_source_data",
                        principalColumn: "hr_pf_fs_dt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hr_pf_si_gl_id",
                        column: x => x.hr_pf_si_grade,
                        principalTable: "hr_pf_grade_level_data",
                        principalColumn: "hr_pf_gl_dt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hr_pf_si_jt_id",
                        column: x => x.hr_pf_si_post_name,
                        principalTable: "hr_pf_job_title_data",
                        principalColumn: "hr_pf_jt_dt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hr_pf_si_sc_id",
                        column: x => x.hr_pf_si_salary_structure_id,
                        principalTable: "hr_pf_salary_structure_data",
                        principalColumn: "hr_pf_salst_dt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hr_pf_si_st_id",
                        column: x => x.hr_pf_si_service_id,
                        principalTable: "hr_pf_service_type_data",
                        principalColumn: "hr_pf_st_dt_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.InsertData(
                table: "hr_pf_agrahara_category_data",
                columns: new[] { "hr_pf_ac_dt_id", "hr_pf_ac_dt_name" },
                values: new object[,]
                {
                    { 1, "Gold" },
                    { 2, "Silver" },
                    { 3, "Bronze" }
                });

            migrationBuilder.InsertData(
                table: "hr_pf_appointment_type_data",
                columns: new[] { "hr_pf_at_dt_id", "hr_pf_at_dt_name" },
                values: new object[,]
                {
                    { 1, "Permanent" },
                    { 2, "Casual" },
                    { 3, "Contract" },
                    { 4, "Temporary" },
                    { 5, "Adesaka" },
                    { 6, "Acting" },
                    { 7, "Itukirima" }
                });

            migrationBuilder.InsertData(
                table: "hr_pf_carder_status_data",
                columns: new[] { "hr_pf_cs_dt_id", "hr_pf_cs_dt_name" },
                values: new object[,]
                {
                    { 1, "Local Authority" },
                    { 2, "Local Government" },
                    { 3, "Attached from Other Office" },
                    { 4, "Attached to Other Office" }
                });

            migrationBuilder.InsertData(
                table: "hr_pf_class_level_data",
                columns: new[] { "hr_pf_cl_dt_id", "hr_pf_cl_dt_name" },
                values: new object[,]
                {
                    { 1, "Level I" },
                    { 2, "Level II" },
                    { 3, "Level III" }
                });

            migrationBuilder.InsertData(
                table: "hr_pf_employee_type_data",
                columns: new[] { "hr_pf_emp_typ_dt_id", "hr_pf_emp_typ_dt_name" },
                values: new object[,]
                {
                    { 1, "New Appointment" },
                    { 2, "Existing" },
                    { 3, "Transfer In" },
                    { 4, "Temporary Hold" }
                });

            migrationBuilder.InsertData(
                table: "hr_pf_funding_source_data",
                columns: new[] { "hr_pf_fs_dt_id", "hr_pf_fs_dt_name" },
                values: new object[,]
                {
                    { 1, "Reimbursed" },
                    { 2, "Council Fund" }
                });

            migrationBuilder.InsertData(
                table: "hr_pf_grade_level_data",
                columns: new[] { "hr_pf_gl_dt_id", "hr_pf_gl_dt_name" },
                values: new object[,]
                {
                    { 1, "Level I" },
                    { 2, "Level II" },
                    { 3, "Level III" }
                });

            migrationBuilder.InsertData(
                table: "hr_pf_salary_structure_data",
                columns: new[] { "hr_pf_salst_dt_id", "hr_pf_salst_dt_first_slab", "hr_pf_salst_dt_fourth_slab", "hr_pf_salst_dt_initial_step", "hr_pf_salst_dt_maximum", "hr_pf_salst_dt_salary_code", "hr_pf_salst_dt_second_slab", "hr_pf_salst_dt_name", "hr_pf_salst_dt_status", "hr_pf_salst_dt_third_slab", "hr_pf_salst_dt_years1", "hr_pf_salst_dt_years2", "hr_pf_salst_dt_years3", "hr_pf_salst_dt_years4" },
                values: new object[,]
                {
                    { 1, 250.00m, 330.00m, 24250.00m, 36410.00m, "PL 1-2016", 270.00m, "Primary Level Unskilled", 1, 300.00m, 10, 10, 10, 12 },
                    { 2, 270.00m, 350.00m, 25250.00m, 38450.00m, "PL 2-2016", 300.00m, "Primary Level Semi-skilled", 1, 330.00m, 10, 10, 10, 12 },
                    { 3, 270.00m, 350.00m, 25790.00m, 38990.00m, "PL 3-2016", 300.00m, "Primary Level Skilled", 1, 330.00m, 10, 10, 10, 12 },
                    { 4, 300.00m, 660.00m, 27140.00m, 45540.00m, "MN 1-2016", 350.00m, "Management Assistants Seg 2", 1, 495.00m, 10, 11, 10, 10 },
                    { 5, 300.00m, 660.00m, 28940.00m, 47990.00m, "MN 2-2016", 350.00m, "Management Assistants Seg 1", 1, 560.00m, 10, 11, 10, 10 },
                    { 6, 445.00m, 750.00m, 31040.00m, 57550.00m, "MN 3-2016", 660.00m, "MA Supervisory Non-tech / Tech", 1, 730.00m, 10, 11, 10, 10 },
                    { 7, 445.00m, 750.00m, 31490.00m, 54250.00m, "MN 4-2016", 660.00m, "Associate Officer", 1, 730.00m, 10, 11, 10, 5 },
                    { 8, 660.00m, null, 34605.00m, 63460.00m, "MN 5-2016", 755.00m, "Field/Office based Officer Seg 2", 1, 930.00m, 10, 11, 15, null },
                    { 9, 660.00m, null, 36585.00m, 65440.00m, "MN 6-2016", 755.00m, "Field/Office based Officer Seg 1", 1, 930.00m, 10, 11, 15, null },
                    { 10, 755.00m, null, 41580.00m, 68425.00m, "MN 7-2016", 1030.00m, "Management Assistants Supra", 1, null, 11, 18, null, null },
                    { 11, 300.00m, 660.00m, 29840.00m, 48890.00m, "MT 1-2016", 350.00m, "MA Technical Seg 3", 1, 560.00m, 10, 11, 10, 10 },
                    { 12, 350.00m, 660.00m, 30140.00m, 49910.00m, "MT 2-2016", 370.00m, "MA Technical Seg 2", 1, 560.00m, 10, 11, 10, 10 },
                    { 13, 350.00m, 660.00m, 30840.00m, 50610.00m, "MT 3-2016", 370.00m, "MA Technical Seg 1", 1, 560.00m, 10, 11, 10, 10 },
                    { 14, 445.00m, 750.00m, 31190.00m, 57700.00m, "MT 4-2016", 660.00m, "Para Medical Services Seg 3", 1, 730.00m, 10, 11, 10, 10 },
                    { 15, 445.00m, 750.00m, 31635.00m, 58145.00m, "MT 5-2016", 660.00m, "Para Medical Services Seg 2", 1, 730.00m, 10, 11, 10, 10 },
                    { 16, 445.00m, 750.00m, 32080.00m, 58590.00m, "MT 6-2016", 660.00m, "PSM/Para Medical Services Seg 1", 1, 730.00m, 10, 11, 10, 10 },
                    { 17, 445.00m, 750.00m, 32525.00m, 59035.00m, "MT 7-2016", 660.00m, "Nurses", 1, 730.00m, 10, 11, 10, 10 },
                    { 18, 1345.00m, null, 50200.00m, 76690.00m, "MT 8-2016", 1630.00m, "Nurses, PSM, PMS Spl. Grade", 1, null, 10, 8, null, null },
                    { 19, 1335.00m, null, 47615.00m, 110895.00m, "SL 1-2016", 1630.00m, "Executive", 1, 2170.00m, 10, 8, 17, null },
                    { 20, 1335.00m, 2170.00m, 52955.00m, 104355.00m, "SL 2-2016", 1345.00m, "Medical Officers", 1, 1630.00m, 3, 7, 2, 16 },
                    { 21, 2700.00m, null, 88000.00m, 120400.00m, "SL 3-2016", null, "Senior executive/MO Specialists", 1, null, 12, null, null, null },
                    { 22, 2925.00m, null, 98650.00m, 133750.00m, "SL 4-2016", null, "Secretaries", 1, null, 12, null, null, null },
                    { 23, 1335.00m, null, 58295.00m, 105670.00m, "SL 5-2016", 1630.00m, "Law Officers", 1, 2170.00m, 5, 5, 15, null },
                    { 24, 2750.00m, null, 106950.00m, 120700.00m, "SL 7-2016", null, "Addl. SG", 1, null, 5, null, null, null },
                    { 25, 300.00m, null, 27740.00m, 33090.00m, "GE 1-2016", 380.00m, "Sri Lanka Teachers Service", 1, 445.00m, 6, 7, 2, null },
                    { 26, 495.00m, 1335.00m, 33300.00m, 71650.00m, "GE 2-2016", 680.00m, "Sri Lanka Teachers Service", 1, 825.00m, 5, 5, 7, 20 },
                    { 27, 680.00m, 1650.00m, 35280.00m, 82775.00m, "GE 4-2016", 825.00m, "Sri Lanka Principals Service", 1, 1335.00m, 7, 6, 11, 14 },
                    { 28, 660.00m, null, 35195.00m, 64150.00m, "MP 1-2016", 745.00m, "Medical Practitioners", 1, 1135.00m, 12, 13, 10, null },
                    { 29, 1345.00m, null, 56205.00m, 82980.00m, "MP 2-2016", 1630.00m, "Medical Practitioners Spl. Gr.", 1, null, 9, 9, null, null },
                    { 30, 300.00m, null, 29540.00m, 41630.00m, "RS 1-2016", 370.00m, "Police / Regulatory Services", 1, null, 7, 27, null, null },
                    { 31, 370.00m, null, 32010.00m, 43755.00m, "RS 2-2016", 495.00m, "Police / Regulatory Services", 1, null, 9, 17, null, null },
                    { 32, 370.00m, null, 32790.00m, 52870.00m, "RS 3-2016", 495.00m, "Police / Regulatory Services", 1, 660.00m, 7, 2, 25, null },
                    { 33, 660.00m, null, 37030.00m, 52870.00m, "RS 4-2016", null, "Police / Regulatory Services", 1, null, 24, null, null, null },
                    { 34, 775.00m, null, 42425.00m, 55600.00m, "RS 5-2016", null, "Police / Regulatory Services", 1, null, 17, null, null, null },
                    { 35, 2925.00m, null, 131500.00m, 146125.00m, "SF 1-2016", null, "Solicitor General", 1, null, 5, null, null, null },
                    { 36, 2925.00m, null, 139000.00m, 153625.00m, "SF 3-2016", null, "Attorney General", 1, null, 5, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "hr_pf_supporting_doc_type_data",
                columns: new[] { "hr_pf_sdt_dt_id", "hr_pf_sdt_dt_name" },
                values: new object[,]
                {
                    { 1, "National Identity Card (NIC)" },
                    { 2, "Birth Certificate" },
                    { 3, "Marriage Certificate" },
                    { 4, "Educational Certificate" },
                    { 5, "Appointment Letter" },
                    { 6, "EB" },
                    { 7, "Child's Birth Certificate" },
                    { 8, "Other Certificate" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_hr_advance_b_hr_ab_employee_id",
                table: "hr_advance_b",
                column: "hr_ab_employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_advance_b_hr_ab_guarantor1_id",
                table: "hr_advance_b",
                column: "hr_ab_guarantor1_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_advance_b_hr_ab_guarantor2_id",
                table: "hr_advance_b",
                column: "hr_ab_guarantor2_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_advance_b_hr_ab_type_id",
                table: "hr_advance_b",
                column: "hr_ab_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_advance_b_attachment_AdvanceBId",
                table: "hr_advance_b_attachment",
                column: "AdvanceBId");

            migrationBuilder.CreateIndex(
                name: "IX_hr_advance_b_settlement_hr_ab_advance_b_id",
                table: "hr_advance_b_settlement",
                column: "hr_ab_advance_b_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_advance_b_type_ledger_mapping_hr_ab_type_id",
                table: "hr_advance_b_type_ledger_mapping",
                column: "hr_ab_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_address_hr_pf_addr_employee_id",
                table: "hr_pf_address",
                column: "hr_pf_addr_employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_child_info_hr_pf_ci_emp_id",
                table: "hr_pf_child_info",
                column: "hr_pf_ci_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_employee_hr_pf_emp_carder_status",
                table: "hr_pf_employee",
                column: "hr_pf_emp_carder_status");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_employee_hr_pf_emp_no_hr_pf_emp_sabha_id",
                table: "hr_pf_employee",
                columns: new[] { "hr_pf_emp_no", "hr_pf_emp_sabha_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_employee_hr_pf_emp_type",
                table: "hr_pf_employee",
                column: "hr_pf_emp_type");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_job_title_data_hr_pf_jt_st_dt_id",
                table: "hr_pf_job_title_data",
                column: "hr_pf_jt_st_dt_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_net_salary_agent_hr_pf_nt_agnt_emp_id",
                table: "hr_pf_net_salary_agent",
                column: "hr_pf_nt_agnt_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_other_remittance_agent_hr_pf_or_agnt_emp_id",
                table: "hr_pf_other_remittance_agent",
                column: "hr_pf_or_agnt_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_salary_info_hr_pf_saly_emp_id",
                table: "hr_pf_salary_info",
                column: "hr_pf_saly_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_service_info_hr_pf_si_agrahara_category",
                table: "hr_pf_service_info",
                column: "hr_pf_si_agrahara_category");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_service_info_hr_pf_si_appointment_type",
                table: "hr_pf_service_info",
                column: "hr_pf_si_appointment_type");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_service_info_hr_pf_si_class",
                table: "hr_pf_service_info",
                column: "hr_pf_si_class");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_service_info_hr_pf_si_emp_id",
                table: "hr_pf_service_info",
                column: "hr_pf_si_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_service_info_hr_pf_si_funding_source",
                table: "hr_pf_service_info",
                column: "hr_pf_si_funding_source");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_service_info_hr_pf_si_grade",
                table: "hr_pf_service_info",
                column: "hr_pf_si_grade");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_service_info_hr_pf_si_post_name",
                table: "hr_pf_service_info",
                column: "hr_pf_si_post_name");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_service_info_hr_pf_si_salary_structure_id",
                table: "hr_pf_service_info",
                column: "hr_pf_si_salary_structure_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_service_info_hr_pf_si_service_id",
                table: "hr_pf_service_info",
                column: "hr_pf_si_service_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_service_type_data_hr_pf_st_sctgy_dt_id",
                table: "hr_pf_service_type_data",
                column: "hr_pf_st_sctgy_dt_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_spouser_info_hr_pf_si_emp_id",
                table: "hr_pf_spouser_info",
                column: "hr_pf_si_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_supporting_document_hr_pf_sd_emp_id",
                table: "hr_pf_supporting_document",
                column: "hr_pf_sd_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_pf_supporting_document_hr_pf_sd_type_id",
                table: "hr_pf_supporting_document",
                column: "hr_pf_sd_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_hrm_sequence_numbers_year_sabha_id_module_type",
                table: "hrm_sequence_numbers",
                columns: new[] { "year", "sabha_id", "module_type" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hr_advance_b_attachment");

            migrationBuilder.DropTable(
                name: "hr_advance_b_settlement");

            migrationBuilder.DropTable(
                name: "hr_advance_b_type_ledger_mapping");

            migrationBuilder.DropTable(
                name: "hr_pf_address");

            migrationBuilder.DropTable(
                name: "hr_pf_child_info");

            migrationBuilder.DropTable(
                name: "hr_pf_net_salary_agent");

            migrationBuilder.DropTable(
                name: "hr_pf_other_remittance_agent");

            migrationBuilder.DropTable(
                name: "hr_pf_salary_info");

            migrationBuilder.DropTable(
                name: "hr_pf_service_info");

            migrationBuilder.DropTable(
                name: "hr_pf_spouser_info");

            migrationBuilder.DropTable(
                name: "hr_pf_supporting_document");

            migrationBuilder.DropTable(
                name: "hrm_sequence_numbers");

            migrationBuilder.DropTable(
                name: "hr_advance_b");

            migrationBuilder.DropTable(
                name: "hr_pf_agrahara_category_data");

            migrationBuilder.DropTable(
                name: "hr_pf_appointment_type_data");

            migrationBuilder.DropTable(
                name: "hr_pf_class_level_data");

            migrationBuilder.DropTable(
                name: "hr_pf_funding_source_data");

            migrationBuilder.DropTable(
                name: "hr_pf_grade_level_data");

            migrationBuilder.DropTable(
                name: "hr_pf_job_title_data");

            migrationBuilder.DropTable(
                name: "hr_pf_supporting_doc_type_data");

            migrationBuilder.DropTable(
                name: "hr_advance_b_type_data");

            migrationBuilder.DropTable(
                name: "hr_pf_employee");

            migrationBuilder.DropTable(
                name: "hr_pf_service_type_data");

            migrationBuilder.DropTable(
                name: "hr_pf_carder_status_data");

            migrationBuilder.DropTable(
                name: "hr_pf_employee_type_data");

            migrationBuilder.DropTable(
                name: "hr_pf_salary_structure_data");
        }
    }
}
