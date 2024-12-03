using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.MixinDb
{
    public partial class FirstMigV18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "vote_assignment_details",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "created_by",
                table: "vote_assignment_details",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "depth",
                table: "vote_assignment_details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "is_sub_level",
                table: "vote_assignment_details",
                type: "tinyint(1)",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "parent_id",
                table: "vote_assignment_details",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "row_version",
                table: "vote_assignment_details",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<int>(
                name: "updated_by",
                table: "vote_assignment_details",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mixin_order_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_nic_number = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_mobile_number = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    discount_rate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    discount_amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    cheque_number = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cheque_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    cheque_bank_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    state = table.Column<int>(type: "int", nullable: false, defaultValueSql: "1"),
                    ir_session_id = table.Column<int>(type: "int", nullable: false),
                    res_payment_method_id = table.Column<int>(type: "int", nullable: false),
                    gn_division_id = table.Column<int>(type: "int", nullable: false),
                    cashier_id = table.Column<int>(type: "int", nullable: true),
                    res_partner_id = table.Column<int>(type: "int", nullable: true, defaultValue: 60),
                    hrm_employee_id = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    account_detail_id = table.Column<int>(type: "int", nullable: true),
                    business_id = table.Column<int>(type: "int", nullable: true),
                    app_category_id = table.Column<int>(type: "int", nullable: true),
                    business_tax_id = table.Column<int>(type: "int", nullable: true),
                    trade_license_status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    tax_type_id = table.Column<int>(type: "int", nullable: true),
                    online_payment_det_id = table.Column<int>(type: "int", nullable: true),
                    assessment_id = table.Column<int>(type: "int", nullable: true),
                    shop_id = table.Column<int>(type: "int", nullable: true),
                    water_connection_id = table.Column<int>(type: "int", nullable: true),
                    assmt_bal_by_excess_deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mixin_order_log", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "mixin_order_line_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    custom_vote_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    payment_vat_amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    payment_nbt_amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    stamp_amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    mixin_vote_assignment_detail_id = table.Column<int>(type: "int", nullable: false),
                    vote_detail_id = table.Column<int>(type: "int", nullable: true),
                    classification_id = table.Column<int>(type: "int", nullable: true),
                    res_payment_vat_id = table.Column<int>(type: "int", nullable: true),
                    res_payment_nbt_id = table.Column<int>(type: "int", nullable: true),
                    mixin_order_id = table.Column<int>(type: "int", nullable: false),
                    vote_or_bal = table.Column<int>(type: "int", nullable: false),
                    vote_ptype_id = table.Column<int>(type: "int", nullable: true),
                    assmt_gross_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    assmt_discount_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    assmt_discount_rate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MixinOrderLogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mixin_order_line_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_mixin_order_line_log_mixin_order_log_MixinOrderLogId",
                        column: x => x.MixinOrderLogId,
                        principalTable: "mixin_order_log",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_order_line_log_MixinOrderLogId",
                table: "mixin_order_line_log",
                column: "MixinOrderLogId");

            migrationBuilder.CreateIndex(
                name: "mixin_order_line_log_FK_4",
                table: "mixin_order_line_log",
                column: "mixin_vote_assignment_detail_id");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_order_log_assessment_id",
                table: "mixin_order_log",
                column: "assessment_id");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_order_log_shop_id",
                table: "mixin_order_log",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_order_log_state",
                table: "mixin_order_log",
                column: "state");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_order_log_water_connection_id",
                table: "mixin_order_log",
                column: "water_connection_id");

            migrationBuilder.CreateIndex(
                name: "mixin_order_log_FK_1",
                table: "mixin_order_log",
                column: "gn_division_id");

            migrationBuilder.CreateIndex(
                name: "mixin_order_log_FK_2",
                table: "mixin_order_log",
                column: "ir_session_id");

            migrationBuilder.CreateIndex(
                name: "mixin_order_log_FK_3",
                table: "mixin_order_log",
                column: "res_partner_id");

            migrationBuilder.CreateIndex(
                name: "mixin_order_log_FK_4",
                table: "mixin_order_log",
                column: "res_payment_method_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mixin_order_line_log");

            migrationBuilder.DropTable(
                name: "mixin_order_log");

            migrationBuilder.DropColumn(
                name: "code",
                table: "vote_assignment_details");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "vote_assignment_details");

            migrationBuilder.DropColumn(
                name: "depth",
                table: "vote_assignment_details");

            migrationBuilder.DropColumn(
                name: "is_sub_level",
                table: "vote_assignment_details");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "vote_assignment_details");

            migrationBuilder.DropColumn(
                name: "row_version",
                table: "vote_assignment_details");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "vote_assignment_details");
        }
    }
}
