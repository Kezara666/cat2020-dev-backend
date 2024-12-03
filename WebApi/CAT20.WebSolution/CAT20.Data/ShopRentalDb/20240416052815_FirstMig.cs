using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class FirstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sr_process_configuration",
                columns: table => new
                {
                    sr_processConfig_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_processConfig_sabha_id = table.Column<int>(type: "int", nullable: false),
                    sr_processConfig_fine_rate = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    sr_processConfig_fine_date = table.Column<int>(type: "int", nullable: false),
                    sr_processConfig_rental_payment_date_type = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sr_processConfig_fine_cal_type = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sr_processConfig_status = table.Column<int>(type: "int", nullable: true),
                    sr_processConfig_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_processConfig_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_processConfig_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_processConfig_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_process_configuration", x => x.sr_processConfig_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_property_nature",
                columns: table => new
                {
                    sr_property_nature_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_property_nature_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_property_nature_status = table.Column<int>(type: "int", nullable: true),
                    sr_property_nature_sabha_id = table.Column<int>(type: "int", nullable: true),
                    sr_property_nature_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_property_nature_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_property_nature_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_property_nature_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_property_nature", x => x.sr_property_nature_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_property_type",
                columns: table => new
                {
                    sr_property_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_property_type_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_property_type_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_property_type", x => x.sr_property_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_rental_place",
                columns: table => new
                {
                    sr_rental_place_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_rental_place_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_rental_place_status = table.Column<int>(type: "int", nullable: true),
                    sr_rental_place_code = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_rental_place_office_id = table.Column<int>(type: "int", nullable: true),
                    sr_rental_place_sabha_id = table.Column<int>(type: "int", nullable: true),
                    sr_rental_place_gn_division_id = table.Column<int>(type: "int", nullable: true),
                    sr_rental_place_address_line1 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_rental_place_address_line2 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeStamp = table.Column<DateTime>(type: "Timestamp", rowVersion: true, nullable: true),
                    sr_rental_place_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_rental_place_updated_by = table.Column<int>(type: "int", nullable: true),
                    sr_rental_place_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_rental_place_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_rental_place", x => x.sr_rental_place_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_vote_payment_types",
                columns: table => new
                {
                    sr_vt_pay_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_vt_pay_type_desc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_vt_pay_type_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_vote_payment_types", x => x.sr_vt_pay_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_floor",
                columns: table => new
                {
                    sr_floor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_floor_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_floor_number = table.Column<int>(type: "int", nullable: true),
                    sr_floor_status = table.Column<int>(type: "int", nullable: true),
                    sr_floor_rental_place_id = table.Column<int>(type: "int", nullable: false),
                    sr_floor_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_floor_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_floor_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_floor_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_floor_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_floor", x => x.sr_floor_id);
                    table.ForeignKey(
                        name: "sr_floor_ibfk_1",
                        column: x => x.sr_floor_rental_place_id,
                        principalTable: "sr_rental_place",
                        principalColumn: "sr_rental_place_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_property",
                columns: table => new
                {
                    sr_property_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_property_no = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_property_status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'1'"),
                    sr_property_property_type_id = table.Column<int>(type: "int", nullable: false),
                    sr_property_floor_id = table.Column<int>(type: "int", nullable: true),
                    sr_property_office_id = table.Column<int>(type: "int", nullable: true),
                    sr_property_sabha_id = table.Column<int>(type: "int", nullable: true),
                    sr_property_property_nature_id = table.Column<int>(type: "int", nullable: false),
                    sr_property_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_property_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_property_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_property_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_property", x => x.sr_property_id);
                    table.ForeignKey(
                        name: "sr_floor_idfk_3",
                        column: x => x.sr_property_floor_id,
                        principalTable: "sr_floor",
                        principalColumn: "sr_floor_id");
                    table.ForeignKey(
                        name: "sr_property_ibfk_3",
                        column: x => x.sr_property_property_type_id,
                        principalTable: "sr_property_type",
                        principalColumn: "sr_property_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sr_property_natureidfk_3",
                        column: x => x.sr_property_property_nature_id,
                        principalTable: "sr_property_nature",
                        principalColumn: "sr_property_nature_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_shop",
                columns: table => new
                {
                    sr_shop_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_shop_property_id = table.Column<int>(type: "int", nullable: true),
                    sr_shop_business_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_shop_business_nature = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_shop_business_registration_number = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_shop_customer_id = table.Column<int>(type: "int", nullable: true),
                    sr_shop_customer_designtion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_shop_agreement_no = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_shop_agreement_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    sr_shop_agreement_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    sr_shop_agreement_close_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    sr_shop_rental = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    sr_shop_key_money = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    sr_shop_security_deposit = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    sr_shop_service_charge = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    sr_shop_status = table.Column<int>(type: "int", nullable: true),
                    sr_shop_is_approved = table.Column<int>(type: "int", nullable: true),
                    sr_shop_approved_by = table.Column<int>(type: "int", nullable: true),
                    sr_shop_approved_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_shop_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_shop_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_shop_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_shop_updated_by = table.Column<int>(type: "int", nullable: true),
                    sr_shop_office_id = table.Column<int>(type: "int", nullable: true),
                    sr_shop_sabha_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_shop", x => x.sr_shop_id);
                    table.ForeignKey(
                        name: "sr_shop_ibfk_3",
                        column: x => x.sr_shop_property_id,
                        principalTable: "sr_property",
                        principalColumn: "sr_property_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_agreement_change_request",
                columns: table => new
                {
                    sr_agcr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_agcr_shop_id = table.Column<int>(type: "int", nullable: false),
                    sr_agcr_requestedstatus = table.Column<int>(type: "int", nullable: false),
                    sr_agcr_agreement_close_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    sr_agcr_office_id = table.Column<int>(type: "int", nullable: true),
                    sr_agcr_sabha_id = table.Column<int>(type: "int", nullable: true),
                    sr_agcr_created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_agcr_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_agcr_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_agcr_updated_by = table.Column<int>(type: "int", nullable: true),
                    sr_agcr_approved_at = table.Column<DateTime>(type: "datetime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    sr_agcr_approved_by = table.Column<int>(type: "int", nullable: true),
                    sr_agcr_approve_comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_ob_approve_status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_agcrId", x => x.sr_agcr_id);
                    table.ForeignKey(
                        name: "sr_agcr_shop_FK",
                        column: x => x.sr_agcr_shop_id,
                        principalTable: "sr_shop",
                        principalColumn: "sr_shop_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_opening_balance",
                columns: table => new
                {
                    sr_ob_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_ob_property_id = table.Column<int>(type: "int", nullable: false),
                    sr_ob_shop_id = table.Column<int>(type: "int", nullable: false),
                    sr_ob_year = table.Column<int>(type: "int", nullable: false),
                    sr_ob_month_id = table.Column<int>(type: "int", nullable: false),
                    sr_ob_last_year_arrears_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sr_ob_this_year_arrears_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sr_ob_last_year_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sr_ob_this_year_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sr_ob_over_payment_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sr_ob_service_charge_arreas_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sr_ob_current_service_charge_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sr_ob_current_rental_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sr_ob_bal_id_for_last_year_arrears = table.Column<int>(type: "int", nullable: true),
                    sr_ob_bal_bal_id_for_current_year_bal = table.Column<int>(type: "int", nullable: false),
                    sr_ob_status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'1'"),
                    sr_ob_office_id = table.Column<int>(type: "int", nullable: true),
                    sr_ob_sabha_id = table.Column<int>(type: "int", nullable: true),
                    sr_ob_created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_ob_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_ob_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_ob_updated_by = table.Column<int>(type: "int", nullable: true),
                    sr_bal_is_processed = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'"),
                    sr_ob_approved_at = table.Column<DateTime>(type: "datetime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    sr_ob_approved_by = table.Column<int>(type: "int", nullable: true),
                    sr_ob_approve_comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_ob_approve_status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_opening_balance", x => x.sr_ob_id);
                    table.ForeignKey(
                        name: "sr_opening_balance_FK",
                        column: x => x.sr_ob_property_id,
                        principalTable: "sr_property",
                        principalColumn: "sr_property_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sr_opening_balance_FK_1",
                        column: x => x.sr_ob_shop_id,
                        principalTable: "sr_shop",
                        principalColumn: "sr_shop_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_shopRental_balance",
                columns: table => new
                {
                    sr_bal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_bal_proprty_id = table.Column<int>(type: "int", nullable: false),
                    sr_bal_shop_id = table.Column<int>(type: "int", nullable: false),
                    sr_bal_year = table.Column<int>(type: "int", nullable: false),
                    sr_bal_month = table.Column<int>(type: "int", nullable: false),
                    sr_bal_from_date = table.Column<DateOnly>(type: "date", nullable: false),
                    sr_bal_to_date = table.Column<DateOnly>(type: "date", nullable: true),
                    sr_bal_bill_process_date = table.Column<DateOnly>(type: "date", nullable: false),
                    sr_bal_arrears_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_paid_arrears_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_paid_fine_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_service_charge_arreas_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_paid_service_charge_arreas_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValueSql: "'0'"),
                    sr_bal_over_payment_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    sr_bal_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sr_bal_is_processed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'0'"),
                    sr_bal_no_of_payments = table.Column<int>(type: "int", nullable: true),
                    sr_bal_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_bal_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_bal_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_bal_updated_by = table.Column<int>(type: "int", nullable: true),
                    sr_bal_has_transaction = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_shopRental_balance", x => x.sr_bal_id);
                    table.ForeignKey(
                        name: "sr_bal_FK_property",
                        column: x => x.sr_bal_proprty_id,
                        principalTable: "sr_property",
                        principalColumn: "sr_property_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sr_bal_FK_shop",
                        column: x => x.sr_bal_shop_id,
                        principalTable: "sr_shop",
                        principalColumn: "sr_shop_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_shopRental_vote_assign",
                columns: table => new
                {
                    sr_vote_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_vote_property_id = table.Column<int>(type: "int", nullable: false),
                    sr_vote_shop_id = table.Column<int>(type: "int", nullable: false),
                    sr_vote_property_rental_id = table.Column<int>(type: "int", nullable: false),
                    sr_vote_last_year_arreas_amount_id = table.Column<int>(type: "int", nullable: false),
                    sr_vote_this_year_arreas_amount_id = table.Column<int>(type: "int", nullable: false),
                    sr_vote_last_year_fine_id = table.Column<int>(type: "int", nullable: false),
                    sr_vote_this_year_fine_id = table.Column<int>(type: "int", nullable: false),
                    sr_vote_service_charge_arreas_amount_id = table.Column<int>(type: "int", nullable: false),
                    sr_vote_service_charge_id = table.Column<int>(type: "int", nullable: false),
                    sr_vote_over_payment_id = table.Column<int>(type: "int", nullable: false),
                    sr_vote_status = table.Column<int>(type: "int", nullable: true),
                    sr_vote_office_id = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    sr_vote_sabha_id = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    sr_vote_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_bal_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_vote_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_vote_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_shopRental_vote_assign", x => x.sr_vote_id);
                    table.ForeignKey(
                        name: "sr_vote_FK_property",
                        column: x => x.sr_vote_property_id,
                        principalTable: "sr_property",
                        principalColumn: "sr_property_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sr_vote_Fk_shop",
                        column: x => x.sr_vote_shop_id,
                        principalTable: "sr_shop",
                        principalColumn: "sr_shop_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_sr_agreement_change_request_sr_agcr_shop_id",
                table: "sr_agreement_change_request",
                column: "sr_agcr_shop_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sr_floor_sr_floor_rental_place_id",
                table: "sr_floor",
                column: "sr_floor_rental_place_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_opening_balance_sr_ob_property_id",
                table: "sr_opening_balance",
                column: "sr_ob_property_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_opening_balance_sr_ob_shop_id",
                table: "sr_opening_balance",
                column: "sr_ob_shop_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sr_opening_balance_sr_ob_shop_id_sr_ob_year_sr_ob_month_id",
                table: "sr_opening_balance",
                columns: new[] { "sr_ob_shop_id", "sr_ob_year", "sr_ob_month_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sr_process_configuration_sr_processConfig_sabha_id_sr_proces~",
                table: "sr_process_configuration",
                columns: new[] { "sr_processConfig_sabha_id", "sr_processConfig_fine_rate", "sr_processConfig_fine_cal_type", "sr_processConfig_rental_payment_date_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sr_property_sr_property_floor_id_sr_property_no",
                table: "sr_property",
                columns: new[] { "sr_property_floor_id", "sr_property_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sr_property_sr_property_property_nature_id",
                table: "sr_property",
                column: "sr_property_property_nature_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_property_sr_property_property_type_id",
                table: "sr_property",
                column: "sr_property_property_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_shop_sr_shop_office_id_sr_shop_agreement_no",
                table: "sr_shop",
                columns: new[] { "sr_shop_office_id", "sr_shop_agreement_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sr_shop_sr_shop_property_id",
                table: "sr_shop",
                column: "sr_shop_property_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_shopRental_balance_sr_bal_proprty_id",
                table: "sr_shopRental_balance",
                column: "sr_bal_proprty_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_shopRental_balance_sr_bal_shop_id_sr_bal_year_sr_bal_month",
                table: "sr_shopRental_balance",
                columns: new[] { "sr_bal_shop_id", "sr_bal_year", "sr_bal_month" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sr_shopRental_vote_assign_sr_vote_property_id",
                table: "sr_shopRental_vote_assign",
                column: "sr_vote_property_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_shopRental_vote_assign_sr_vote_shop_id",
                table: "sr_shopRental_vote_assign",
                column: "sr_vote_shop_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_agreement_change_request");

            migrationBuilder.DropTable(
                name: "sr_opening_balance");

            migrationBuilder.DropTable(
                name: "sr_process_configuration");

            migrationBuilder.DropTable(
                name: "sr_shopRental_balance");

            migrationBuilder.DropTable(
                name: "sr_shopRental_vote_assign");

            migrationBuilder.DropTable(
                name: "sr_vote_payment_types");

            migrationBuilder.DropTable(
                name: "sr_shop");

            migrationBuilder.DropTable(
                name: "sr_property");

            migrationBuilder.DropTable(
                name: "sr_floor");

            migrationBuilder.DropTable(
                name: "sr_property_type");

            migrationBuilder.DropTable(
                name: "sr_property_nature");

            migrationBuilder.DropTable(
                name: "sr_rental_place");
        }
    }
}
