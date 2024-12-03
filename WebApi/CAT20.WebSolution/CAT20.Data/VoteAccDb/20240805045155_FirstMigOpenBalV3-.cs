using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigOpenBalV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vt_obl_creditor_billing",
                columns: table => new
                {
                    cr_bl_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cr_bl_ledger_account_id = table.Column<int>(type: "int", nullable: false),
                    cr_bl_description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cr_bl_creditor_id = table.Column<int>(type: "int", nullable: false),
                    cr_bl_creditor_cetegory = table.Column<int>(type: "int", nullable: false),
                    cr_bl_year = table.Column<int>(type: "int", nullable: false),
                    cr_bl_month = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    cr_bl_office_id = table.Column<int>(type: "int", nullable: false),
                    cr_bl_sabha_id = table.Column<int>(type: "int", nullable: false),
                    cr_bl_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: false),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_creditor_billing", x => x.cr_bl_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_obl_fixed_assets",
                columns: table => new
                {
                    fxa_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fxa_ledger_account_id = table.Column<int>(type: "int", nullable: true),
                    fxa_assets_title = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fxa_assets_reg_no = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fxa_balance_type = table.Column<int>(type: "int", nullable: false),
                    fxa_acquired_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fxa_grant_ledger_account_id = table.Column<int>(type: "int", nullable: true),
                    fxa_balance_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    fxa_grant_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    fxa_accumulated_revenue_recognition = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    fxa_office_id = table.Column<int>(type: "int", nullable: false),
                    fxa_sabha_id = table.Column<int>(type: "int", nullable: false),
                    fxa_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: false),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_fixed_assets", x => x.fxa_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_obl_fixed_deposits",
                columns: table => new
                {
                    fd_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fd_type_vote_id = table.Column<int>(type: "int", nullable: false),
                    fd_bank_branch_id = table.Column<int>(type: "int", nullable: false),
                    fd_reference = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fd_interest_rate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    fd_fd_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    fd_deposit_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fd_duration = table.Column<int>(type: "int", nullable: false),
                    fd_renewable_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fd_office_id = table.Column<int>(type: "int", nullable: false),
                    fd_sabha_id = table.Column<int>(type: "int", nullable: false),
                    fd_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: false),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_fixed_deposits", x => x.fd_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_obl_industrial_creditors_debtors_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: true),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_industrial_creditors_debtors_types", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_obl_la_loan",
                columns: table => new
                {
                    lal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    lal_loan_type_vote = table.Column<int>(type: "int", nullable: false),
                    lal_bank_branch_id = table.Column<int>(type: "int", nullable: true),
                    lal_loan_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    lal_interest_rate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    lal_installment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    lal_borrowing_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    lal_duration = table.Column<int>(type: "int", nullable: false),
                    lal_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    lal_loan_purpose = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lal_mortgage = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lal_office_id = table.Column<int>(type: "int", nullable: false),
                    lal_sabha_id = table.Column<int>(type: "int", nullable: false),
                    lal_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: false),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_la_loan", x => x.lal_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_obl_pre_paid_payments",
                columns: table => new
                {
                    prepay_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    prepay_category_vote = table.Column<int>(type: "int", nullable: false),
                    prepay_paid_to_id = table.Column<int>(type: "int", nullable: false),
                    prepay_to_category = table.Column<int>(type: "int", nullable: false),
                    prepay_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    prepay_description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prepay_office_id = table.Column<int>(type: "int", nullable: false),
                    prepay_sabha_id = table.Column<int>(type: "int", nullable: false),
                    prepay_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: false),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_pre_paid_payments", x => x.prepay_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_obl_receivable_exchange_non_exchange",
                columns: table => new
                {
                    r_ex_nex_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    r_ex_nex_exchnage_type = table.Column<int>(type: "int", nullable: false),
                    r_ex_nex_ledger_account_id = table.Column<int>(type: "int", nullable: false),
                    r_ex_nex_receivable_category = table.Column<int>(type: "int", nullable: true),
                    r_ex_nex_receivable_from_id = table.Column<int>(type: "int", nullable: false),
                    r_ex_nex_financial_year = table.Column<int>(type: "int", nullable: false),
                    r_ex_nex_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    r_ex_nex_office_id = table.Column<int>(type: "int", nullable: false),
                    r_ex_nex_sabha_id = table.Column<int>(type: "int", nullable: false),
                    r_ex_nex_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: false),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_receivable_exchange_non_exchange", x => x.r_ex_nex_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_obl_sabha_fund_source",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    account_system = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: true),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: false),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_sabha_fund_source", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_obl_stores_creditors",
                columns: table => new
                {
                    stc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    stc_ledger_account_id = table.Column<int>(type: "int", nullable: false),
                    stc_supplier_category = table.Column<int>(type: "int", nullable: false),
                    stc_supplier_id = table.Column<int>(type: "int", nullable: false),
                    stc_order_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stc_purchasing_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    stc_received_number = table.Column<int>(type: "int", nullable: false),
                    stc_grn = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stc_invoice_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    stc_office_id = table.Column<int>(type: "int", nullable: false),
                    stc_sabha_id = table.Column<int>(type: "int", nullable: false),
                    stc_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: false),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_stores_creditors", x => x.stc_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_obl_industrial_creditors",
                columns: table => new
                {
                    idc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idc_creditor_type_id = table.Column<int>(type: "int", nullable: false),
                    idc_fund_source_id = table.Column<int>(type: "int", nullable: false),
                    idc_category_vote = table.Column<int>(type: "int", nullable: false),
                    idc_creditor_id = table.Column<int>(type: "int", nullable: false),
                    idc_project_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idc_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    idc_creditor_category = table.Column<int>(type: "int", nullable: false),
                    idc_office_id = table.Column<int>(type: "int", nullable: false),
                    idc_sabha_id = table.Column<int>(type: "int", nullable: false),
                    idc_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: false),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_industrial_creditors", x => x.idc_id);
                    table.ForeignKey(
                        name: "fk_creditor_type_sabha_fund_dource",
                        column: x => x.idc_creditor_type_id,
                        principalTable: "vt_obl_industrial_creditors_debtors_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_industrial_creditors_sabha_fund_dource",
                        column: x => x.idc_fund_source_id,
                        principalTable: "vt_obl_sabha_fund_source",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_obl_industrial_debtors",
                columns: table => new
                {
                    idb_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idb_debtor_type_id = table.Column<int>(type: "int", nullable: false),
                    idb_fund_source_id = table.Column<int>(type: "int", nullable: false),
                    idb_category_vote = table.Column<int>(type: "int", nullable: false),
                    idb_debtor_category = table.Column<int>(type: "int", nullable: false),
                    idb_debtor_id = table.Column<int>(type: "int", nullable: false),
                    idb_project_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idb_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    idc_office_id = table.Column<int>(type: "int", nullable: false),
                    idc_sabha_id = table.Column<int>(type: "int", nullable: false),
                    idc_status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    system_create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    system_update_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_create_by = table.Column<int>(type: "int", nullable: false),
                    system_update_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_obl_industrial_debtors", x => x.idb_id);
                    table.ForeignKey(
                        name: "fk_debtor_type_sabha_fund_dource",
                        column: x => x.idb_debtor_type_id,
                        principalTable: "vt_obl_industrial_creditors_debtors_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_industrial_debtors_sabha_fund_dource",
                        column: x => x.idb_fund_source_id,
                        principalTable: "vt_obl_sabha_fund_source",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_vt_obl_industrial_creditors_idc_creditor_type_id",
                table: "vt_obl_industrial_creditors",
                column: "idc_creditor_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_obl_industrial_creditors_idc_fund_source_id",
                table: "vt_obl_industrial_creditors",
                column: "idc_fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_obl_industrial_debtors_idb_debtor_type_id",
                table: "vt_obl_industrial_debtors",
                column: "idb_debtor_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_obl_industrial_debtors_idb_fund_source_id",
                table: "vt_obl_industrial_debtors",
                column: "idb_fund_source_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vt_obl_creditor_billing");

            migrationBuilder.DropTable(
                name: "vt_obl_fixed_assets");

            migrationBuilder.DropTable(
                name: "vt_obl_fixed_deposits");

            migrationBuilder.DropTable(
                name: "vt_obl_industrial_creditors");

            migrationBuilder.DropTable(
                name: "vt_obl_industrial_debtors");

            migrationBuilder.DropTable(
                name: "vt_obl_la_loan");

            migrationBuilder.DropTable(
                name: "vt_obl_pre_paid_payments");

            migrationBuilder.DropTable(
                name: "vt_obl_receivable_exchange_non_exchange");

            migrationBuilder.DropTable(
                name: "vt_obl_stores_creditors");

            migrationBuilder.DropTable(
                name: "vt_obl_industrial_creditors_debtors_types");

            migrationBuilder.DropTable(
                name: "vt_obl_sabha_fund_source");
        }
    }
}
