using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.MixinDb
{
    public partial class update_mx_db_with_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "banking",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    banked_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    office_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banking", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "mixin_order",
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
                    cheque_number = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cheque_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    cheque_bank_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    state = table.Column<int>(type: "int", nullable: false, defaultValueSql: "1"),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    ir_session_id = table.Column<int>(type: "int", nullable: false),
                    res_payment_method_id = table.Column<int>(type: "int", nullable: false),
                    gn_division_id = table.Column<int>(type: "int", nullable: false),
                    cashier_id = table.Column<int>(type: "int", nullable: true),
                    res_partner_id = table.Column<int>(type: "int", nullable: true),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    account_detail_id = table.Column<int>(type: "int", nullable: true),
                    business_id = table.Column<int>(type: "int", nullable: true),
                    app_category_id = table.Column<int>(type: "int", nullable: true),
                    business_tax_id = table.Column<int>(type: "int", nullable: true),
                    trade_license_status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    tax_type_id = table.Column<int>(type: "int", nullable: true),
                    assessment_id = table.Column<int>(type: "int", nullable: true),
                    shop_id = table.Column<int>(type: "int", nullable: true),
                    water_connection_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mixin_order", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vote_assignment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    isactive = table.Column<sbyte>(type: "tinyint", nullable: false, defaultValueSql: "'1'"),
                    vote_id = table.Column<int>(type: "int", nullable: false),
                    office_id = table.Column<int>(type: "int", nullable: false),
                    bank_account_id = table.Column<int>(type: "int", nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime", nullable: true),
                    date_modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    sabha_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vote_assignment", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "mixin_cancel_order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    reason = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    ir_session_id = table.Column<int>(type: "int", nullable: false),
                    mixin_order_id = table.Column<int>(type: "int", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    approvalcomment = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mixin_cancel_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_mixin_cancel_order_mixin_order_mixin_order_id",
                        column: x => x.mixin_order_id,
                        principalTable: "mixin_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "mixin_order_line",
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
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    mixin_vote_assignment_detail_id = table.Column<int>(type: "int", nullable: false),
                    res_payment_vat_id = table.Column<int>(type: "int", nullable: true),
                    res_payment_nbt_id = table.Column<int>(type: "int", nullable: true),
                    mixin_order_id = table.Column<int>(type: "int", nullable: false),
                    vote_or_bal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mixin_order_line", x => x.id);
                    table.ForeignKey(
                        name: "FK_mixin_order_line_mixin_order_mixin_order_id",
                        column: x => x.mixin_order_id,
                        principalTable: "mixin_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "vote_assignment_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    custom_vote_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isactive = table.Column<sbyte>(type: "tinyint", nullable: false, defaultValueSql: "'1'"),
                    date_created = table.Column<DateTime>(type: "datetime", nullable: true),
                    date_modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    mixin_vote_assignment_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vote_assignment_details", x => x.id);
                    table.ForeignKey(
                        name: "mixin_vote_assignment_details_FK",
                        column: x => x.mixin_vote_assignment_id,
                        principalTable: "vote_assignment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_cancel_order_mixin_order_id",
                table: "mixin_cancel_order",
                column: "mixin_order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "mixin_cancel_order_FK",
                table: "mixin_cancel_order",
                column: "mixin_order_id");

            migrationBuilder.CreateIndex(
                name: "mixin_cancel_order_FK_1",
                table: "mixin_cancel_order",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_order_assessment_id",
                table: "mixin_order",
                column: "assessment_id");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_order_shop_id",
                table: "mixin_order",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_order_state",
                table: "mixin_order",
                column: "state");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_order_water_connection_id",
                table: "mixin_order",
                column: "water_connection_id");

            migrationBuilder.CreateIndex(
                name: "mixin_order_FK_1",
                table: "mixin_order",
                column: "gn_division_id");

            migrationBuilder.CreateIndex(
                name: "mixin_order_FK_2",
                table: "mixin_order",
                column: "ir_session_id");

            migrationBuilder.CreateIndex(
                name: "mixin_order_FK_3",
                table: "mixin_order",
                column: "res_partner_id");

            migrationBuilder.CreateIndex(
                name: "mixin_order_FK_4",
                table: "mixin_order",
                column: "res_payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_mixin_order_line_mixin_order_id",
                table: "mixin_order_line",
                column: "mixin_order_id");

            migrationBuilder.CreateIndex(
                name: "mixin_order_line_FK_4",
                table: "mixin_order_line",
                column: "mixin_vote_assignment_detail_id");

            migrationBuilder.CreateIndex(
                name: "mixin_vote_assignment_FK_1",
                table: "vote_assignment",
                column: "office_id");

            migrationBuilder.CreateIndex(
                name: "mixin_vote_assignment_FK_2",
                table: "vote_assignment",
                column: "bank_account_id");

            migrationBuilder.CreateIndex(
                name: "mixin_vote_assignment_UN",
                table: "vote_assignment",
                columns: new[] { "vote_id", "office_id", "isactive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "mixin_vote_assignment_details_UN",
                table: "vote_assignment_details",
                columns: new[] { "mixin_vote_assignment_id", "custom_vote_name", "isactive" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "banking");

            migrationBuilder.DropTable(
                name: "mixin_cancel_order");

            migrationBuilder.DropTable(
                name: "mixin_order_line");

            migrationBuilder.DropTable(
                name: "vote_assignment_details");

            migrationBuilder.DropTable(
                name: "mixin_order");

            migrationBuilder.DropTable(
                name: "vote_assignment");
        }
    }
}
