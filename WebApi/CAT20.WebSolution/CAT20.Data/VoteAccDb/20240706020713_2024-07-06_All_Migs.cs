using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class _20240706_All_Migs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account_details",
                columns: table => new
                {
                    acc_d_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    acc_d_acc_no = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acc_d_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acc_d_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acc_d_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acc_d_bank_id = table.Column<int>(type: "int", nullable: true, comment: "control db fk"),
                    acc_d_vote_id = table.Column<int>(type: "int", nullable: true),
                    acc_d_status = table.Column<int>(type: "int", nullable: true),
                    acc_d_office_id = table.Column<int>(type: "int", nullable: true),
                    acc_d_bank_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acc_d_branch_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    running_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    expense_hold = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    row_status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.acc_d_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "account_transfer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    office_id = table.Column<int>(type: "int", nullable: false),
                    voucher_Id = table.Column<int>(type: "int", nullable: false),
                    ActionState = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    from_account_Id = table.Column<int>(type: "int", nullable: false),
                    from_vote_balanceId = table.Column<int>(type: "int", nullable: false),
                    from_vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    to_account_id = table.Column<int>(type: "int", nullable: false),
                    to_vote_balanceId = table.Column<int>(type: "int", nullable: false),
                    to_vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    is_refund = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    refunded_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    is_fully_refunded = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false),
                    request_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_transfer", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "deposit_sub_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_craete_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposit_sub_info", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "deposits",
                columns: table => new
                {
                    dep_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dep_deposit_sub_category_id = table.Column<int>(type: "int", nullable: false),
                    dep_ledger_account_id = table.Column<int>(type: "int", nullable: false),
                    dep_sub_info = table.Column<int>(type: "int", nullable: true),
                    dep_deposit_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    dep_mix_order_id = table.Column<int>(type: "int", nullable: true),
                    dep_mix_order_line_id = table.Column<int>(type: "int", nullable: true),
                    dep_receipt_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dep_description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dep_partner_id = table.Column<int>(type: "int", nullable: false),
                    dep_initial_deposit_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    dep_relesed_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    hold_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    dep_sabha_id = table.Column<int>(type: "int", nullable: false),
                    dep_office_id = table.Column<int>(type: "int", nullable: false),
                    dep_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    is_editable = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposits", x => x.dep_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "final_account_sequence_numbers",
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
                    table.PrimaryKey("PK_final_account_sequence_numbers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "SpecialLedgerAccountTypesResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameInEnglish = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameInSinhala = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameInTamil = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialLedgerAccountTypesResource", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sub_imprest",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sub_imprest_vote_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    dep_mix_order_id = table.Column<int>(type: "int", nullable: true),
                    voucher_id = table.Column<int>(type: "int", nullable: true),
                    receipt_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    settle_by_bills = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    settle_by_cash = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    office_id = table.Column<int>(type: "int", nullable: true),
                    exceed_settlement_vote_id = table.Column<int>(type: "int", nullable: false),
                    exceed_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    settlement_voucher_id = table.Column<int>(type: "int", nullable: true),
                    action_states = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    is_open_balance = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_illegal = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    dep_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_imprest", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "trade_tax_business_natures",
                columns: table => new
                {
                    business_nature_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    business_nature_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_status = table.Column<int>(type: "int", nullable: true),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    business_nature_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    business_nature_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trade_tax_business_natures", x => x.business_nature_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "trade_tax_tax_values",
                columns: table => new
                {
                    tax_value_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    active_status = table.Column<int>(type: "int", nullable: false),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    tax_type_id = table.Column<int>(type: "int", nullable: false),
                    annual_value_minimum = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    annual_value_maximum = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    tax_value = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    tax_value_status = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trade_tax_tax_values", x => x.tax_value_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "trade_tax_vote_assignments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    active_status = table.Column<int>(type: "int", nullable: false),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    tax_type_id = table.Column<int>(type: "int", nullable: false),
                    vote_assignement_detail_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trade_tax_vote_assignments", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_balancesheet_balance",
                columns: table => new
                {
                    vt_balancesheet_bal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_balancesheet_bal_vote_id = table.Column<int>(type: "int", nullable: false),
                    vt_balancesheet_bal_year = table.Column<int>(type: "int", nullable: true),
                    vt_balancesheet_bal_balance = table.Column<double>(type: "double", nullable: false),
                    vt_balancesheet_bal_comment = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_balancesheet_bal_enter_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    vt_balancesheet_bal_sabha_id = table.Column<int>(type: "int", nullable: false),
                    vt_balancesheet_bal_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.vt_balancesheet_bal_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_balsheet_title",
                columns: table => new
                {
                    vt_balsheet_title_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_balsheet_title_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    vt_balsheet_title_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    vt_balsheet_title_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    vt_balsheet_title_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    vt_balsheet_title_balpath = table.Column<int>(type: "int", nullable: true),
                    vt_balsheet_title_status = table.Column<int>(type: "int", nullable: true),
                    vt_balsheet_title_sabha_id = table.Column<int>(type: "int", nullable: true),
                    vt_balsheet_title_classification_id = table.Column<int>(type: "int", nullable: true),
                    vt_balsheet_title_mainlegr_acct_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_balsheet_title", x => x.vt_balsheet_title_id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "vt_cash_book",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_cb_sbha_id = table.Column<int>(type: "int", nullable: false),
                    vt_cb__office_id = table.Column<int>(type: "int", nullable: false),
                    vt_cb_session_id = table.Column<int>(type: "int", nullable: false),
                    vt_cb_bank_acc_id = table.Column<int>(type: "int", nullable: false),
                    vt_cb_description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_cb_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    vt_cb_tr_type = table.Column<int>(type: "int", nullable: false),
                    vt_cb_income_expense_method = table.Column<int>(type: "int", nullable: false),
                    vt_cb_income_category = table.Column<int>(type: "int", nullable: false),
                    vt_cb_income_item_id = table.Column<int>(type: "int", nullable: true),
                    vt_cb_expense_category = table.Column<int>(type: "int", nullable: false),
                    vt_cb_expense_item_id = table.Column<int>(type: "int", nullable: true),
                    vt_cb_cheque_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_cb_income_head_string = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_cb_expense_head_string = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_cb_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_cb_sub_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_cb_cash_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_cb_cheque_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_cb_direct_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_cb_cross_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_cb_rn_total = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_cash_book", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_cash_book_daily_balance",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    office_id = table.Column<int>(type: "int", nullable: false),
                    session_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    bank_acc_id = table.Column<int>(type: "int", nullable: false),
                    inc_cheque_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    inc_direct_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    inc_cross_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    inc_total_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    exp_cash_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    exp_cheque_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    exp_direct_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    exp_cross_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    current_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    row_status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_cash_book_daily_balance", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_inc_title",
                columns: table => new
                {
                    vt_inc_title_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_inc_title_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_title_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_title_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_title_name_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_title_status = table.Column<int>(type: "int", nullable: true),
                    vt_inc_title_programme_id = table.Column<int>(type: "int", nullable: true),
                    vt_inc_title_sabha_id = table.Column<int>(type: "int", nullable: true),
                    vt_inc_title_classification_id = table.Column<int>(type: "int", nullable: true),
                    vt_inc_title_mainlegr_acct_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_inc_title", x => x.vt_inc_title_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_ledger_book",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_lb_sbha_id = table.Column<int>(type: "int", nullable: false),
                    vt_lb_office_id = table.Column<int>(type: "int", nullable: false),
                    vt_lb_session_id = table.Column<int>(type: "int", nullable: false),
                    vt_lb_item_code = table.Column<string>(type: "longtext", nullable: false, defaultValue: "-", collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_lb_description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_lb_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    vt_lb_vote_balance_id = table.Column<int>(type: "int", nullable: false),
                    vt_lb_vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    vt_lb_tr_type = table.Column<int>(type: "int", nullable: false),
                    vt_lb_vote_bal_tr_type = table.Column<int>(type: "int", nullable: false),
                    vt_lb_income_item_id = table.Column<int>(type: "int", nullable: true),
                    vt_lb_expense_item_id = table.Column<int>(type: "int", nullable: true),
                    vt_lb_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_lb_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_lb_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_lb_rn_total = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    row_status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_ledger_book", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_ledger_book_daily_balance",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false),
                    session_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    vote_balance_id = table.Column<int>(type: "int", nullable: false),
                    vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    total_daily_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    TotalDailyDebit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    total_running_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    row_status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_ledger_book_daily_balance", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_internal_journal_transfer",
                columns: table => new
                {
                    ijt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ijt_vote_bal_id = table.Column<int>(type: "int", nullable: false),
                    ijt_vote_id = table.Column<int>(type: "int", nullable: false),
                    ijt_sabha_id = table.Column<int>(type: "int", nullable: true),
                    ijt_year = table.Column<int>(type: "int", nullable: true),
                    ijt_month = table.Column<int>(type: "int", nullable: true),
                    ijt_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ijt_sub_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ijt_description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ijt_status = table.Column<int>(type: "int", nullable: false),
                    ijt_transaction_type = table.Column<int>(type: "int", nullable: false),
                    ijt_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    ijt_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    ijt_module_primary_key = table.Column<int>(type: "int", nullable: true),
                    ijt_app_category = table.Column<int>(type: "int", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_internal_journal_transfer", x => x.ijt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_commitment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    bank_id = table.Column<int>(type: "int", nullable: false),
                    payee_category = table.Column<int>(type: "int", nullable: false),
                    PayeeId = table.Column<int>(type: "int", nullable: false),
                    payee_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    commitment_sequence_number = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_state = table.Column<int>(type: "int", nullable: false),
                    payment_status = table.Column<int>(type: "int", nullable: true),
                    has_voucher = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    session_id = table.Column<int>(type: "int", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    month = table.Column<int>(type: "int", nullable: false),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_commitment", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commitment_id = table.Column<int>(type: "int", nullable: true),
                    sub_imprest_id = table.Column<int>(type: "int", nullable: true),
                    settlement_id = table.Column<int>(type: "int", nullable: true),
                    account_transfer_id = table.Column<int>(type: "int", nullable: true),
                    refund_id = table.Column<int>(type: "int", nullable: true),
                    repayment_order_id = table.Column<int>(type: "int", nullable: true),
                    comment_or_description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    vat_total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    nbt_total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    stamp_total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    voucher_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    bank_id = table.Column<int>(type: "int", nullable: false),
                    cross_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    total_cheque_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    payee_category = table.Column<int>(type: "int", nullable: false),
                    action_state = table.Column<int>(type: "int", nullable: false),
                    payment_status = table.Column<int>(type: "int", nullable: true, defaultValue: 2),
                    voucher_category = table.Column<int>(type: "int", nullable: true),
                    voucher_sequence_number = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    session_id = table.Column<int>(type: "int", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    month = table.Column<int>(type: "int", nullable: false),
                    pre_chairman_approve = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    file_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pre_committee_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pre_committee_approve = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    per_council_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    per_council_approve_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    subject_to_approve = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_cheque",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cheque_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    grp_id = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bank_id = table.Column<int>(type: "int", nullable: false),
                    payee_category = table.Column<int>(type: "int", nullable: false),
                    payee_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    payee_id = table.Column<int>(type: "int", nullable: false),
                    voucher_id_as_string = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    is_printed = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    cheque_category = table.Column<int>(type: "int", nullable: true),
                    voucher_item_id = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher_cheque", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_vote_cut_province",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cut_province_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    office_id = table.Column<int>(type: "int", nullable: false),
                    vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    vote_balance_id = table.Column<int>(type: "int", nullable: false),
                    ActionState = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    request_snapshot_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    action_snapshot_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    request_snapshot_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    action_snapshot_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    requet_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    requet_by = table.Column<int>(type: "int", nullable: false),
                    requet_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    action_by = table.Column<int>(type: "int", nullable: true),
                    action_system_date = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    requet_system_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SystemActionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_vote_cut_province", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_vote_fr66_transfer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    journal_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    office_id = table.Column<int>(type: "int", nullable: false),
                    ActionState = table.Column<int>(type: "int", nullable: false),
                    from_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    to_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    requet_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    requet_by = table.Column<int>(type: "int", nullable: false),
                    requet_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    action_by = table.Column<int>(type: "int", nullable: true),
                    action_system_date = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    requet_system_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SystemActionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_vote_fr66_transfer", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_vote_journal_adjustments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    journal_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    office_id = table.Column<int>(type: "int", nullable: false),
                    ActionState = table.Column<int>(type: "int", nullable: false),
                    from_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    to_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    requet_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    requet_by = table.Column<int>(type: "int", nullable: false),
                    requet_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    action_by = table.Column<int>(type: "int", nullable: true),
                    action_system_date = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    requet_system_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SystemActionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_vote_journal_adjustments", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_vote_supplementary",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cut_province_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    office_id = table.Column<int>(type: "int", nullable: false),
                    vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    vote_balance_id = table.Column<int>(type: "int", nullable: false),
                    ActionState = table.Column<int>(type: "int", nullable: false),
                    from_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    request_snapshot_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    action_snapshot_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    request_snapshot_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    action_snapshot_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    requet_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    requet_by = table.Column<int>(type: "int", nullable: false),
                    requet_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    action_by = table.Column<int>(type: "int", nullable: true),
                    action_system_date = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    requet_system_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SystemActionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_vote_supplementary", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_programme",
                columns: table => new
                {
                    vt_programme_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_programme_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_programme_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_programme_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_programme_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_programme_status = table.Column<int>(type: "int", nullable: true),
                    vt_programme_sabha_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_programme", x => x.vt_programme_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_vote_details",
                columns: table => new
                {
                    vt_d_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_d_vote_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_vote_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_vote_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_vote_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_vote_order = table.Column<int>(type: "int", nullable: true),
                    vt_d_programme_id = table.Column<int>(type: "int", nullable: false),
                    vt_d_programme_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_programme_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_programme_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProgrammeCode = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_project_id = table.Column<int>(type: "int", nullable: false),
                    vt_d_project_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_project_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_project_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_project_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_subproject_id = table.Column<int>(type: "int", nullable: false),
                    vt_d_subproject_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_subproject_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_subproject_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_subproject_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_classsification_id = table.Column<int>(type: "int", nullable: true),
                    vt_d_classsification_desc = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_main_ledger_acct_id = table.Column<int>(type: "int", nullable: true),
                    vt_d_main_ledger_acct_desc = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_title_id = table.Column<int>(type: "int", nullable: false),
                    vt_d_title_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_title_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_title_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_title_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_subtitle_id = table.Column<int>(type: "int", nullable: false),
                    vt_d_subtitle_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_subtitle_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_subtitle_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_subtitle_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_income_or_expense = table.Column<int>(type: "int", nullable: true),
                    vt_d_vote_or_bal = table.Column<int>(type: "int", nullable: true),
                    vt_d_sabha_id = table.Column<int>(type: "int", nullable: false),
                    vt_d_sabha_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_d_status = table.Column<int>(type: "int", nullable: false),
                    vt_d_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    vt_d_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    vt_d_created_by = table.Column<int>(type: "int", nullable: true),
                    vt_d_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.vt_d_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "account_bal_details",
                columns: table => new
                {
                    acc_bd_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    acc_bd_acc_d_id = table.Column<int>(type: "int", nullable: false),
                    acc_bd_year = table.Column<int>(type: "int", nullable: true),
                    acc_bd_bal_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    acc_bd_enter_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    acc_bd_status = table.Column<int>(type: "int", nullable: false),
                    acc_sabha_id = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.acc_bd_id);
                    table.ForeignKey(
                        name: "fk_acc_bd_acc_d_id",
                        column: x => x.acc_bd_acc_d_id,
                        principalTable: "account_details",
                        principalColumn: "acc_d_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "account_transfer_refunding",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    account_transfer_id = table.Column<int>(type: "int", nullable: false),
                    refund_note = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoucherId = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CrossOrderId = table.Column<int>(type: "int", nullable: true),
                    dep_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_transfer_refunding", x => x.id);
                    table.ForeignKey(
                        name: "FK_account_transfer_refunding_account_transfer_account_transfer~",
                        column: x => x.account_transfer_id,
                        principalTable: "account_transfer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "special_ledger_account_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name_in_english = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name_in_sinhala = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name_in_tamil = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_special_ledger_account_type", x => x.id);
                    table.ForeignKey(
                        name: "FK_special_ledger_account_type_SpecialLedgerAccountTypesResourc~",
                        column: x => x.TypeId,
                        principalTable: "SpecialLedgerAccountTypesResource",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sub_imprest_settlement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sub_ipmrest_id = table.Column<int>(type: "int", nullable: true),
                    vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    dep_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_imprest_settlement", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_imprest_settlement_sub_imprest_sub_ipmrest_id",
                        column: x => x.sub_ipmrest_id,
                        principalTable: "sub_imprest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sub_imprest_settlement_corss_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    settlement_cross_id = table.Column<int>(type: "int", nullable: true),
                    SubImprestId = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_imprest_settlement_corss_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_imprest_settlement_corss_orders_sub_imprest_SubImprestId",
                        column: x => x.SubImprestId,
                        principalTable: "sub_imprest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "trade_tax_business_subnatures",
                columns: table => new
                {
                    business_subnature_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    business_subnature_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_subnature_status = table.Column<int>(type: "int", nullable: false),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    business_nature_id = table.Column<int>(type: "int", nullable: false),
                    other_charges = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    active_status = table.Column<int>(type: "int", nullable: false),
                    tax_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trade_tax_business_subnatures", x => x.business_subnature_id);
                    table.ForeignKey(
                        name: "fk_business_subnature_business_nature_id",
                        column: x => x.business_nature_id,
                        principalTable: "trade_tax_business_natures",
                        principalColumn: "business_nature_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_balsheet_subtitle",
                columns: table => new
                {
                    vt_balsheet_subtitle_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_balsheet_subtitle_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    vt_balsheet_subtitle_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    vt_balsheet_subtitle_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    vt_balsheet_subtitle_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    vt_balsheet_subtitle_title_id = table.Column<int>(type: "int", nullable: true),
                    vt_balsheet_subtitle_status = table.Column<int>(type: "int", nullable: true),
                    vt_balsheet_subtitle_sabha_id = table.Column<int>(type: "int", nullable: true),
                    vt_balsheet_subtitle_bank_account_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_balsheet_subtitle", x => x.vt_balsheet_subtitle_id);
                    table.ForeignKey(
                        name: "fk_vt_balsheet_subtitle_title_id",
                        column: x => x.vt_balsheet_subtitle_title_id,
                        principalTable: "vt_balsheet_title",
                        principalColumn: "vt_balsheet_title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "vt_inc_subtitle",
                columns: table => new
                {
                    vt_inc_subtitle_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_inc_subtitle_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_subtitle_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_subtitle_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_subtitle_name_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_subtitle_title_id = table.Column<int>(type: "int", nullable: true),
                    vt_inc_subtitle_status = table.Column<int>(type: "int", nullable: true),
                    vt_inc_subtitle_sabha_id = table.Column<int>(type: "int", nullable: true),
                    vt_inc_subtitle_programme_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_inc_subtitle", x => x.vt_inc_subtitle_id);
                    table.ForeignKey(
                        name: "fk_vt_inc_subtitle_title_id",
                        column: x => x.vt_inc_subtitle_title_id,
                        principalTable: "vt_inc_title",
                        principalColumn: "vt_inc_title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_commitment_action_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commitment_id = table.Column<int>(type: "int", nullable: true),
                    action_state = table.Column<int>(type: "int", nullable: true),
                    action_by = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_commitment_action_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_commitment_action_log_vt_lgr_pmnt_commitment_com~",
                        column: x => x.commitment_id,
                        principalTable: "vt_lgr_pmnt_commitment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_commitment_line",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commitment_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_commitment_line", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_commitment_line_vt_lgr_pmnt_commitment_commitmen~",
                        column: x => x.commitment_id,
                        principalTable: "vt_lgr_pmnt_commitment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_commitment_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commitment_id = table.Column<int>(type: "int", nullable: true),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    PayeeCategory = table.Column<int>(type: "int", nullable: false),
                    payee_id = table.Column<int>(type: "int", nullable: false),
                    payee_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    commitment_sequence_number = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_state = table.Column<int>(type: "int", nullable: true),
                    has_voucher = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    session_id = table.Column<int>(type: "int", nullable: true),
                    year = table.Column<int>(type: "int", nullable: false),
                    month = table.Column<int>(type: "int", nullable: false),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_commitment_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_commitment_log_vt_lgr_pmnt_commitment_commitment~",
                        column: x => x.commitment_id,
                        principalTable: "vt_lgr_pmnt_commitment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_deposit_for_voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_id = table.Column<int>(type: "int", nullable: true),
                    deposit_id = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_deposit_for_voucher_vt_lgr_pmnt_voucher_voucher_~",
                        column: x => x.voucher_id,
                        principalTable: "vt_lgr_pmnt_voucher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_sub_voucher_item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    sub_voucher_no = table.Column<int>(type: "int", nullable: false),
                    comment_or_description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    payee_id = table.Column<int>(type: "int", nullable: false),
                    vat_total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    nbt_total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    stamp = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    cross_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    total_cheque_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_sub_voucher_item_vt_lgr_pmnt_voucher_voucher_id",
                        column: x => x.voucher_id,
                        principalTable: "vt_lgr_pmnt_voucher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_action_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_id = table.Column<int>(type: "int", nullable: true),
                    action_state = table.Column<int>(type: "int", nullable: true),
                    action_by = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher_action_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_voucher_action_log_vt_lgr_pmnt_voucher_voucher_id",
                        column: x => x.voucher_id,
                        principalTable: "vt_lgr_pmnt_voucher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_doucument",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_id = table.Column<int>(type: "int", nullable: true),
                    doc_type = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uri = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher_doucument", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_voucher_doucument_vt_lgr_pmnt_voucher_voucher_id",
                        column: x => x.voucher_id,
                        principalTable: "vt_lgr_pmnt_voucher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_invoice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uri = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_status = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher_invoice", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_voucher_invoice_vt_lgr_pmnt_voucher_voucher_id",
                        column: x => x.voucher_id,
                        principalTable: "vt_lgr_pmnt_voucher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_line",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    vote_id = table.Column<int>(type: "int", nullable: false),
                    vote_code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vote_balance_id = table.Column<int>(type: "int", nullable: false),
                    comment_or_description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    commitment_line_id = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    vat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    nbt = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    rpt_budget_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    rpt_expenditure = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    rpt_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher_line", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_voucher_line_vt_lgr_pmnt_voucher_voucher_id",
                        column: x => x.voucher_id,
                        principalTable: "vt_lgr_pmnt_voucher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_id = table.Column<int>(type: "int", nullable: true),
                    commitment_id = table.Column<int>(type: "int", nullable: false),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    vat_total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    nbt_total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    stamp = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    action_state = table.Column<int>(type: "int", nullable: true),
                    voucher_category = table.Column<int>(type: "int", nullable: true),
                    voucher_sequence_number = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    session_id = table.Column<int>(type: "int", nullable: false),
                    payee_id = table.Column<int>(type: "int", nullable: false),
                    row_status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_voucher_log_vt_lgr_pmnt_voucher_voucher_id",
                        column: x => x.voucher_id,
                        principalTable: "vt_lgr_pmnt_voucher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_cheque_action_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_cheque_id = table.Column<int>(type: "int", nullable: true),
                    action_state = table.Column<int>(type: "int", nullable: true),
                    comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_by = table.Column<int>(type: "int", nullable: false),
                    action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher_cheque_action_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_voucher_cheque_action_log_vt_lgr_pmnt_voucher_ch~",
                        column: x => x.voucher_cheque_id,
                        principalTable: "vt_lgr_pmnt_voucher_cheque",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_cheque_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_cheque_id = table.Column<int>(type: "int", nullable: false),
                    cheque_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_printed = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher_cheque_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_voucher_cheque_log_vt_lgr_pmnt_voucher_cheque_vo~",
                        column: x => x.voucher_cheque_id,
                        principalTable: "vt_lgr_pmnt_voucher_cheque",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_cheque_voucher_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_cheque_id = table.Column<int>(type: "int", nullable: false),
                    sub_voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher_cheque_voucher_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_voucher_cheque_voucher_items_vt_lgr_pmnt_voucher~",
                        column: x => x.voucher_cheque_id,
                        principalTable: "vt_lgr_pmnt_voucher_cheque",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_vote_fr66_items_from",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vote_fr66_id = table.Column<int>(type: "int", nullable: false),
                    vote_fr66_from_vote_balance_id = table.Column<int>(type: "int", nullable: false),
                    from_vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    from_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    request_snapshot_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    action_snapshot_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    request_snapshot_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    action_snapshot_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_vote_fr66_items_from", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_vote_fr66_items_from_vt_lgr_vote_fr66_transfer_vote_f~",
                        column: x => x.vote_fr66_id,
                        principalTable: "vt_lgr_vote_fr66_transfer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_vote_fr66_items_to",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vote_fr66_id = table.Column<int>(type: "int", nullable: false),
                    vote_fr66_to_vote_balance_id = table.Column<int>(type: "int", nullable: false),
                    to_vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    to_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    request_snapshot_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    action_snapshot_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    request_snapshot_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    action_snapshot_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_vote_fr66_items_to", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_vote_fr66_items_to_vt_lgr_vote_fr66_transfer_vote_fr6~",
                        column: x => x.vote_fr66_id,
                        principalTable: "vt_lgr_vote_fr66_transfer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_vote_journal_items_from",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vote_journal_adjustment_id = table.Column<int>(type: "int", nullable: false),
                    vote_journal_from_vote_balance_id = table.Column<int>(type: "int", nullable: false),
                    from_vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    from_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_vote_journal_items_from", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_vote_journal_items_from_vt_lgr_vote_journal_adjustmen~",
                        column: x => x.vote_journal_adjustment_id,
                        principalTable: "vt_lgr_vote_journal_adjustments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_vote_journal_items_to",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vote_journal_adjustment_id = table.Column<int>(type: "int", nullable: false),
                    vote_journal_to_vote_balance_id = table.Column<int>(type: "int", nullable: false),
                    to_vote_detail_id = table.Column<int>(type: "int", nullable: false),
                    to_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_vote_journal_items_to", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_vote_journal_items_to_vt_lgr_vote_journal_adjustments~",
                        column: x => x.vote_journal_adjustment_id,
                        principalTable: "vt_lgr_vote_journal_adjustments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_inc_project",
                columns: table => new
                {
                    vt_inc_project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_inc_project_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_project_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_project_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_project_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_project_status = table.Column<int>(type: "int", nullable: true),
                    vt_inc_project_programme_id = table.Column<int>(type: "int", nullable: true),
                    vt_inc_project_sabha_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_inc_project", x => x.vt_inc_project_id);
                    table.ForeignKey(
                        name: "fk_vt_inc_project_programme_id",
                        column: x => x.vt_inc_project_programme_id,
                        principalTable: "vt_programme",
                        principalColumn: "vt_programme_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_vote_balance",
                columns: table => new
                {
                    vt_vote_bal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_vote_bal_vote_id = table.Column<int>(type: "int", nullable: false),
                    vt_vote_classification_id = table.Column<int>(type: "int", nullable: true),
                    vt_vote_bal_sabha_id = table.Column<int>(type: "int", nullable: true),
                    vt_vote_bal_year = table.Column<int>(type: "int", nullable: true),
                    vt_vote_bal_month = table.Column<int>(type: "int", nullable: true),
                    vt_vote_bal_status = table.Column<int>(type: "int", nullable: false),
                    vt_vote_bal_transaction_type = table.Column<int>(type: "int", nullable: false),
                    vt_vote_bal_is_carry_forward = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false),
                    vt_vote_bal_estimated_income_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: true, defaultValue: 0m),
                    vt_vote_bal_allocation_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: true),
                    vt_vote_bal_take_hold_rate = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_vote_bal_take_hold_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_vote_bal_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_total_commited = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_total_hold = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_total_pending = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_credit_debit_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_running_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_carry_forward_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_carry_forward_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_credit_debit_carry_forward_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_surcharge_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_surcharge_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_credit_debit_surcharge_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_transfer_flag = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    session_id_by_office = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    office_id = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_vote_balance", x => x.vt_vote_bal_id);
                    table.ForeignKey(
                        name: "FK_vt_vote_balance_vt_vote_details_vt_vote_bal_vote_id",
                        column: x => x.vt_vote_bal_vote_id,
                        principalTable: "vt_vote_details",
                        principalColumn: "vt_d_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "special_ledger_accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vote_code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vote_id = table.Column<int>(type: "int", nullable: false),
                    type_id = table.Column<int>(type: "int", nullable: true),
                    acc_sabha_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_special_ledger_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_special_ledger_accounts_special_ledger_account_type_type_id",
                        column: x => x.type_id,
                        principalTable: "special_ledger_account_type",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sub_imprest_documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sub_ipmrest_settlement_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uri = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_imprest_documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_imprest_documents_sub_imprest_settlement_sub_ipmrest_set~",
                        column: x => x.sub_ipmrest_settlement_id,
                        principalTable: "sub_imprest_settlement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_commitmentline_votes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commitment_line_id = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vote_id = table.Column<int>(type: "int", nullable: false),
                    vote_code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vote_allacation_id = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    remaining = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    payment_status = table.Column<int>(type: "int", nullable: true),
                    row_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_commitmentline_votes", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_commitmentline_votes_vt_lgr_pmnt_commitment_line~",
                        column: x => x.commitment_line_id,
                        principalTable: "vt_lgr_pmnt_commitment_line",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_commitment_line_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commitment_log_id = table.Column<int>(type: "int", nullable: false),
                    commitment_line_id = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_commitment_line_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_commitment_line_log_vt_lgr_pmnt_commitment_log_c~",
                        column: x => x.commitment_log_id,
                        principalTable: "vt_lgr_pmnt_commitment_log",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_cross_orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    cross_order_id = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    order_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_voucher_cross_orders_vt_lgr_pmnt_sub_voucher_ite~",
                        column: x => x.voucher_item_id,
                        principalTable: "vt_lgr_pmnt_sub_voucher_item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_voucher_sub_line",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_line_id = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    vat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    nbt = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RowVersion = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_voucher_sub_line", x => x.id);
                    table.ForeignKey(
                        name: "fk_voucher_line_id_voucher_subline_voucher_line_id",
                        column: x => x.voucher_line_id,
                        principalTable: "vt_lgr_pmnt_voucher_line",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_voucher_line_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    voucher_log_id = table.Column<int>(type: "int", nullable: false),
                    voucher_line_id = table.Column<int>(type: "int", nullable: true),
                    vote_balance_id = table.Column<int>(type: "int", nullable: false),
                    comment_or_description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    commitment_line_id = table.Column<int>(type: "int", nullable: false),
                    vote_id = table.Column<int>(type: "int", nullable: false),
                    vote_code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    vat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    nbt = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    rpt_budget_allocation = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    rpt_expenditure = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    rpt_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_voucher_line_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_voucher_line_log_vt_lgr_pmnt_voucher_log_voucher~",
                        column: x => x.voucher_log_id,
                        principalTable: "vt_lgr_pmnt_voucher_log",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_inc_sub_project",
                columns: table => new
                {
                    vt_inc_sub_project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_inc_sub_project_name_sinhala = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_sub_project_name_english = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_sub_project_name_tamil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_sub_project_name_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_inc_sub_project_status = table.Column<int>(type: "int", nullable: true),
                    vt_inc_sub_project_project_id = table.Column<int>(type: "int", nullable: false),
                    vt_inc_sub_project_sabha_id = table.Column<int>(type: "int", nullable: true),
                    vt_inc_sub_project_programme_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_inc_sub_project", x => x.vt_inc_sub_project_id);
                    table.ForeignKey(
                        name: "fk_vt_inc_sub_project_project_id",
                        column: x => x.vt_inc_sub_project_project_id,
                        principalTable: "vt_inc_project",
                        principalColumn: "vt_inc_project_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_vote_balance_action_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vote_balance_id = table.Column<int>(type: "int", nullable: true),
                    action_state = table.Column<int>(type: "int", nullable: true),
                    action_by = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_vote_balance_action_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_vote_balance_action_log_vt_vote_balance_vote_balance_id",
                        column: x => x.vote_balance_id,
                        principalTable: "vt_vote_balance",
                        principalColumn: "vt_vote_bal_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_vote_balance_log",
                columns: table => new
                {
                    vt_vote_bal_lg_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_vote_bal_lg_vote_bal_id = table.Column<int>(type: "int", nullable: false),
                    vt_vote_bal_lg_vote_id = table.Column<int>(type: "int", nullable: false),
                    vt_vote_bal_lg_sabha_id = table.Column<int>(type: "int", nullable: true),
                    vt_vote_bal_lg_year = table.Column<int>(type: "int", nullable: true),
                    vt_vote_bal_lg_month = table.Column<int>(type: "int", nullable: true),
                    vt_vote_bal_lg_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_vote_bal_lg_sub_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_vote_bal_lg_description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_vote_bal_lg_status = table.Column<int>(type: "int", nullable: false),
                    vt_vote_bal_lg_transaction_type = table.Column<int>(type: "int", nullable: false),
                    vt_vote_bal_lg_is_carry_forward = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false),
                    vt_vote_bal_lg_estimated_income_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: true, defaultValue: 0m),
                    vt_vote_bal_lg_allocation_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: true, defaultValue: 0m),
                    vt_vote_bal_lg_take_hold_rate = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_vote_bal_lg_take_hold_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_vote_bal_lg_allocation_exchange_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: true, defaultValue: 0m),
                    vt_vote_bal_lg_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_total_commited = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_total_hold = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_total_lg_pending = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_exchange_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_credit_debit_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_running_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_vote_bal_lg_carry_forward_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_carry_forward_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_credit_debit_carry_forward_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_surcharge_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_surcharge_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_credit_debit_surcharge_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_vote_bal_lg_module_primary_key = table.Column<int>(type: "int", nullable: true),
                    vt_vote_bal_lg_app_category = table.Column<int>(type: "int", nullable: true),
                    office_id = table.Column<int>(type: "int", nullable: true),
                    session_id_by_office = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_vote_balance_log", x => x.vt_vote_bal_lg_id);
                    table.ForeignKey(
                        name: "FK_vt_vote_balance_log_vt_vote_balance_vt_vote_bal_lg_vote_bal_~",
                        column: x => x.vt_vote_bal_lg_vote_bal_id,
                        principalTable: "vt_vote_balance",
                        principalColumn: "vt_vote_bal_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_lgr_pmnt_commitmentline_votes_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commitment_line_log_id = table.Column<int>(type: "int", nullable: false),
                    commitment_line_vote_id = table.Column<int>(type: "int", nullable: true),
                    vote_id = table.Column<int>(type: "int", nullable: false),
                    vote_code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vote_allacation_id = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    remaining = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    payment_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_lgr_pmnt_commitmentline_votes_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_lgr_pmnt_commitmentline_votes_log_vt_lgr_pmnt_commitment_~",
                        column: x => x.commitment_line_log_id,
                        principalTable: "vt_lgr_pmnt_commitment_line_log",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_account_bal_details_acc_bd_acc_d_id",
                table: "account_bal_details",
                column: "acc_bd_acc_d_id");

            migrationBuilder.CreateIndex(
                name: "IX_account_transfer_refunding_account_transfer_id",
                table: "account_transfer_refunding",
                column: "account_transfer_id");

            migrationBuilder.CreateIndex(
                name: "IX_deposit_sub_info_name_sabha_id",
                table: "deposit_sub_info",
                columns: new[] { "name", "sabha_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_final_account_sequence_numbers_year_sabha_id_module_type",
                table: "final_account_sequence_numbers",
                columns: new[] { "year", "sabha_id", "module_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_special_ledger_account_type_TypeId",
                table: "special_ledger_account_type",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_special_ledger_accounts_acc_sabha_id_type_id",
                table: "special_ledger_accounts",
                columns: new[] { "acc_sabha_id", "type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_special_ledger_accounts_type_id",
                table: "special_ledger_accounts",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_imprest_documents_sub_ipmrest_settlement_id",
                table: "sub_imprest_documents",
                column: "sub_ipmrest_settlement_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_imprest_settlement_sub_ipmrest_id",
                table: "sub_imprest_settlement",
                column: "sub_ipmrest_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_imprest_settlement_corss_orders_SubImprestId",
                table: "sub_imprest_settlement_corss_orders",
                column: "SubImprestId");

            migrationBuilder.CreateIndex(
                name: "IX_trade_tax_business_subnatures_business_nature_id",
                table: "trade_tax_business_subnatures",
                column: "business_nature_id");

            migrationBuilder.CreateIndex(
                name: "IX_trade_tax_vote_assignments_sabha_id_tax_type_id",
                table: "trade_tax_vote_assignments",
                columns: new[] { "sabha_id", "tax_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vt_balsheet_subtitle_vt_balsheet_subtitle_title_id",
                table: "vt_balsheet_subtitle",
                column: "vt_balsheet_subtitle_title_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_inc_project_vt_inc_project_programme_id",
                table: "vt_inc_project",
                column: "vt_inc_project_programme_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_inc_sub_project_vt_inc_sub_project_project_id",
                table: "vt_inc_sub_project",
                column: "vt_inc_sub_project_project_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_inc_subtitle_vt_inc_subtitle_title_id",
                table: "vt_inc_subtitle",
                column: "vt_inc_subtitle_title_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_commitment_action_log_commitment_id",
                table: "vt_lgr_pmnt_commitment_action_log",
                column: "commitment_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_commitment_line_commitment_id",
                table: "vt_lgr_pmnt_commitment_line",
                column: "commitment_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_commitment_line_log_commitment_log_id",
                table: "vt_lgr_pmnt_commitment_line_log",
                column: "commitment_log_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_commitment_log_commitment_id",
                table: "vt_lgr_pmnt_commitment_log",
                column: "commitment_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_commitmentline_votes_commitment_line_id",
                table: "vt_lgr_pmnt_commitmentline_votes",
                column: "commitment_line_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_commitmentline_votes_log_commitment_line_log_id",
                table: "vt_lgr_pmnt_commitmentline_votes_log",
                column: "commitment_line_log_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_deposit_for_voucher_voucher_id",
                table: "vt_lgr_pmnt_deposit_for_voucher",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_sub_voucher_item_voucher_id_sub_voucher_no",
                table: "vt_lgr_pmnt_sub_voucher_item",
                columns: new[] { "voucher_id", "sub_voucher_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_voucher_action_log_voucher_id",
                table: "vt_lgr_pmnt_voucher_action_log",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_voucher_cheque_action_log_voucher_cheque_id",
                table: "vt_lgr_pmnt_voucher_cheque_action_log",
                column: "voucher_cheque_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_voucher_cheque_log_voucher_cheque_id",
                table: "vt_lgr_pmnt_voucher_cheque_log",
                column: "voucher_cheque_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_voucher_cheque_voucher_items_voucher_cheque_id",
                table: "vt_lgr_pmnt_voucher_cheque_voucher_items",
                column: "voucher_cheque_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_voucher_cross_orders_voucher_item_id",
                table: "vt_lgr_pmnt_voucher_cross_orders",
                column: "voucher_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_voucher_doucument_voucher_id",
                table: "vt_lgr_pmnt_voucher_doucument",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_voucher_invoice_voucher_id",
                table: "vt_lgr_pmnt_voucher_invoice",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_voucher_line_voucher_id",
                table: "vt_lgr_pmnt_voucher_line",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_voucher_line_log_voucher_log_id",
                table: "vt_lgr_pmnt_voucher_line_log",
                column: "voucher_log_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_pmnt_voucher_log_voucher_id",
                table: "vt_lgr_pmnt_voucher_log",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_vote_fr66_items_from_vote_fr66_id",
                table: "vt_lgr_vote_fr66_items_from",
                column: "vote_fr66_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_vote_fr66_items_to_vote_fr66_id",
                table: "vt_lgr_vote_fr66_items_to",
                column: "vote_fr66_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_vote_journal_items_from_vote_journal_adjustment_id",
                table: "vt_lgr_vote_journal_items_from",
                column: "vote_journal_adjustment_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_vote_journal_items_to_vote_journal_adjustment_id",
                table: "vt_lgr_vote_journal_items_to",
                column: "vote_journal_adjustment_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_lgr_voucher_sub_line_voucher_line_id",
                table: "vt_lgr_voucher_sub_line",
                column: "voucher_line_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_vote_balance_vt_vote_bal_vote_id_vt_vote_bal_year_vt_vote~",
                table: "vt_vote_balance",
                columns: new[] { "vt_vote_bal_vote_id", "vt_vote_bal_year", "vt_vote_bal_status" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vt_vote_balance_action_log_vote_balance_id",
                table: "vt_vote_balance_action_log",
                column: "vote_balance_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_vote_balance_log_vt_vote_bal_lg_vote_bal_id",
                table: "vt_vote_balance_log",
                column: "vt_vote_bal_lg_vote_bal_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_bal_details");

            migrationBuilder.DropTable(
                name: "account_transfer_refunding");

            migrationBuilder.DropTable(
                name: "deposit_sub_info");

            migrationBuilder.DropTable(
                name: "deposits");

            migrationBuilder.DropTable(
                name: "final_account_sequence_numbers");

            migrationBuilder.DropTable(
                name: "special_ledger_accounts");

            migrationBuilder.DropTable(
                name: "sub_imprest_documents");

            migrationBuilder.DropTable(
                name: "sub_imprest_settlement_corss_orders");

            migrationBuilder.DropTable(
                name: "trade_tax_business_subnatures");

            migrationBuilder.DropTable(
                name: "trade_tax_tax_values");

            migrationBuilder.DropTable(
                name: "trade_tax_vote_assignments");

            migrationBuilder.DropTable(
                name: "vt_balancesheet_balance");

            migrationBuilder.DropTable(
                name: "vt_balsheet_subtitle");

            migrationBuilder.DropTable(
                name: "vt_cash_book");

            migrationBuilder.DropTable(
                name: "vt_cash_book_daily_balance");

            migrationBuilder.DropTable(
                name: "vt_inc_sub_project");

            migrationBuilder.DropTable(
                name: "vt_inc_subtitle");

            migrationBuilder.DropTable(
                name: "vt_ledger_book");

            migrationBuilder.DropTable(
                name: "vt_ledger_book_daily_balance");

            migrationBuilder.DropTable(
                name: "vt_lgr_internal_journal_transfer");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_commitment_action_log");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_commitmentline_votes");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_commitmentline_votes_log");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_deposit_for_voucher");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_action_log");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_cheque_action_log");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_cheque_log");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_cheque_voucher_items");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_cross_orders");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_doucument");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_invoice");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_line_log");

            migrationBuilder.DropTable(
                name: "vt_lgr_vote_cut_province");

            migrationBuilder.DropTable(
                name: "vt_lgr_vote_fr66_items_from");

            migrationBuilder.DropTable(
                name: "vt_lgr_vote_fr66_items_to");

            migrationBuilder.DropTable(
                name: "vt_lgr_vote_journal_items_from");

            migrationBuilder.DropTable(
                name: "vt_lgr_vote_journal_items_to");

            migrationBuilder.DropTable(
                name: "vt_lgr_vote_supplementary");

            migrationBuilder.DropTable(
                name: "vt_lgr_voucher_sub_line");

            migrationBuilder.DropTable(
                name: "vt_vote_balance_action_log");

            migrationBuilder.DropTable(
                name: "vt_vote_balance_log");

            migrationBuilder.DropTable(
                name: "account_details");

            migrationBuilder.DropTable(
                name: "account_transfer");

            migrationBuilder.DropTable(
                name: "special_ledger_account_type");

            migrationBuilder.DropTable(
                name: "sub_imprest_settlement");

            migrationBuilder.DropTable(
                name: "trade_tax_business_natures");

            migrationBuilder.DropTable(
                name: "vt_balsheet_title");

            migrationBuilder.DropTable(
                name: "vt_inc_project");

            migrationBuilder.DropTable(
                name: "vt_inc_title");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_commitment_line");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_commitment_line_log");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_cheque");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_sub_voucher_item");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_log");

            migrationBuilder.DropTable(
                name: "vt_lgr_vote_fr66_transfer");

            migrationBuilder.DropTable(
                name: "vt_lgr_vote_journal_adjustments");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher_line");

            migrationBuilder.DropTable(
                name: "vt_vote_balance");

            migrationBuilder.DropTable(
                name: "SpecialLedgerAccountTypesResource");

            migrationBuilder.DropTable(
                name: "sub_imprest");

            migrationBuilder.DropTable(
                name: "vt_programme");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_commitment_log");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_voucher");

            migrationBuilder.DropTable(
                name: "vt_vote_details");

            migrationBuilder.DropTable(
                name: "vt_lgr_pmnt_commitment");
        }
    }
}
