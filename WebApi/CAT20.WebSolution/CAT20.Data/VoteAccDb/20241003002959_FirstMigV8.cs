using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.VoteAccDb
{
    public partial class FirstMigV8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "vt_vote_bal_lg_cstm_vt_total_credit",
                table: "vt_vote_balance_log",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "vt_vote_bal_lg_cstm_vt_total_debit",
                table: "vt_vote_balance_log",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "vt_vote_bal_cstm_vt_credit",
                table: "vt_vote_balance",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "vt_vote_bal_cstm_vt_debit",
                table: "vt_vote_balance",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "vt_custom_vote_balance",
                columns: table => new
                {
                    vt_cstm_vote_bal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_cstm_vote_bal__csmtvote_id = table.Column<int>(type: "int", nullable: false),
                    vt_cstm_vote_bal_csmt_vote_detail_parent_id = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_classification_id = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_depth = table.Column<int>(type: "int", nullable: false),
                    vt_cstm_vote_bal_parent_id = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_is_sublevel = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    vt_cstm_vote_bal_sabha_id = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_year = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_month = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_status = table.Column<int>(type: "int", nullable: false),
                    vt_cstm_vote_bal_transaction_type = table.Column<int>(type: "int", nullable: false),
                    vt_cstm_vote_bal_is_carry_forward = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false),
                    vt_cstm_vote_bal_estimated_income_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: true, defaultValue: 0m),
                    vt_cstm_vote_bal_allocation_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: true),
                    vt_cstm_vote_bal_take_hold_rate = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_cstm_vote_bal_take_hold_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_cstm_vote_bal_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_children_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_children_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_total_commited = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_total_hold = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_total_pending = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_credit_debit_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_running_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_carry_forward_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_carry_forward_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_credit_debit_carry_forward_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_surcharge_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_surcharge_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_credit_debit_surcharge_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_transfer_flag = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    session_id_by_office = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    office_id = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_custom_vote_balance", x => x.vt_cstm_vote_bal_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_custom_vote_balance_action_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cstm_vote_balance_id = table.Column<int>(type: "int", nullable: true),
                    action_state = table.Column<int>(type: "int", nullable: true),
                    action_by = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vt_custom_vote_balance_action_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_vt_custom_vote_balance_action_log_vt_custom_vote_balance_cst~",
                        column: x => x.cstm_vote_balance_id,
                        principalTable: "vt_custom_vote_balance",
                        principalColumn: "vt_cstm_vote_bal_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vt_custom_vote_balance_log",
                columns: table => new
                {
                    vt_cstm_vote_bal_lg_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vt_cstm_vote_bal_lg_csmt_vote_bal_id = table.Column<int>(type: "int", nullable: false),
                    vt_cstm_vote_bal_lg_csmt_vote_detail_parent_id = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_cus_vote_bal_lg_csmt_vote_id = table.Column<int>(type: "int", nullable: false),
                    vt_cstm_vote_bal_lg_depth = table.Column<int>(type: "int", nullable: false),
                    vt_cstm_vote_bal_lg_parent_id = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_lg_is_sublevel = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    vt_cstm_vote_bal_lg_sabha_id = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_lg_year = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_lg_month = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_lg_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_cstm_vote_bal_lg_sub_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_cstm_vote_bal_lg_description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vt_cstm_vote_bal_lg_status = table.Column<int>(type: "int", nullable: false),
                    vt_cstm_vote_bal_lg_transaction_type = table.Column<int>(type: "int", nullable: false),
                    vt_cstm_vote_bal_lg_is_carry_forward = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false),
                    vt_cstm_vote_bal_lg_estimated_income_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: true, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_allocation_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: true, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_take_hold_rate = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_cstm_vote_bal_lg_take_hold_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_cstm_vote_bal_lg_allocation_exchange_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: true, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_children_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_children_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_total_commited = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_total_hold = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_total_lg_pending = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_exchange_amount = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_credit_debit_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_running_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    vt_cstm_vote_bal_lg_carry_forward_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_carry_forward_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_credit_debit_carry_forward_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_surcharge_total_debit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_surcharge_total_credit = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_credit_debit_surcharge_balance = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false, defaultValue: 0m),
                    vt_cstm_vote_bal_lg_module_primary_key = table.Column<int>(type: "int", nullable: true),
                    vt_cstm_vote_bal_lg_app_category = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_vt_custom_vote_balance_log", x => x.vt_cstm_vote_bal_lg_id);
                    table.ForeignKey(
                        name: "FK_vt_custom_vote_balance_log_vt_custom_vote_balance_vt_cstm_vo~",
                        column: x => x.vt_cstm_vote_bal_lg_csmt_vote_bal_id,
                        principalTable: "vt_custom_vote_balance",
                        principalColumn: "vt_cstm_vote_bal_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_vt_custom_vote_balance_vt_cstm_vote_bal__csmtvote_id_vt_cstm~",
                table: "vt_custom_vote_balance",
                columns: new[] { "vt_cstm_vote_bal__csmtvote_id", "vt_cstm_vote_bal_year", "vt_cstm_vote_bal_status" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vt_custom_vote_balance_action_log_cstm_vote_balance_id",
                table: "vt_custom_vote_balance_action_log",
                column: "cstm_vote_balance_id");

            migrationBuilder.CreateIndex(
                name: "IX_vt_custom_vote_balance_log_vt_cstm_vote_bal_lg_csmt_vote_bal~",
                table: "vt_custom_vote_balance_log",
                column: "vt_cstm_vote_bal_lg_csmt_vote_bal_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vt_custom_vote_balance_action_log");

            migrationBuilder.DropTable(
                name: "vt_custom_vote_balance_log");

            migrationBuilder.DropTable(
                name: "vt_custom_vote_balance");

            migrationBuilder.DropColumn(
                name: "vt_vote_bal_lg_cstm_vt_total_credit",
                table: "vt_vote_balance_log");

            migrationBuilder.DropColumn(
                name: "vt_vote_bal_lg_cstm_vt_total_debit",
                table: "vt_vote_balance_log");

            migrationBuilder.DropColumn(
                name: "vt_vote_bal_cstm_vt_credit",
                table: "vt_vote_balance");

            migrationBuilder.DropColumn(
                name: "vt_vote_bal_cstm_vt_debit",
                table: "vt_vote_balance");
        }
    }
}
